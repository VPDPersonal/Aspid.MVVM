using NUnit.Framework;
using System.Reflection;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the members added to <c>TextConverters.cs</c> in this batch:
    /// <see cref="TextCase.Sentence"/>, <see cref="TextCase.Invert"/>,
    /// <see cref="SplitJoinStringConverter"/> and <see cref="ReverseStringConverter"/>.
    /// </summary>
    /// <remarks>
    /// All four members walk the string themselves rather than calling <c>string.Split</c>,
    /// <c>ToLower</c> and <c>Array.Reverse</c>, so the interesting inputs are the degenerate ones — an
    /// empty string, a string that is only separators, a mark at index 0, a limit of one. Three of them
    /// keep a <see cref="System.Text.StringBuilder"/> on the instance, so each is exercised twice on one
    /// instance to catch a missing <c>Clear()</c>.
    /// <para>
    /// Expectations were taken from running the implementation; where one contradicts the documentation,
    /// the test says so in its name. The culture is pinned to
    /// <see cref="CultureInfoMode.InvariantCulture"/> — the default would make these machine-dependent.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class TextConverterAdditionsTests
    {
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
        // behaviour a caller will meet the moment a label shows an ordinal or a quantity.
        [TestCase("1st place. 2nd place", "1St place. 2Nd place")]
        [TestCase("42 apples. 7 pears", "42 Apples. 7 Pears")]
        public void Sentence_ADigitDoesNotCloseTheOpening(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // There is no abbreviation list — any full stop reopens — so an abbreviation is read as a
        // sentence boundary. Worth pinning so that a future "smarter" scanner has to change a test
        // rather than change behaviour silently.
        [TestCase("e.g. this", "E.G. This")]
        [TestCase("dr. smith went home", "Dr. Smith went home")]
        [TestCase("a.b.c", "A.B.C")]
        public void Sentence_TreatsEveryFullStopAsABoundary(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // A string the scanner never finds a letter in must come back byte for byte; the opening flag
        // stays true for the whole walk and nothing is raised.
        [TestCase("...", "...")]
        [TestCase("!!!", "!!!")]
        [TestCase("  ", "  ")]
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

        // Whitespace is not empty, so this one does go through the loop: one part, trimmed to
        // nothing. A blank label therefore comes out empty rather than preserved — the opposite of
        // what the empty-string guard above does, and the difference is worth pinning.
        [Test]
        public void SplitJoin_WhitespaceOnlyInputCollapsesToEmpty() =>
            Assert.AreEqual(string.Empty, new SplitJoinStringConverter(",", " | ").Convert("   "));

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
        // off "e" and onto "y". Asserted as the behaviour, not as the documented claim.
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
            field!.SetValue(target, value);
        }
    }
}
