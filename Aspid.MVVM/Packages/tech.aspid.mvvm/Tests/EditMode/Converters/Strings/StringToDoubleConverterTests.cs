using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToDoubleConverter"/> — reading, the clamp bounds, the failure
    /// modes and the round trip under a comma-decimal culture.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToDoubleConverterTests
    {
        // NumberStyles.Float carries AllowExponent and the surrounding whitespace, and the family
        // adds AllowThousands on top: "1,000.5" is a spelling of a number rather than a mistake.
        [TestCase("1.5", 1.5d)]
        [TestCase("-0.25", -0.25d)]
        [TestCase("1E5", 100000d)]
        [TestCase("-1.5e-2", -0.015d)]
        [TestCase("1,000.5", 1000.5d)]
        [TestCase("  2.5  ", 2.5d)]
        public void Convert_ReadsTheNumber(string value, double expected) =>
            Assert.AreEqual(expected, new StringToDoubleConverter().Convert(value), delta: 1e-12);

        // Blank text is an unfilled field rather than a malformed number, so it must not reach the
        // console — an empty input in a scene would otherwise report on the first push.
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Convert_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(7.5d, new StringToDoubleConverter(7.5d).Convert(value), delta: 1e-12);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void Convert_UnreadableText_FallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            Assert.AreEqual(7.5d, new StringToDoubleConverter(7.5d).Convert("abc"), delta: 1e-12);
        }

        // Every push that fails is reported. Text that only starts failing once a locale changes, or
        // once a backend answers differently, is the case a report-once rule would hide.
        [Test]
        public void Convert_UnreadableText_ReportsEveryPush()
        {
            for (var i = 0; i < 3; i++)
                LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            var converter = new StringToDoubleConverter();
            converter.Convert("abc");
            converter.Convert("abc");
            converter.Convert("still not a number");
        }

        // 2^24 + 1 is the smallest whole number a float cannot hold; it is the reason the double
        // converter exists next to the float one.
        [Test]
        public void Convert_KeepsAWholeNumberTheFloatSiblingRounds()
        {
            Assert.AreEqual(16777217d, new StringToDoubleConverter().Convert("16777217"), delta: 0d);
            Assert.AreEqual(16777216f, new StringToFloatConverter().Convert("16777217"));
        }

        [TestCase("42", 10d)]
        [TestCase("-1", 0d)]
        [TestCase("5", 5d)]
        public void Convert_Clamp_HoldsTheResultInsideTheBounds(string value, double expected) =>
            Assert.AreEqual(
                expected,
                Clamped(new StringToDoubleConverter(), 0d, 10d).Convert(value),
                delta: 1e-12);

        // Math.Clamp throws when max is authored below min, and these are Inspector fields with
        // nothing to validate them. A reversed pair never lets a value through: below min it snaps
        // to max, and anything from min upward is pinned to max as well.
        [TestCase("5", 10d)]
        [TestCase("0", 10d)]
        [TestCase("10", 0d)]
        [TestCase("20", 0d)]
        public void Convert_ReversedClampBounds_PinInsteadOfThrowing(string value, double expected) =>
            Assert.AreEqual(
                expected,
                Clamped(new StringToDoubleConverter(), 10d, 0d).Convert(value),
                delta: 1e-12);

        // The clamp sits after the parse, so the fallback is returned raw: a fallback authored
        // outside the bounds comes out outside them.
        [Test]
        public void Convert_Clamp_DoesNotApplyToTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            Assert.AreEqual(0d, Clamped(new StringToDoubleConverter(), 5d, 10d).Convert("abc"), delta: 1e-12);
        }

        // NaN fails both comparisons, so clamping does not fence it in — the bounds hold numbers,
        // not everything a double can be.
        [Test]
        public void Convert_Clamp_LetsNaNThrough() =>
            Assert.IsTrue(double.IsNaN(Clamped(new StringToDoubleConverter(), 0d, 1d).Convert("NaN")));

        // Both halves resolve the culture at call time from the same field, so the pair agrees on a
        // device that writes one and a half as "1,5".
        [Test]
        [SetCulture("de-DE")]
        public void Convert_RoundTripsUnderACommaDecimalCulture()
        {
            var converter = new StringToDoubleConverter(0d);

            var text = converter.ConvertBack(1.5d);

            Assert.AreEqual("1,5", text);
            Assert.AreEqual(1.5d, converter.Convert(text), delta: 1e-12);
        }

        // ConvertBack writes the round-trip format rather than the default one. The default stops at
        // fifteen significant digits for a double, so a value pushed through an input field and back
        // would drift a little further on every trip.
        [Test]
        public void ConvertBack_KeepsEveryDigitTheDefaultFormatWouldDrop()
        {
            const double value = 0.1d + 0.2d;
            var converter = new StringToDoubleConverter();

            Assert.AreNotEqual(value.ToString(CultureInfo.InvariantCulture), converter.ConvertBack(value));
            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)), delta: 0d);
        }

        private static T Clamped<T>(T converter, object min, object max)
            where T : class
        {
            With(converter, "_clamp", true);
            With(converter, "_min", min);
            With(converter, "_max", max);

            return converter;
        }

        private static T With<T>(T converter, string field, object value)
            where T : class
        {
            var info = converter.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"{converter.GetType().Name} has no field {field}");

            info.SetValue(converter, value);

            if (converter is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();

            return converter;
        }
    }
}
