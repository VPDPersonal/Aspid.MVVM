using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the string-manipulation and rich-text converters, including
    /// <see cref="TextCase.Sentence"/>, <see cref="TextCase.Invert"/>,
    /// <see cref="SplitJoinStringConverter"/> and <see cref="ReverseStringConverter"/>.
    /// </summary>
    [TestFixture]
    internal sealed class TextConverterTests
    {
        [TestCase(null, "—")]
        [TestCase("", "—")]
        [TestCase("   ", "—")]
        [TestCase("abc", "abc")]
        public void DefaultString_SubstitutesForBlank(string value, string expected) =>
            Assert.AreEqual(expected, new DefaultStringConverter("—").Convert(value));

        [TestCase(TextCase.Upper, "hello world", "HELLO WORLD")]
        [TestCase(TextCase.Lower, "HELLO WORLD", "hello world")]
        [TestCase(TextCase.FirstUpper, "hello world", "Hello world")]
        [TestCase(TextCase.Title, "hello world", "Hello World")]
        public void TextCase_Recases(TextCase textCase, string value, string expected) =>
            Assert.AreEqual(
                expected,
                new TextCaseConverter(textCase, CultureInfoMode.InvariantCulture).Convert(value));

        // Recasing spaces would produce the same characters, so the assertion is on the reference:
        // the guard hands the string back rather than walking it.
        [TestCase("")]
        [TestCase("   ")]
        public void TextCase_LeavesBlankAlone(string value) =>
            Assert.AreSame(value, new TextCaseConverter(TextCase.Upper).Convert(value));

        [Test]
        public void Truncate_CutsTheEnd() =>
            Assert.AreEqual("abcdefghi…", new TruncateStringConverter(10).Convert("abcdefghijklmnop"));

        [Test]
        public void Truncate_LeavesShortStringsAlone() =>
            Assert.AreEqual("abc", new TruncateStringConverter(10).Convert("abc"));

        [Test]
        public void Truncate_CutsTheStartWhenAsked() =>
            Assert.AreEqual("…hijklmnop", new TruncateStringConverter(10, TruncateSide.Start).Convert("abcdefghijklmnop"));

        [Test]
        public void Truncate_CutsTheMiddleWhenAsked() =>
            Assert.AreEqual("abcde…mnop", new TruncateStringConverter(10, TruncateSide.Middle).Convert("abcdefghijklmnop"));

        [Test]
        public void Truncate_StopsAtAWordBoundaryWhenAsked() =>
            Assert.AreEqual(
                "hello…",
                new TruncateStringConverter(10, atWordBoundary: true).Convert("hello beautiful world"));

        // A limit shorter than the marker leaves nothing sensible to keep.
        [Test]
        public void Truncate_LimitShorterThanTheEllipsis() =>
            Assert.AreEqual("…", new TruncateStringConverter(1).Convert("abcdef"));

        // The word boundary is honoured by the End side alone — the other two have no head to walk
        // back through — so a Start cut lands mid-word however the flag is set.
        [Test]
        public void Truncate_WordBoundaryAppliesToTheEndSideOnly() =>
            Assert.AreEqual(
                "…iful world",
                new TruncateStringConverter(11, TruncateSide.Start, atWordBoundary: true).Convert("hello beautiful world"));

        // A surrogate pair is one character stored as two: a cut between the halves leaves a lone half
        // that renders as a replacement box, so the cut moves off it and the character is dropped whole.
        [Test]
        public void Truncate_DoesNotSplitASurrogatePair()
        {
            const string value = "abcd😀ef";

            Assert.AreEqual("abcd…", new TruncateStringConverter(6).Convert(value));
            Assert.AreEqual("…ef", new TruncateStringConverter(4, TruncateSide.Start).Convert(value));
        }

        // A limit no string could ever be shortened to is a misconfiguration, not a way to switch
        // the converter off, so it is reported on every push.
        [TestCase(0)]
        [TestCase(-5)]
        public void Truncate_ANonPositiveLimit_IsReported(int maxLength)
        {
            LogAssert.Expect(LogType.Error, new Regex("TruncateStringConverter.*not positive"));

            Assert.AreEqual("abcdef", new TruncateStringConverter(maxLength).Convert("abcdef"));
        }

        // The side is consulted only once the string is over the limit and the marker fits, so the
        // input has to clear both guards before an undeclared side can be reached at all. Unshortened
        // is the only answer that cannot be mistaken for one of the three declared cuts.
        [Test]
        public void Truncate_UndeclaredSide_ReportsAndReturnsTheStringUnshortened()
        {
            LogAssert.Expect(LogType.Error, new Regex("TruncateStringConverter.*not a declared TruncateSide"));

            Assert.AreEqual(
                "hello beautiful world",
                new TruncateStringConverter(10, (TruncateSide)42).Convert("hello beautiful world"));
        }

        [TestCase(TrimSide.Both, "  abc  ", "abc")]
        [TestCase(TrimSide.Start, "  abc  ", "abc  ")]
        [TestCase(TrimSide.End, "  abc  ", "  abc")]
        public void Trim_TrimsTheRequestedEnds(TrimSide side, string value, string expected) =>
            Assert.AreEqual(expected, new TrimStringConverter(side).Convert(value));

        [Test]
        public void Trim_TakesSpecificCharacters() =>
            Assert.AreEqual("abc", new TrimStringConverter(TrimSide.Both, "*").Convert("**abc**"));

        // Every declared side removes at least one of the two runs of stars, so the untouched string
        // is what tells an undeclared side apart from a trim that simply found nothing to take.
        [Test]
        public void Trim_UndeclaredSide_ReportsAndReturnsTheStringUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("TrimStringConverter.*not a declared TrimSide"));

            Assert.AreEqual("**abc**", new TrimStringConverter((TrimSide)42, "*").Convert("**abc**"));
        }

        // The characters are made once and kept, so re-authoring the field has to reach them. Unity
        // reads the object again after an Inspector edit, which is what SetField imitates.
        [Test]
        public void Trim_CharactersReauthored_TrimsTheNewOnes()
        {
            var converter = new TrimStringConverter(TrimSide.Both, "*");
            Assert.AreEqual("abc", converter.Convert("**abc**"));

            SetField(converter, "_trimChars", "#");

            Assert.AreEqual("abc", converter.Convert("##abc##"));
            Assert.AreEqual("**abc**", converter.Convert("**abc**"));
        }

        // An empty field means whitespace, and the emptiness has to survive the same round trip.
        [Test]
        public void Trim_CharactersClearedToEmpty_GoesBackToWhitespace()
        {
            var converter = new TrimStringConverter(TrimSide.Both, "*");
            Assert.AreEqual("abc", converter.Convert("**abc**"));

            SetField(converter, "_trimChars", string.Empty);

            Assert.AreEqual("abc", converter.Convert("  abc  "));
        }

        [Test]
        public void Replace_SwapsEveryOccurrence() =>
            Assert.AreEqual("a-b-c", new ReplaceStringConverter("_", "-").Convert("a_b_c"));

        [Test]
        public void Replace_CanIgnoreCase() =>
            Assert.AreEqual("xbx", new ReplaceStringConverter("a", "x", ignoreCase: true).Convert("AbA"));

        [Test]
        public void Replace_EmptySearchPassesThrough() =>
            Assert.AreEqual("abc", new ReplaceStringConverter("", "x").Convert("abc"));

        [Test]
        public void Mask_HidesTheMiddle() =>
            Assert.AreEqual("ab••••gh", new MaskStringConverter(2, 2).Convert("abcdefgh"));

        // A string too short to keep both ends is masked completely, so a short value never leaks by
        // being left alone.
        [Test]
        public void Mask_ShortStringIsMaskedCompletely() =>
            Assert.AreEqual("•••", new MaskStringConverter(2, 2).Convert("abc"));

        // A surrogate pair is one character stored as two: a count landing between the halves hides
        // the whole character rather than showing a lone half, which renders as a replacement box and
        // is a fragment of the value the converter was asked to hide.
        [Test]
        public void Mask_DoesNotSplitASurrogatePair()
        {
            const string value = "ab😀cd";

            Assert.AreEqual("ab••cd", new MaskStringConverter(3, 2).Convert(value));
            Assert.AreEqual("a•••cd", new MaskStringConverter(1, 3).Convert(value));
        }

        // A blank value has nothing to hide, so it comes back as it arrived rather than as a row of
        // bullets the width of the spaces.
        [Test]
        public void Mask_BlankIsLeftUnmasked()
        {
            const string value = "   ";

            Assert.AreSame(value, new MaskStringConverter(2, 2).Convert(value));
        }

        [Test]
        public void Repeat_WritesOneUnitPerCount() =>
            Assert.AreEqual("★★★", new RepeatStringConverter("★").Convert(3));

        [Test]
        public void Repeat_FillsTheRemainderToTheMaximum() =>
            Assert.AreEqual("★★★☆☆", new RepeatStringConverter("★", 5, "☆").Convert(3));

        [Test]
        public void Repeat_ClampsAboveTheMaximum() =>
            Assert.AreEqual("★★★★★", new RepeatStringConverter("★", 5, "☆").Convert(9));

        [Test]
        public void Repeat_NegativeCountWritesNothing() =>
            Assert.AreEqual(string.Empty, new RepeatStringConverter("★").Convert(-3));

        // With no maximum the count is whatever the ViewModel sends, and a runaway one would be built
        // into a string of that many units before anything noticed. The ceiling is reported, not silent.
        [Test]
        public void Repeat_NoMaximum_CapsTheCountAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("RepeatStringConverter.*ceiling"));

            Assert.AreEqual(1000, new RepeatStringConverter("★", max: 0).Convert(5000).Length);
        }

        [Test]
        public void Pad_PadsToWidth()
        {
            Assert.AreEqual("     abc", new PadStringConverter(8).Convert("abc"));
            Assert.AreEqual("abc     ", new PadStringConverter(8, padLeft: false).Convert("abc"));
        }

        [Test]
        public void Substring_TakesTheSlice() =>
            Assert.AreEqual("bcd", new SubstringConverter(1, 3).Convert("abcdef"));

        [Test]
        public void Substring_ClampsToWhatIsThere() =>
            Assert.AreEqual("ef", new SubstringConverter(4, 10).Convert("abcdef"));

        [Test]
        public void Substring_StartPastTheEndYieldsEmpty() =>
            Assert.AreEqual(string.Empty, new SubstringConverter(10, 3).Convert("abc"));

        // Nothing to slice: the guard hands the string back whole rather than cutting a space out of it.
        [Test]
        public void Substring_BlankIsReturnedUnchanged()
        {
            const string value = "   ";

            Assert.AreSame(value, new SubstringConverter(0, 1).Convert(value));
        }

        [Test]
        public void Concat_WrapsTheValue() =>
            Assert.AreEqual("[abc]", new ConcatStringConverter("[", "]").Convert("abc"));

        [TestCase("")]
        [TestCase("   ")]
        public void Concat_LeavesBlankUndecorated(string value) =>
            Assert.AreSame(value, new ConcatStringConverter("[", "]").Convert(value));

        [Test]
        public void Concat_DecoratesBlankWhenAsked() =>
            Assert.AreEqual("[]", new ConcatStringConverter("[", "]", skipWhenEmpty: false).Convert(string.Empty));

        [Test]
        public void Concat_RoundTrips()
        {
            var converter = new ConcatStringConverter("[", "]");

            Assert.AreEqual("abc", converter.ConvertBack(converter.Convert("abc")));
        }

        // Text the user typed without the decoration comes back as they typed it, so a two-way input
        // field does not have to carry the brackets to be read.
        [TestCase("abc", "abc")]
        [TestCase("[abc", "abc")]
        [TestCase("abc]", "abc")]
        public void Concat_ConvertBack_UndecoratedTextIsLeftAlone(string value, string expected) =>
            Assert.AreEqual(expected, new ConcatStringConverter("[", "]").ConvertBack(value));

        // The prefix is claimed first, so the two cannot both take the same characters of a string
        // shorter than they are together.
        [Test]
        public void Concat_ConvertBack_PrefixAndSuffixDoNotOverlap() =>
            Assert.AreEqual(string.Empty, new ConcatStringConverter("ab", "ab").ConvertBack("ab"));

        // A player name like <size=400%> resizes the label it lands in, on every screen showing that
        // player. noparse makes TMP render the characters instead of obeying them.
        [Test]
        public void RichTextNoParse_NeutralisesMarkup() =>
            Assert.AreEqual(
                "<noparse><size=400%>troll</noparse>",
                new RichTextNoParseConverter().Convert("<size=400%>troll"));

        [TestCase("")]
        [TestCase("   ")]
        public void RichTextNoParse_LeavesBlankAlone(string value) =>
            Assert.AreSame(value, new RichTextNoParseConverter().Convert(value));

        [Test]
        public void RichTextColor_TagsTheText() =>
            Assert.AreEqual("<color=#FF0000>hp</color>", new RichTextColorConverter(Color.red).Convert("hp"));

        [Test]
        public void RichTextColor_IncludesAlphaWhenAsked() =>
            Assert.AreEqual(
                "<color=#FF0000FF>hp</color>",
                new RichTextColorConverter(Color.red, includeAlpha: true).Convert("hp"));

        // Explicit channels rather than Color.yellow, which is #FFEB04 in Unity rather than #FFFF00.
        private static readonly Color _pureYellow = new(1f, 1f, 0f);

        [Test]
        public void ThresholdRichTextColor_PicksTheHighestQualifyingStop()
        {
            var converter = new ThresholdRichTextColorConverter(
                new[]
                {
                    new ColorStop(0.75f, Color.green),
                    new ColorStop(0.25f, _pureYellow),
                },
                fallback: Color.red);

            Assert.AreEqual("<color=#00FF00>0.8</color>", converter.Convert(0.8f));
            Assert.AreEqual("<color=#FFFF00>0.5</color>", converter.Convert(0.5f));
            Assert.AreEqual("<color=#FF0000>0.1</color>", converter.Convert(0.1f));
        }

        // The stops are authored in whatever order the Inspector left them.
        [Test]
        public void ThresholdRichTextColor_DoesNotDependOnStopOrder()
        {
            var ascending = new ThresholdRichTextColorConverter(
                new[]
                {
                    new ColorStop(0.25f, _pureYellow),
                    new ColorStop(0.75f, Color.green),
                },
                fallback: Color.red);

            Assert.AreEqual("<color=#00FF00>0.8</color>", ascending.Convert(0.8f));
        }

        // The number slot takes any converter, so the text inside the tag is not limited to a
        // numeric format string.
        [Test]
        public void ThresholdRichTextColor_NumberConverter_WritesTheNumber()
        {
            var converter = new ThresholdRichTextColorConverter(
                new[] { new ColorStop(0.25f, _pureYellow) },
                fallback: Color.red,
                number: new NumberFormatConverter("F2", CultureInfoMode.InvariantCulture));

            Assert.AreEqual("<color=#FFFF00>0.50</color>", converter.Convert(0.5f));
        }

        // An empty stop table is a converter that can never pick anything, so it is reported rather
        // than quietly painting everything the fallback color. The number is still written and still
        // wrapped, so the tag proves the failure happened inside the color pick alone.
        [Test]
        public void ThresholdRichTextColor_NoStops_ReportsItAndUsesTheFallbackColor()
        {
            LogAssert.Expect(LogType.Error, new Regex("ThresholdRichTextColorConverter.*no stops are authored"));

            var converter = new ThresholdRichTextColorConverter(
                System.Array.Empty<ColorStop>(),
                fallback: Color.red,
                number: new NumberFormatConverter("F2", CultureInfoMode.InvariantCulture));

            Assert.AreEqual("<color=#FF0000>0.50</color>", converter.Convert(0.5f));
        }

        [Test]
        public void RichTextStyle_WrapsInTheRequestedTags() =>
            Assert.AreEqual("<i><b>hp</b></i>", new RichTextStyleConverter(bold: true, italic: true).Convert("hp"));

        [Test]
        public void RichTextStyle_WithNothingSetLeavesTheTextAlone() =>
            Assert.AreEqual("hp", new RichTextStyleConverter().Convert("hp"));

        // There is nothing to style in a blank string, so no tag is put around it — a label bound to
        // an unfilled field stays empty instead of holding a pair of empty tags.
        [TestCase("")]
        [TestCase("   ")]
        public void RichTextStyle_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextStyleConverter(bold: true).Convert(value));

        [TestCase("")]
        [TestCase("   ")]
        public void RichTextColor_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextColorConverter(Color.red).Convert(value));

        [TestCase("")]
        [TestCase("   ")]
        public void RichTextSize_LeavesBlankUntagged(string value) =>
            Assert.AreSame(value, new RichTextSizeConverter(200f).Convert(value));

        [Test]
        public void RichTextSize_TagsAsPercentByDefault() =>
            Assert.AreEqual("<size=150%>hp</size>", new RichTextSizeConverter(150f).Convert("hp"));

        [Test]
        public void RichTextSize_TagsAsPointsWhenAsked() =>
            Assert.AreEqual("<size=32>hp</size>", new RichTextSizeConverter(32f, isPercent: false).Convert("hp"));

        [TestCase(0f)]
        [TestCase(-50f)]
        [TestCase(float.NaN)]
        public void RichTextSize_NotAboveZero_IsRefusedByTheConstructor(float size) =>
            Assert.Throws<System.ArgumentOutOfRangeException>(() => new RichTextSizeConverter(size));

        // A size the Inspector can still hold — an animated or copied value — has to show the text at
        // its own size rather than emit <size=0%>, which draws nothing at all.
        [TestCase(0f)]
        [TestCase(-50f)]
        [TestCase(float.NaN)]
        public void RichTextSize_SerializedSizeNotAboveZero_IsReportedAndLeavesTheStringUntagged(float size)
        {
            LogAssert.Expect(LogType.Error, new Regex("RichTextSizeConverter.*no text can be drawn at"));

            var converter = new RichTextSizeConverter(100f);
            SetField(converter, "_size", size);

            Assert.AreEqual("hp", converter.Convert("hp"));
        }

        #region TextCase — serialized ordinals

        // TextCase is a [SerializeField] on TextCaseConverter, so the ordinal is what the scene
        // stores. Inserting a member above these two rather than appending silently re-cases every
        // converter already authored, with nothing in the diff to show it.
        [TestCase(TextCase.Upper, 0)]
        [TestCase(TextCase.Lower, 1)]
        [TestCase(TextCase.FirstUpper, 2)]
        [TestCase(TextCase.Title, 3)]
        [TestCase(TextCase.Sentence, 4)]
        [TestCase(TextCase.Invert, 5)]
        public void TextCase_KeepsItsSerializedOrdinal(TextCase textCase, int ordinal) =>
            Assert.AreEqual(ordinal, (int)textCase);

        #endregion

        #region TextCase.Sentence

        [TestCase("hello world. how are you? fine! ok", "Hello world. How are you? Fine! Ok")]
        [TestCase("hello", "Hello")]
        [TestCase("x", "X")]
        [TestCase("no terminator", "No terminator")]
        [TestCase("hello   world. two spaces", "Hello   world. Two spaces")]
        public void Sentence_RaisesTheFirstLetterOfEachSentence(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // "the rest lower" is the half of the contract that is easy to lose: the loop lowers every
        // character it does not raise, so a shouted string comes back as prose rather than unchanged.
        [TestCase("HELLO WORLD. GOODBYE", "Hello world. Goodbye")]
        [TestCase("MIXED case. SECOND one!", "Mixed case. Second one!")]
        public void Sentence_LowersEverythingItDoesNotRaise(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // The sentence stays open across anything that is not a letter, so the space, the closing
        // quote and the extra full stops after a terminator are passed over rather than consuming the
        // opening. A scanner that closed on the first character after the stop would raise the space
        // and leave the word lower.
        [TestCase("  hello. there", "  Hello. There")]
        [TestCase("hello... world", "Hello... World")]
        [TestCase("\"hello. \"there\"", "\"Hello. \"There\"")]
        [TestCase("hello?world", "Hello?World")]
        public void Sentence_SkipsNonLettersLookingForTheOpening(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // The trap, and a deviation from what "sentence case" normally means. The opening closes on
        // the first *letter*, not on the first character of a word, so a token starting with a digit
        // has its first letter raised in the middle of the word. Documented here because it is
        // behavior a caller will meet the moment a label shows an ordinal or a quantity.
        [TestCase("1st place. 2nd place", "1St place. 2Nd place")]
        [TestCase("42 apples. 7 pears", "42 Apples. 7 Pears")]
        public void Sentence_ADigitDoesNotCloseTheOpening(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // There is no abbreviation list — any full stop reopens — so an abbreviation is read as a
        // sentence boundary. Worth pinning so that a future "smarter" scanner has to change a test
        // rather than change behavior silently.
        [TestCase("e.g. this", "E.G. This")]
        [TestCase("dr. smith went home", "Dr. Smith went home")]
        [TestCase("a.b.c", "A.B.C")]
        public void Sentence_TreatsEveryFullStopAsABoundary(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // A string the scanner never finds a letter in must come back byte for byte; the opening flag
        // stays true for the whole walk and nothing is raised. Spaces alone never reach the walk —
        // they are blank, and TextCase_LeavesBlankAlone pins the guard that returns them.
        [TestCase("...", "...")]
        [TestCase("!!!", "!!!")]
        [TestCase("ends with. ", "Ends with. ")]
        public void Sentence_WithNoLetterToRaiseIsUnchanged(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        #endregion

        #region TextCase.Invert

        [TestCase("Hello World", "hELLO wORLD")]
        [TestCase("hELLO wORLD", "Hello World")]
        public void Invert_SwapsTheCaseOfEveryLetter(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Invert).Convert(value));

        // The branch asks IsUpper and sends everything else through ToUpper, so digits, spaces and
        // punctuation take the "raise" path and come back unchanged. An implementation that tested
        // IsLower in the else and lowered the remainder would pass the letters-only cases above and
        // fail here.
        [TestCase("abc123XYZ!", "ABC123xyz!")]
        [TestCase("123 !@#-_", "123 !@#-_")]
        public void Invert_LeavesNonLettersAlone(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Invert).Convert(value));

        // U+00DF has no single-character upper case — its uppercase form is two letters — so
        // char.ToUpper hands it back untouched and the string keeps its length. A rewrite that
        // upper-cased whole runs with string.ToUpper is free to expand it and change the length,
        // which is what this case is here to catch.
        [Test]
        public void Invert_SharpSHasNoSingleCharacterUpperCaseAndSurvives() =>
            Assert.AreEqual("sTRA\u00DFE", Case(TextCase.Invert).Convert("Stra\u00DFe"));

        // The loop is per-char, and neither half of a surrogate pair is an upper-case letter, so an
        // emoji passes through while the ASCII around it swaps. Casing a code unit of a pair would
        // corrupt the character.
        [Test]
        public void Invert_LeavesSurrogatePairsIntact() =>
            Assert.AreEqual("A\uD83D\uDE00b", Case(TextCase.Invert).Convert("a\uD83D\uDE00B"));

        // Applying Invert twice is its own undo on the ASCII letters, which is the property a caller
        // will assume. It is NOT an involution in general — a Unicode title-case letter raises to a
        // different code point than the one it lowers from — so the claim is pinned only where it
        // actually holds.
        [Test]
        public void Invert_AppliedTwiceRestoresAsciiText()
        {
            var converter = Case(TextCase.Invert);

            Assert.AreEqual("Hello World", converter.Convert(converter.Convert("Hello World")));
        }

        #endregion

        #region TextCaseConverter — shared state

        // Sentence and Invert are the two branches that use the instance's cached StringBuilder. Two
        // calls on one converter must not concatenate; a missing Clear() would return "HelloWorld"
        // from the second call here.
        [TestCase(TextCase.Sentence, "hello", "Hello", "world", "World")]
        [TestCase(TextCase.Invert, "abc", "ABC", "def", "DEF")]
        public void TextCase_ReusingOneInstanceDoesNotAccumulate(
            TextCase textCase, string first, string firstExpected, string second, string secondExpected)
        {
            var converter = Case(textCase);

            Assert.AreEqual(firstExpected, converter.Convert(first));
            Assert.AreEqual(secondExpected, converter.Convert(second), "the cached builder leaked the previous call");
        }

        // The blank guard sits above the switch, so it has to keep working for members added below
        // the ones it was written for.
        [TestCase(TextCase.Sentence)]
        [TestCase(TextCase.Invert)]
        public void TextCase_BlankIsReturnedUnchanged(TextCase textCase)
        {
            Assert.IsNull(Case(textCase).Convert(null));
            Assert.AreEqual(string.Empty, Case(textCase).Convert(string.Empty));
        }

        // Mixed casing on the way in is what makes the answer readable: every declared case changes
        // "hELLo" somehow, so the string coming back spelled as authored is the undeclared branch and
        // nothing else.
        [Test]
        public void TextCase_UndeclaredCase_ReportsAndReturnsTheStringUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("TextCaseConverter.*not a declared TextCase"));

            Assert.AreEqual("hELLo", Case((TextCase)42).Convert("hELLo"));
        }

        // The blank guard sits above the switch, so a blank value never reaches the undeclared branch
        // and there is nothing to report.
        [Test]
        public void TextCase_UndeclaredCase_BlankIsReturnedWithoutReporting() =>
            Assert.AreEqual(string.Empty, Case((TextCase)42).Convert(string.Empty));

        #endregion

        #region SplitJoinStringConverter — the happy path

        [Test]
        public void SplitJoin_RejoinsWithTheReplacementSeparator() =>
            Assert.AreEqual(
                "sword | shield | potion",
                new SplitJoinStringConverter(",", " | ").Convert("sword,shield,potion"));

        // The parameterless constructor is what a freshly added component in the Inspector holds.
        [Test]
        public void SplitJoin_DefaultConstructedRespacesACommaList() =>
            Assert.AreEqual("sword, shield, potion", new SplitJoinStringConverter().Convert("sword,shield,potion"));

        [Test]
        public void SplitJoin_SplitsOnAMultiCharacterSeparator() =>
            Assert.AreEqual("a-b-c", new SplitJoinStringConverter("<>", "-").Convert("a<>b<>c"));

        #endregion

        #region SplitJoinStringConverter — _maxParts

        // The limit counts parts, not separators, and the last part swallows the remainder with its
        // separators still in it — the same bargain string.Split(count) makes. Zero and anything
        // negative mean no limit, because the guard is "> 0"; a limit at or above the real part count
        // is indistinguishable from no limit.
        [TestCase(0, "a | b | c | d")]
        [TestCase(1, "a,b,c,d")]
        [TestCase(2, "a | b,c,d")]
        [TestCase(3, "a | b | c,d")]
        [TestCase(4, "a | b | c | d")]
        [TestCase(9, "a | b | c | d")]
        [TestCase(-1, "a | b | c | d")]
        public void SplitJoin_MaxPartsCapsThePartCount(int maxParts, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", " | ", maxParts).Convert("a,b,c,d"));

        // Trimming applies to the ends of each part, and the capped last part is one part — so the
        // whitespace inside the swallowed remainder is left exactly as it arrived while the outer
        // edges still come off. Easy to get wrong by trimming the whole tail or not trimming it at all.
        [Test]
        public void SplitJoin_TrimsTheEndsOfTheSwallowedRemainderOnly() =>
            Assert.AreEqual("a|b , c", new SplitJoinStringConverter(",", "|", maxParts: 2).Convert("a, b , c "));

        #endregion

        #region SplitJoinStringConverter — degenerate input

        // The empty string is returned by the guard rather than walked, so it never becomes a single
        // empty part with a join around it.
        [Test]
        public void SplitJoin_EmptyInputIsReturnedUnchanged() =>
            Assert.AreEqual(string.Empty, new SplitJoinStringConverter(",", " | ").Convert(string.Empty));

        [Test]
        public void SplitJoin_NullInputIsReturnedUnchanged() =>
            Assert.IsNull(new SplitJoinStringConverter(",", " | ").Convert(null));

        // Whitespace is blank too, so it takes the same guard as the empty string rather than going
        // through the loop as one part trimmed to nothing: a blank label is preserved, not emptied.
        [Test]
        public void SplitJoin_WhitespaceOnlyInputIsReturnedUnchanged()
        {
            const string value = "   ";

            Assert.AreSame(value, new SplitJoinStringConverter(",", " | ").Convert(value));
        }

        [Test]
        public void SplitJoin_InputWithoutTheSeparatorIsOnePart() =>
            Assert.AreEqual("abc", new SplitJoinStringConverter(",", " | ").Convert("abc"));

        // Nothing to split on, so the value has to survive untouched — including the trimming, which
        // is skipped along with the rest of the walk.
        [Test]
        public void SplitJoin_EmptySeparatorPassesTheValueThrough() =>
            Assert.AreEqual(" a,b ", new SplitJoinStringConverter(string.Empty, " | ").Convert(" a,b "));

        // An empty part is still a part: it gets its join and its place. Dropping them would be a
        // different converter, and a boundary separator is exactly where a hand-rolled scanner tends
        // to lose one.
        [TestCase("a,b,", "a | b | ")]
        [TestCase(",a,b", " | a | b")]
        [TestCase("a,,b", "a |  | b")]
        [TestCase(",", " | ")]
        public void SplitJoin_EmptyPartsAreKept(string value, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", " | ").Convert(value));

        #endregion

        #region SplitJoinStringConverter — _trimParts and shared state

        // "a, b ,c" and "a,b,c" have to produce the same thing, which is the whole point of the
        // default. Tabs count too, because the scan uses char.IsWhiteSpace rather than a space test.
        [TestCase("a, b ,c", "a|b|c")]
        [TestCase("a,\tb\t,c", "a|b|c")]
        [TestCase("a,   ,b", "a||b")]
        public void SplitJoin_TrimsPartsByDefault(string value, string expected) =>
            Assert.AreEqual(expected, new SplitJoinStringConverter(",", "|").Convert(value));

        // _trimParts defaults to true and no constructor overload exposes it, so the untrimmed path
        // is reachable only from the Inspector — and therefore only from reflection here. Without
        // this case that branch of Append is dead as far as the suite is concerned.
        [Test]
        public void SplitJoin_TrimPartsOffKeepsTheWhitespaceAroundEachPart()
        {
            var converter = new SplitJoinStringConverter(",", "-");
            SetField(converter, "_trimParts", false);

            Assert.AreEqual("a- b -c", converter.Convert("a, b ,c"));
        }

        // The builder is cached on the instance; without the Clear() the second call would return
        // "a|bx|y".
        [Test]
        public void SplitJoin_ReusingOneInstanceDoesNotAccumulate()
        {
            var converter = new SplitJoinStringConverter(",", "|");

            Assert.AreEqual("a|b", converter.Convert("a,b"));
            Assert.AreEqual("x|y", converter.Convert("x,y"), "the cached builder leaked the previous call");
        }

        #endregion

        #region ReverseStringConverter

        [TestCase("abc", "cba")]
        [TestCase("a", "a")]
        [TestCase("ab", "ba")]
        [TestCase("", "")]
        public void Reverse_WritesTheStringBackToFront(string value, string expected) =>
            Assert.AreEqual(expected, new ReverseStringConverter().Convert(value));

        // Reversing spaces would produce the same characters, so the reference is what pins the guard.
        [Test]
        public void Reverse_BlankIsReturnedUnchanged()
        {
            const string value = " \t ";

            Assert.AreSame(value, new ReverseStringConverter().Convert(value));
        }

        [Test]
        public void Reverse_NullIsReturnedUnchanged() =>
            Assert.IsNull(new ReverseStringConverter().Convert(null));

        // The promise the class actually makes: a surrogate pair is one character stored as two, and
        // the pair keeps its internal order while everything around it reverses. A plain
        // char-by-char reversal produces two lone surrogates here and renders as a replacement box.
        [Test]
        public void Reverse_KeepsSurrogatePairsInOrder() =>
            Assert.AreEqual("dc\uD83D\uDE00ba", new ReverseStringConverter().Convert("ab\uD83D\uDE00cd"));

        [Test]
        public void Reverse_SwapsWholePairsRatherThanCodeUnits() =>
            Assert.AreEqual(
                "\uD83C\uDF89\uD83D\uDE00",
                new ReverseStringConverter().Convert("\uD83D\uDE00\uD83C\uDF89"));

        [Test]
        public void Reverse_OfAReversedPairStringIsTheOriginal()
        {
            const string value = "ab\uD83D\uDE00cd";
            var converter = new ReverseStringConverter();

            Assert.AreEqual(value, converter.Convert(converter.Convert(value)));
        }

        // Malformed input reaches converters from user-typed text and truncated network strings. A
        // surrogate with no partner is copied as an ordinary code unit rather than pairing with the
        // letter beside it.
        [Test]
        public void Reverse_LoneHighSurrogateIsCopiedAsIs() =>
            Assert.AreEqual("b\uD83Da", new ReverseStringConverter().Convert("a\uD83Db"));

        // The pair test reads value[i - 1], so a low surrogate at index 0 is where an unguarded read
        // walks off the front of the string. The loop's "i > 0" is the only thing preventing it.
        [Test]
        public void Reverse_LoneLowSurrogateAtIndexZeroDoesNotReadPastTheStart() =>
            Assert.AreEqual("ba\uDE00", new ReverseStringConverter().Convert("\uDE00ab"));

        // The guard is directional — high at i-1, low at i — so a low/high sequence is not a pair and
        // is reversed like any other two characters. Reversing malformed input can therefore hand back
        // a well-formed emoji.
        [Test]
        public void Reverse_LowThenHighIsNotTreatedAsAPair() =>
            Assert.AreEqual("\uD83D\uDE00", new ReverseStringConverter().Convert("\uDE00\uD83D"));

        // Contradicts the class remarks, which say the mark "lands on the letter that used to precede
        // it". A combining mark decorates the character before it, and plain reversal puts the mark
        // in front of its old host and behind whatever used to follow it — so here the accent moves
        // off "e" and onto "y". Asserted as the behavior, not as the documented claim.
        [Test]
        public void Reverse_CombiningMarkMovesOntoTheFollowingCharacter() =>
            Assert.AreEqual("y\u0301ex", new ReverseStringConverter().Convert("xe\u0301y"));

        // With nothing after it the mark ends up at index 0, decorating nothing at all.
        [Test]
        public void Reverse_TrailingCombiningMarkEndsUpDanglingAtTheStart() =>
            Assert.AreEqual("\u0301eba", new ReverseStringConverter().Convert("abe\u0301"));

        // Without the Clear() the second call would return "cbazyx".
        [Test]
        public void Reverse_ReusingOneInstanceDoesNotAccumulate()
        {
            var converter = new ReverseStringConverter();

            Assert.AreEqual("cba", converter.Convert("abc"));
            Assert.AreEqual("zyx", converter.Convert("xyz"), "the cached builder leaked the previous call");
        }

        #endregion

        private static TextCaseConverter Case(TextCase textCase) =>
            new TextCaseConverter(textCase, CultureInfoMode.InvariantCulture);

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);

            // Unity reads the object again after an Inspector edit, which is where a converter
            // holding a cache built from its settings drops it.
            if (target is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }
    }
}
