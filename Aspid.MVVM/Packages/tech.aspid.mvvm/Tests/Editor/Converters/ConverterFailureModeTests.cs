using System;
using NUnit.Framework;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The parse converters report a value they cannot read and answer it with their fallback.
    /// </summary>
    /// <remarks>
    /// A fallback of zero is indistinguishable from text that legitimately read as zero, so a mistyped
    /// field or a locale mismatch used to present as a value rather than as a problem.
    /// <para>
    /// The tail of the fixture covers the other half of the same rule — Inspector state that cannot
    /// work at all. It is reported on every push rather than once per session, and answered with a
    /// documented value.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterFailureModeTests
    {
        [Test]
        public void StringToInt_Unparsed_ReturnsFallbackAndReportsEveryFailure()
        {
            var converter = new StringToIntConverter(fallback: -1);

            // Every failure is reported. A value that stops converting halfway through a session is
            // the case a report-once rule hides, and a console line is cheaper than that.
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));

            Assert.AreEqual(-1, converter.Convert("not a number"));
            Assert.AreEqual(-1, converter.Convert("still not a number"));
        }

        // A value matching neither branch cannot be read back, so the authored fallback answers.
        [Test]
        public void BoolToValue_ValueMatchingNeitherBranch_ReportsAndUsesTheFallback()
        {
            var converter = new BoolToValueConverter<object>("a", "b", convertBackFallback: true);

            LogAssert.Expect(LogType.Error, new Regex("BoolToValueConverter<object>.*Using the fallback"));

            Assert.IsTrue(converter.ConvertBack("neither"));
        }

        // BoolLogicConverter joined the failure-mode family with ReturnInput as its authored
        // default, so an irreversible operation still passes the combined value back unchanged
        // out of the box — the behavior it had before the mode existed.
        [Test]
        public void BoolLogic_IrreversibleByDefault_ReturnsTheInputUnchanged()
        {
            var converter = new BoolLogicConverter(LogicOperation.Or, operand: true);

            LogAssert.Expect(LogType.Error, new Regex("BoolLogicConverter.*Returning the input unchanged"));

            Assert.IsTrue(converter.ConvertBack(true));
        }

        // An undeclared operation — corrupted YAML or a stray cast — answers through the same
        // mode instead of throwing unconditionally.
        [Test]
        public void BoolLogic_UndeclaredOperation_AnswersThroughTheMode()
        {
            var converter = new BoolLogicConverter((LogicOperation)999, operand: false);

            LogAssert.Expect(LogType.Error, new Regex("BoolLogicConverter.*not a declared LogicOperation"));

            Assert.IsTrue(converter.Convert(true), "ReturnInput passes the bound value through");
        }

        [Test]
        public void StringToFloat_Unparsed_ReturnsFallbackAndReports()
        {
            var converter = new StringToFloatConverter(fallback: 2.5f);
            LogAssert.Expect(LogType.Error, new Regex("StringToFloatConverter.*a decimal number"));

            Assert.AreEqual(2.5f, converter.Convert("abc"));
        }

        [Test]
        public void StringToEnum_Unparsed_ReturnsFallbackAndNamesTheEnum()
        {
            var converter = new StringToEnumConverter<ComparisonMode>(ComparisonMode.Equal);
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter.*ComparisonMode"));

            Assert.AreEqual(ComparisonMode.Equal, converter.Convert("NotAMember"));
        }

        [Test]
        public void StringToDateTime_Unparsed_ReturnsFallbackAndReports()
        {
            var fallback = new DateTime(2000, 1, 1);
            var converter = new StringToDateTimeConverter(format: string.Empty, fallback);
            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter.*a date"));

            Assert.AreEqual(fallback, converter.Convert("not a date"));
        }

        // Empty text means the field was left blank, which is absence rather than a malformed value.
        // Reporting it would fire on every scene that has an unfilled input.
        [Test]
        public void StringToDateTime_Empty_TakesTheFallbackQuietly()
        {
            var fallback = new DateTime(2000, 1, 1);
            Assert.AreEqual(fallback, new StringToDateTimeConverter(format: string.Empty, fallback).Convert(""));
        }

        [Test]
        public void StringToBool_WithNoFalseSpellings_TreatsAnythingUnmatchedAsFalse()
        {
            // Nothing to report: without a false list, "not true" is the definition of false.
            Assert.IsFalse(new StringToBoolConverter(new[] { "yes" }).Convert("banana"));
        }

        [Test]
        public void StringToBool_WithFalseSpellings_ReportsTextMatchingNeither()
        {
            var converter = new StringToBoolConverter(new[] { "yes" }, new[] { "no" });
            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*a boolean spelling"));

            Assert.IsFalse(converter.Convert("banana"));
            Assert.IsTrue(converter.Convert("yes"), "a matching spelling still reads normally");
            Assert.IsFalse(converter.Convert("no"));
        }

        // A tick count an Inspector long is free to hold but the calendar is not. It is read only on
        // the path that reaches for the fallback, so a converter whose text parses never mentions it.
        [Test]
        public void StringToDateTime_FallbackTicksOutsideTheCalendar_AreReportedAndPinned()
        {
            var converter = new StringToDateTimeConverter(format: string.Empty);
            SetField(converter, "_fallbackTicks", -1L);

            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter.*outside the range"));

            Assert.AreEqual(DateTime.MinValue, converter.Convert(string.Empty));
        }

        [Test]
        public void StringToDateTime_FallbackTicksInsideTheCalendar_AreLeftAlone()
        {
            var fallback = new DateTime(2000, 1, 1);

            Assert.AreEqual(fallback, new StringToDateTimeConverter(string.Empty, fallback).Convert(string.Empty));
            LogAssert.NoUnexpectedReceived();
        }

        // The Inspector can clear the list, which leaves a converter no text can ever read as true.
        // Blank text still takes the fallback quietly: that is an unfilled field, not the mistake.
        [Test]
        public void StringToBool_WithNoTrueSpellings_ReportsEveryPushAndTakesTheFallback()
        {
            var converter = new StringToBoolConverter(new[] { "yes" });
            SetField(converter, "_trueTokens", Array.Empty<string>());

            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*read as true is empty"));
            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*read as true is empty"));

            Assert.IsFalse(converter.Convert("yes"));
            Assert.IsFalse(converter.Convert("no"));
            Assert.IsFalse(converter.Convert(string.Empty));
        }

        // Contains, StartsWith and EndsWith all answer true for an empty needle, so an unfilled field
        // would present as a converter that is always on rather than as one nobody finished authoring.
        [Test]
        public void StringMatch_BlankText_ReportsEveryPushAndAnswersFalse()
        {
            var converter = new StringMatchToBoolConverter(StringMatch.Contains, string.Empty);

            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));
            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));

            Assert.IsFalse(converter.Convert("abc"));
            Assert.IsFalse(converter.Convert(null));
        }

        // The answer is the documented fallback rather than the result of a comparison, so inversion
        // has nothing to invert.
        [Test]
        public void StringMatch_BlankText_IsNotInverted()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringMatchToBoolConverter.*blank"));

            Assert.IsFalse(
                new StringMatchToBoolConverter(StringMatch.Equals, string.Empty, isInvert: true).Convert("abc"));
        }

        // PadLeft and PadRight throw on a negative width, and the field is an Inspector int with
        // nothing to stop one being typed.
        [Test]
        public void Pad_NegativeWidth_ReportsEveryPushAndLeavesTheStringAlone()
        {
            var converter = new PadStringConverter(-4);

            LogAssert.Expect(LogType.Error, new Regex("PadStringConverter.*negative"));
            LogAssert.Expect(LogType.Error, new Regex("PadStringConverter.*negative"));

            Assert.AreEqual("abc", converter.Convert("abc"));
            Assert.AreEqual("xy", converter.Convert("xy"));
            Assert.IsNull(converter.Convert(null), "a null value never reaches the width at all");
        }

        // The reverse direction has one configuration it cannot honour: with no false spellings, the
        // word written for false is read back through the fallback, and a true fallback turns it into
        // the opposite answer. Reported rather than left to present as a toggle that will not turn off.
        [Test]
        public void StringToBool_TrueFallbackWithNoFalseSpellings_ReportsWhatComesBack()
        {
            var converter = new StringToBoolConverter(new[] { "yes" }, falseTokens: null, fallback: true);

            LogAssert.Expect(LogType.Error, new Regex("StringToBoolConverter.*fallback is true"));

            Assert.AreEqual("false", converter.ConvertBack(false));
            Assert.IsTrue(converter.Convert("false"), "which is the reading the message warns about");
        }

        // The cleared list Convert already reports leaves the reverse direction nothing to write
        // either, so it says so instead of pushing a spelling the converter would not read as true.
        [Test]
        public void StringToBool_WithNoTrueSpellings_ReportsWhatItWritesBack()
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
