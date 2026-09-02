using System;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToBoolConverter"/> — the built-in spellings and authored
    /// alternatives.
    /// </summary>
    [TestFixture]
    public sealed class StringToBoolConverterTests
    {
        [TestCase("true", true)]
        [TestCase("TRUE", true)]
        [TestCase("1", true)]
        [TestCase("yes", true)]
        [TestCase("on", true)]
        [TestCase("false", false)]
        [TestCase("0", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void Convert_ReadsTheUsualSpellings(string value, bool expected) =>
            Assert.AreEqual(expected, new StringToBoolConverter().Convert(value));

        [Test]
        public void Convert_TakesAuthoredSpellings() =>
            Assert.IsTrue(new StringToBoolConverter(new[] { "oui" }).Convert("OUI"));

        // A parse converter writes the first spelling authored for the answer, so a project reading
        // "oui"/"non" pushes those words back rather than the framework's own.
        [Test]
        public void ConvertBack_WritesTheFirstAuthoredSpelling()
        {
            var converter = new StringToBoolConverter(new[] { "oui", "1" }, new[] { "non", "0" });

            Assert.AreEqual("oui", converter.ConvertBack(true));
            Assert.AreEqual("non", converter.ConvertBack(false));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ConvertBack_RoundTripsBothAnswers(bool value)
        {
            var converter = new StringToBoolConverter(new[] { "oui", "1" }, new[] { "non", "0" });

            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)));
        }

        // Without false spellings authored there is no word to pick, and "not true" is the definition
        // of false — so the plain word goes back, and the same rule reads it as false again.
        [Test]
        public void ConvertBack_WithoutFalseSpellings_WritesThePlainWord()
        {
            var converter = new StringToBoolConverter(new[] { "yes" });

            Assert.AreEqual("false", converter.ConvertBack(false));
            Assert.IsFalse(converter.Convert(converter.ConvertBack(false)));
        }

        [Test]
        public void Convert_WithNoFalseSpellings_TreatsAnythingUnmatchedAsFalse()
        {
            // Nothing to report: without a false list, "not true" is the definition of false.
            Assert.IsFalse(new StringToBoolConverter(new[] { "yes" }).Convert("banana"));
        }

        [Test]
        public void Convert_WithFalseSpellings_ReportsTextMatchingNeither()
        {
            var converter = new StringToBoolConverter(new[] { "yes" }, new[] { "no" });
            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*a boolean spelling"));

            Assert.IsFalse(converter.Convert("banana"));
            Assert.IsTrue(converter.Convert("yes"), "a matching spelling still reads normally");
            Assert.IsFalse(converter.Convert("no"));
        }

        // The Inspector can clear the list, which leaves a converter no text can ever read as true.
        // Blank text still takes the fallback quietly: that is an unfilled field, not the mistake.
        [Test]
        public void Convert_WithNoTrueSpellings_ReportsEveryPushAndTakesTheFallback()
        {
            var converter = new StringToBoolConverter(new[] { "yes" });
            SetField(converter, "_trueTokens", Array.Empty<string>());

            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*read as true is empty"));
            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*read as true is empty"));

            Assert.IsFalse(converter.Convert("yes"));
            Assert.IsFalse(converter.Convert("no"));
            Assert.IsFalse(converter.Convert(string.Empty));
        }

        // The reverse direction has one configuration it cannot honour: with no false spellings, the
        // word written for false is read back through the fallback, and a true fallback turns it into
        // the opposite answer. Reported rather than left to present as a toggle that will not turn off.
        [Test]
        public void ConvertBack_TrueFallbackWithNoFalseSpellings_ReportsWhatComesBack()
        {
            var converter = new StringToBoolConverter(new[] { "yes" }, falseTokens: null, fallback: true);

            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*fallback is true"));

            Assert.AreEqual("false", converter.ConvertBack(false));
            Assert.IsTrue(converter.Convert("false"), "which is the reading the message warns about");
        }

        // The cleared list Convert already reports leaves the reverse direction nothing to write
        // either, so it says so instead of pushing a spelling the converter would not read as true.
        [Test]
        public void ConvertBack_WithNoTrueSpellings_ReportsWhatItWritesBack()
        {
            var converter = new StringToBoolConverter(new[] { "yes" });
            SetField(converter, "_trueTokens", Array.Empty<string>());

            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*read as true is empty"));

            Assert.AreEqual("true", converter.ConvertBack(true));
        }

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field!.SetValue(target, value);
        }
    }
}
