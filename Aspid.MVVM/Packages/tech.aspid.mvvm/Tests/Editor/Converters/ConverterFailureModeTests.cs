using System;
using NUnit.Framework;
using System.Reflection;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// The parse converters answer a value they cannot read according to <see cref="ConverterFailureMode"/>.
    /// </summary>
    /// <remarks>
    /// A fallback of zero is indistinguishable from text that legitimately read as zero, so a mistyped
    /// field or a locale mismatch used to present as a value rather than as a problem.
    /// <para>
    /// Only the parse family took the mode: a converter whose fallback is its purpose has no failure to
    /// report.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ConverterFailureModeTests
    {
        [Test]
        public void StringToInt_Unparsed_ReturnsFallbackAndReportsOnce()
        {
            var converter = new StringToIntConverter(fallback: -1);
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter.*a whole number"));

            Assert.AreEqual(-1, converter.Convert("not a number"));
            // The second failure must not log: a binder pushes on every notification, and
            // Debug.LogError captures a stack trace.
            Assert.AreEqual(-1, converter.Convert("still not a number"));
        }

        [Test]
        public void StringToInt_Throw_RaisesFormatException()
        {
            var converter = new StringToIntConverter(fallback: 0);
            SetFailureMode(converter, ConverterFailureMode.Throw);

            Assert.Throws<FormatException>(() => converter.Convert("nope"));
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
            var converter = new StringToEnumConverter<Comparisons>(Comparisons.Equal);
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter.*Comparisons"));

            Assert.AreEqual(Comparisons.Equal, converter.Convert("NotAMember"));
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
        public void StringToBoolParse_WithNoFalseSpellings_TreatsAnythingUnmatchedAsFalse()
        {
            // Nothing to report: without a false list, "not true" is the definition of false.
            Assert.IsFalse(new StringToBoolParseConverter(new[] { "yes" }).Convert("banana"));
        }

        [Test]
        public void StringToBoolParse_WithFalseSpellings_ReportsTextMatchingNeither()
        {
            var converter = new StringToBoolParseConverter(new[] { "yes" });
            SetField(converter, "_falseTokens", new[] { "no" });
            LogAssert.Expect(LogType.Error, new Regex("StringToBoolParseConverter.*a boolean spelling"));

            Assert.IsFalse(converter.Convert("banana"));
            Assert.IsTrue(converter.Convert("yes"), "a matching spelling still reads normally");
            Assert.IsFalse(converter.Convert("no"));
        }

        private static void SetFailureMode(object converter, ConverterFailureMode mode) =>
            SetField(converter, "_onFailure", mode);

        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field!.SetValue(target, value);
        }
    }
}
