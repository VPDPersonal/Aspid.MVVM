using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="TextCaseConverter"/>, including <see cref="TextCase.Sentence"/> and
    /// <see cref="TextCase.Invert"/>.
    /// </summary>
    [TestFixture]
    public sealed class TextCaseConverterTests
    {
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

        // A deviation from what "sentence case" normally means: the opening closes on the first
        // *letter*, not on the first character of a word, so a token starting with a digit has its
        // first letter raised in the middle of the word.
        [TestCase("1st place. 2nd place", "1St place. 2Nd place")]
        [TestCase("42 apples. 7 pears", "42 Apples. 7 Pears")]
        public void Sentence_ADigitDoesNotCloseTheOpening(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        // There is no abbreviation list, so any full stop reopens a new sentence.
        [TestCase("e.g. this", "E.G. This")]
        [TestCase("dr. smith went home", "Dr. Smith went home")]
        [TestCase("a.b.c", "A.B.C")]
        public void Sentence_TreatsEveryFullStopAsABoundary(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        [TestCase("...", "...")]
        [TestCase("!!!", "!!!")]
        [TestCase("ends with. ", "Ends with. ")]
        public void Sentence_WithNoLetterToRaiseIsUnchanged(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Sentence).Convert(value));

        [TestCase("Hello World", "hELLO wORLD")]
        [TestCase("hELLO wORLD", "Hello World")]
        public void Invert_SwapsTheCaseOfEveryLetter(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Invert).Convert(value));

        // The branch asks IsUpper and sends everything else through ToUpper, so digits, spaces and
        // punctuation take the "raise" path and come back unchanged.
        [TestCase("abc123XYZ!", "ABC123xyz!")]
        [TestCase("123 !@#-_", "123 !@#-_")]
        public void Invert_LeavesNonLettersAlone(string value, string expected) =>
            Assert.AreEqual(expected, Case(TextCase.Invert).Convert(value));

        // U+00DF has no single-character upper case, so char.ToUpper hands it back untouched and the
        // string keeps its length.
        [Test]
        public void Invert_SharpSHasNoSingleCharacterUpperCaseAndSurvives() =>
            Assert.AreEqual("sTRAßE", Case(TextCase.Invert).Convert("Straße"));

        // The loop is per-char, and neither half of a surrogate pair is an upper-case letter, so an
        // emoji passes through while the ASCII around it swaps.
        [Test]
        public void Invert_LeavesSurrogatePairsIntact() =>
            Assert.AreEqual("A😀b", Case(TextCase.Invert).Convert("a😀B"));

        // NOT an involution in general — a Unicode title-case letter raises to a different code point
        // than the one it lowers from — so the claim is pinned only where it actually holds.
        [Test]
        public void Invert_AppliedTwiceRestoresAsciiText()
        {
            var converter = Case(TextCase.Invert);

            Assert.AreEqual("Hello World", converter.Convert(converter.Convert("Hello World")));
        }

        // Sentence and Invert are the two branches that use the instance's cached StringBuilder. Two
        // calls on one converter must not concatenate.
        [TestCase(TextCase.Sentence, "hello", "Hello", "world", "World")]
        [TestCase(TextCase.Invert, "abc", "ABC", "def", "DEF")]
        public void TextCase_ReusingOneInstanceDoesNotAccumulate(
            TextCase textCase, string first, string firstExpected, string second, string secondExpected)
        {
            var converter = Case(textCase);

            Assert.AreEqual(firstExpected, converter.Convert(first));
            Assert.AreEqual(secondExpected, converter.Convert(second), "the cached builder leaked the previous call");
        }

        [TestCase(TextCase.Sentence)]
        [TestCase(TextCase.Invert)]
        public void TextCase_BlankIsReturnedUnchanged(TextCase textCase)
        {
            Assert.IsNull(Case(textCase).Convert(null));
            Assert.AreEqual(string.Empty, Case(textCase).Convert(string.Empty));
        }

        [Test]
        public void TextCase_UndeclaredCase_ReportsAndReturnsTheStringUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("TextCaseConverter.*not a declared TextCase"));

            Assert.AreEqual("hELLo", Case((TextCase)42).Convert("hELLo"));
        }

        [Test]
        public void TextCase_UndeclaredCase_BlankIsReturnedWithoutReporting() =>
            Assert.AreEqual(string.Empty, Case((TextCase)42).Convert(string.Empty));

        private static TextCaseConverter Case(TextCase textCase) =>
            new TextCaseConverter(textCase, CultureInfoMode.InvariantCulture);
    }
}
