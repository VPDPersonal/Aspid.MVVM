using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the shipped <see cref="PluralRule"/> grammars, and for the zero word and the
    /// missing-word report they all share.
    /// </summary>
    /// <remarks>
    /// Each grammar is pinned on the counts that tell it apart from its neighbours rather than on a
    /// sweep: the teens, the counts past a hundred, and the last digit above twenty are where a rule
    /// rewritten from the last digit alone goes wrong.
    /// </remarks>
    [TestFixture]
    internal sealed class PluralRuleTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(11)]
        public void SingleForm_WordsEveryCountTheSame(long count) =>
            Assert.AreEqual("item", new SingleFormPluralRule("item").Convert(count));

        [TestCase(1, "apple")]
        [TestCase(0, "apples")]
        [TestCase(2, "apples")]
        [TestCase(21, "apples")]
        public void English_OnlyOneIsSingular(long count, string expected) =>
            Assert.AreEqual(expected, new EnglishPluralRule("apple", "apples").Convert(count));

        // Zero shares the singular in French, which is the whole difference from English.
        [TestCase(0, "pomme")]
        [TestCase(1, "pomme")]
        [TestCase(2, "pommes")]
        public void French_ZeroSharesTheSingular(long count, string expected) =>
            Assert.AreEqual(expected, new FrenchPluralRule("pomme", "pommes").Convert(count));

        [TestCase(1, "яблоко")]
        [TestCase(21, "яблоко")]
        [TestCase(101, "яблоко")]
        [TestCase(2, "яблока")]
        [TestCase(4, "яблока")]
        [TestCase(22, "яблока")]
        [TestCase(104, "яблока")]
        [TestCase(0, "яблок")]
        [TestCase(5, "яблок")]
        // The teens take the many form whatever their last digit, and the window is found on the last
        // two digits — so 111 is a teen and 101 is not.
        [TestCase(11, "яблок")]
        [TestCase(12, "яблок")]
        [TestCase(14, "яблок")]
        [TestCase(111, "яблок")]
        public void EastSlavic_ReadsTheLastDigitExceptInTheTeens(long count, string expected) =>
            Assert.AreEqual(expected, Russian().Convert(count));

        // The difference from East Slavic, and the reason Polish is a rule of its own: the singular is
        // claimed by the count one alone, so 21 is many here and one in Russian.
        [TestCase(1, "plik")]
        [TestCase(21, "plików")]
        [TestCase(101, "plików")]
        [TestCase(2, "pliki")]
        [TestCase(4, "pliki")]
        [TestCase(22, "pliki")]
        [TestCase(0, "plików")]
        [TestCase(5, "plików")]
        [TestCase(12, "plików")]
        [TestCase(14, "plików")]
        public void Polish_OnlyABareOneIsSingular(long count, string expected) =>
            Assert.AreEqual(expected, new PolishPluralRule("plik", "pliki", "plików").Convert(count));

        // Czech reads the count itself rather than its last digit, so 22 takes the same word as 5.
        [TestCase(1, "soubor")]
        [TestCase(2, "soubory")]
        [TestCase(4, "soubory")]
        [TestCase(0, "souborů")]
        [TestCase(5, "souborů")]
        [TestCase(22, "souborů")]
        public void Czech_ReadsTheCountItself(long count, string expected) =>
            Assert.AreEqual(expected, new CzechPluralRule("soubor", "soubory", "souborů").Convert(count));

        [TestCase(0, "zero")]
        [TestCase(1, "one")]
        [TestCase(2, "two")]
        [TestCase(3, "few")]
        [TestCase(10, "few")]
        [TestCase(103, "few")]
        [TestCase(110, "few")]
        [TestCase(11, "many")]
        [TestCase(99, "many")]
        [TestCase(111, "many")]
        // The round hundreds and the two counts after each fall outside every window above.
        [TestCase(100, "other")]
        [TestCase(101, "other")]
        [TestCase(102, "other")]
        public void Arabic_UsesAllSixWords(long count, string expected) =>
            Assert.AreEqual(expected, Arabic().Convert(count));

        // With no zero word of its own the Arabic grammar lands zero on the round-hundreds word, since
        // zero is a round hundred as far as the last two digits are concerned.
        [Test]
        public void Arabic_NoZeroWord_ZeroTakesTheOtherWord() =>
            Assert.AreEqual(
                "other",
                new ArabicPluralRule("one", "two", "few", "many", "other").Convert(0));

        // ---------------------------------------------------------------------------------------
        // The zero word and the missing-word report, shared by every grammar
        // ---------------------------------------------------------------------------------------

        // Zero is a category only a few grammars declare, so a word authored for it would be
        // unreachable under English — the base class hands it zero regardless.
        [Test]
        public void ZeroWord_TakesZeroWhateverTheGrammarSays() =>
            Assert.AreEqual(
                "no apples",
                new EnglishPluralRule("apple", "apples", zero: "no apples").Convert(0));

        [Test]
        public void ZeroWord_LeavesEveryOtherCountAlone() =>
            Assert.AreEqual(
                "apple",
                new EnglishPluralRule("apple", "apples", zero: "no apples").Convert(1));

        // A grammar reached for a word its Inspector does not carry would write a phrase with a hole
        // where the noun belongs — still a string, and so still looking like a conversion.
        [Test]
        public void UnauthoredWord_ReportsItAndReturnsNothing()
        {
            ExpectMissingWordError();

            Assert.AreEqual(string.Empty, new EnglishPluralRule("apple", string.Empty).Convert(2));
        }

        // The word is a field, not a value, so every push hits it and every push has to say so.
        [Test]
        public void UnauthoredWord_ReportsItOnEveryPush()
        {
            var rule = new EnglishPluralRule("apple", string.Empty);

            ExpectMissingWordError();
            ExpectMissingWordError();

            rule.Convert(2);
            rule.Convert(2);
        }

        // Only the word the grammar reached for matters: the empty few form of a Slavic rule is silent
        // until a count in the 2-4 window arrives.
        [Test]
        public void UnauthoredWord_TheGrammarNeverReachedFor_IsSilent() =>
            Assert.AreEqual("яблоко", new EastSlavicPluralRule("яблоко", string.Empty, "яблок").Convert(1));

        private static void ExpectMissingWordError() =>
            LogAssert.Expect(LogType.Error, new Regex("PluralRule.*no word is authored"));

        private static EastSlavicPluralRule Russian() => new("яблоко", "яблока", "яблок");

        private static ArabicPluralRule Arabic() => new("one", "two", "few", "many", "other", zero: "zero");
    }
}
