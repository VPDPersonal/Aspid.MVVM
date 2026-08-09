using UnityEngine;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the string-manipulation and rich-text converters.
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

        [Test]
        public void DefaultString_CanTreatSpacesAsContent() =>
            Assert.AreEqual("   ", new DefaultStringConverter("—", treatWhiteSpaceAsEmpty: false).Convert("   "));

        [TestCase(TextCase.Upper, "hello world", "HELLO WORLD")]
        [TestCase(TextCase.Lower, "HELLO WORLD", "hello world")]
        [TestCase(TextCase.FirstUpper, "hello world", "Hello world")]
        [TestCase(TextCase.Title, "hello world", "Hello World")]
        public void TextCase_Recases(TextCase textCase, string value, string expected) =>
            Assert.AreEqual(
                expected,
                new TextCaseConverter(textCase, CultureInfoMode.InvariantCulture).Convert(value));

        [Test]
        public void TextCase_LeavesBlankAlone() =>
            Assert.AreEqual(string.Empty, new TextCaseConverter(TextCase.Upper).Convert(string.Empty));

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
                new TruncateStringConverter(10, TruncateSide.End, atWordBoundary: true).Convert("hello beautiful world"));

        // A limit shorter than the marker leaves nothing sensible to keep.
        [Test]
        public void Truncate_LimitShorterThanTheEllipsis() =>
            Assert.AreEqual("…", new TruncateStringConverter(1).Convert("abcdef"));

        [TestCase(TrimSide.Both, "  abc  ", "abc")]
        [TestCase(TrimSide.Start, "  abc  ", "abc  ")]
        [TestCase(TrimSide.End, "  abc  ", "  abc")]
        public void Trim_TrimsTheRequestedEnds(TrimSide side, string value, string expected) =>
            Assert.AreEqual(expected, new TrimStringConverter(side).Convert(value));

        [Test]
        public void Trim_TakesSpecificCharacters() =>
            Assert.AreEqual("abc", new TrimStringConverter(TrimSide.Both, "*").Convert("**abc**"));

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

        [Test]
        public void Pad_PadsToWidth()
        {
            Assert.AreEqual("     abc", new PadStringConverter(8).Convert("abc"));
            Assert.AreEqual("abc     ", new PadStringConverter(8, ' ', padLeft: false).Convert("abc"));
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

        [Test]
        public void Concat_WrapsTheValue() =>
            Assert.AreEqual("[abc]", new ConcatStringConverter("[", "]").Convert("abc"));

        [Test]
        public void Concat_LeavesBlankUndecorated() =>
            Assert.AreEqual(string.Empty, new ConcatStringConverter("[", "]").Convert(string.Empty));

        [Test]
        public void Concat_DecoratesBlankWhenAsked() =>
            Assert.AreEqual("[]", new ConcatStringConverter("[", "]", skipWhenEmpty: false).Convert(string.Empty));

        // A player name like <size=400%> resizes the label it lands in, on every screen showing that
        // player. noparse makes TMP render the characters instead of obeying them.
        [Test]
        public void RichTextNoParse_NeutralisesMarkup() =>
            Assert.AreEqual(
                "<noparse><size=400%>troll</noparse>",
                new RichTextNoParseConverter().Convert("<size=400%>troll"));

        [Test]
        public void RichTextNoParse_LeavesBlankAlone() =>
            Assert.AreEqual(string.Empty, new RichTextNoParseConverter().Convert(string.Empty));

        [Test]
        public void RichTextColor_TagsTheText() =>
            Assert.AreEqual("<color=#FF0000>hp</color>", new RichTextColorConverter(Color.red).Convert("hp"));

        [Test]
        public void RichTextColor_IncludesAlphaWhenAsked() =>
            Assert.AreEqual(
                "<color=#FF0000FF>hp</color>",
                new RichTextColorConverter(Color.red, includeAlpha: true).Convert("hp"));

        // Explicit channels rather than Color.yellow, which is #FFEB04 in Unity rather than #FFFF00.
        private static readonly Color PureYellow = new(1f, 1f, 0f);

        [Test]
        public void ThresholdRichTextColor_PicksTheHighestQualifyingStop()
        {
            var converter = new ThresholdRichTextColorConverter(
                new[]
                {
                    new ColorStop { Threshold = 0.75f, Color = Color.green },
                    new ColorStop { Threshold = 0.25f, Color = PureYellow },
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
                    new ColorStop { Threshold = 0.25f, Color = PureYellow },
                    new ColorStop { Threshold = 0.75f, Color = Color.green },
                },
                fallback: Color.red);

            Assert.AreEqual("<color=#00FF00>0.8</color>", ascending.Convert(0.8f));
        }

        [Test]
        public void RichTextStyle_WrapsInTheRequestedTags() =>
            Assert.AreEqual("<i><b>hp</b></i>", new RichTextStyleConverter(bold: true, italic: true).Convert("hp"));

        [Test]
        public void RichTextStyle_WithNothingSetLeavesTheTextAlone() =>
            Assert.AreEqual("hp", new RichTextStyleConverter().Convert("hp"));

        [Test]
        public void RichTextSize_TagsAsPercentByDefault() =>
            Assert.AreEqual("<size=150%>hp</size>", new RichTextSizeConverter(150f).Convert("hp"));

        [Test]
        public void RichTextSize_TagsAsPointsWhenAsked() =>
            Assert.AreEqual("<size=32>hp</size>", new RichTextSizeConverter(32f, isPercent: false).Convert("hp"));
    }
}
