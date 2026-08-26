using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for what <see cref="PluralizeConverter"/> keeps once the grammar is a
    /// <see cref="PluralRule"/> of its own: the phrase, the sign, and the report for an unset rule.
    /// </summary>
    /// <remarks>
    /// The grammars are covered by <see cref="PluralRuleTests"/>, so the rule here is a spy whenever
    /// the assertion is about the converter rather than about a language.
    /// </remarks>
    [TestFixture]
    internal sealed class PluralizeConverterTests
    {
        [TestCase(1, "1 apple")]
        [TestCase(2, "2 apples")]
        [TestCase(0, "0 apples")]
        public void Convert_WritesTheCountAndTheWordTheGrammarPicked(int value, string expected) =>
            Assert.AreEqual(expected, English().Convert(value));

        // The count keeps its sign in the phrase while the grammar is asked about the magnitude, so a
        // negative count is worded rather than falling through to the plural.
        [Test]
        public void Convert_NegativeCount_KeepsTheSignAndReadsTheMagnitude()
        {
            var rule = new Spy();

            Assert.AreEqual("-1 word", new PluralizeConverter(rule).Convert(-1));
            Assert.AreEqual(1L, rule.Received);
        }

        // Math.Abs on int.MinValue throws, which is why the magnitude is taken as a long.
        [Test]
        public void Convert_IntMinValue_DoesNotThrow()
        {
            var rule = new Spy();

            Assert.AreEqual($"{int.MinValue} word", new PluralizeConverter(rule).Convert(int.MinValue));
            Assert.AreEqual(2147483648L, rule.Received);
        }

        // An empty picker is what an Inspector-authored converter looks like before a grammar is
        // chosen, and it has no word to write — so it says so rather than writing the count alone in
        // silence.
        [Test]
        public void Convert_NoRule_ReportsItAndLeavesTheWordOut()
        {
            var converter = English();
            SetField(converter, "_rule", null);

            LogAssert.Expect(LogType.Error, new Regex("PluralizeConverter.*no plural rule is set"));

            Assert.AreEqual("1 ", converter.Convert(1));
        }

        // A typo in the Inspector used to throw out of the binder and cut every subscriber queued
        // behind it.
        [Test]
        public void Convert_InvalidFormat_ReportsItAndWritesTheWordAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("PluralizeConverter.*not a composite format"));

            Assert.AreEqual("apples", English("{0").Convert(2));
        }

        [Test]
        public void Convert_InvalidFormat_ReportsItOnEveryPush()
        {
            var converter = English("{0");

            LogAssert.Expect(LogType.Error, new Regex("not a composite format"));
            LogAssert.Expect(LogType.Error, new Regex("not a composite format"));

            converter.Convert(2);
            converter.Convert(2);
        }

        // An empty format is authored, not broken: the word is written on its own and nothing is said.
        [Test]
        public void Convert_EmptyFormat_WritesTheWordAlone() =>
            Assert.AreEqual("apples", English(string.Empty).Convert(2));

        [Test]
        public void Convert_FormatWithoutTheCount_WritesTheWordAlone() =>
            Assert.AreEqual("apples!", English("{1}!").Convert(2));

        private static PluralizeConverter English(string? format = null) =>
            new(new EnglishPluralRule("apple", "apples"), format);

        private static void SetField(object target, string name, object? value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");

            field!.SetValue(target, value);
        }

        // A grammar with no grammar in it, which also records what the converter handed it.
        private sealed class Spy : PluralRule
        {
            public long Received { get; private set; }

            protected override string Word(long value)
            {
                Received = value;
                return "word";
            }
        }
    }
}
