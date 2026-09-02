using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToDecimalConverter"/> — exponents, the text-authored fallback
    /// and clamp bounds, and the round trip under a comma-decimal culture.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToDecimalConverterTests
    {
        // AllowExponent on top of Number: a backend that serializes 1E5 would otherwise be readable
        // by the converter with less precision and not by the exact one.
        [TestCase("1E5", 100000)]
        [TestCase("1.5E2", 150)]
        [TestCase("-2e3", -2000)]
        public void Convert_ReadsAnExponent(string value, int expected) =>
            Assert.AreEqual((decimal)expected, new StringToDecimalConverter().Convert(value));

        [Test]
        public void Convert_ReadsAFractionalExponent() =>
            Assert.AreEqual(0.02m, new StringToDecimalConverter().Convert("2E-2"));

        // NumberStyles.Number carries AllowThousands, so the player-facing read matches the rest of
        // the family even though the fallback below refuses the same character.
        [Test]
        public void Convert_ReadsGroupedText() =>
            Assert.AreEqual(1000.5m, new StringToDecimalConverter().Convert("1,000.5"));

        // Eighteen significant digits: the reason to reach for this over the double converter.
        [Test]
        public void Convert_KeepsDigitsADoubleWouldRound() =>
            Assert.AreEqual(1234567890.12345678m, new StringToDecimalConverter().Convert("1234567890.12345678"));

        // decimal.MaxValue + 1. Out of range is a parse failure like any other rather than an
        // OverflowException escaping the push.
        [Test]
        public void Convert_OutOfRangeText_FallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter"));

            Assert.AreEqual(
                decimal.Zero,
                new StringToDecimalConverter().Convert("79228162514264337593543950336"));
        }

        // The fallback is authored text read as invariant, where the comma is the GROUP separator:
        // read with NumberStyles.Number it would come back as fifteen — ten times the value the
        // author meant, silently, for every player. Float refuses it and says so instead.
        [Test]
        public void Convert_CommaInTheFallback_IsRefusedRatherThanReadAsFifteen()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter"));

            var converter = With(new StringToDecimalConverter(), "_fallback", "1,5");

            Assert.AreEqual(decimal.Zero, converter.Convert(null));
        }

        // The fallback is authored once and the player's text is typed every time, so the two are
        // read with different cultures on purpose: "1.5" in the field, "1,5" from a German player.
        [Test]
        [SetCulture("de-DE")]
        public void Convert_Fallback_IsInvariantWhilePlayerTextIsNot()
        {
            var converter = With(new StringToDecimalConverter(), "_fallback", "1.5");

            Assert.AreEqual(1.5m, converter.Convert(null));
            Assert.AreEqual(1.5m, converter.Convert("1,5"));
            LogAssert.NoUnexpectedReceived();
        }

        // The parsed fallback is cached against the string it came from by reference, so a field
        // edited while the game runs must not keep serving the old reading.
        [Test]
        public void Convert_Fallback_IsRereadWhenTheFieldChanges()
        {
            var converter = With(new StringToDecimalConverter(), "_fallback", "1.5");
            Assert.AreEqual(1.5m, converter.Convert(null));

            With(converter, "_fallback", "2.5");
            Assert.AreEqual(2.5m, converter.Convert(null));
        }

        [Test]
        [SetCulture("de-DE")]
        public void Convert_RoundTripsUnderACommaDecimalCulture()
        {
            var converter = new StringToDecimalConverter(0m);

            var text = converter.ConvertBack(1.5m);

            Assert.AreEqual("1,5", text);
            Assert.AreEqual(1.5m, converter.Convert(text));
        }

        // The bounds are authored as text for the same reason the fallback is: Unity cannot serialize
        // a decimal field, and rounding them through a double would defeat the converter's purpose.
        [TestCase("42", 10)]
        [TestCase("-1", 0)]
        [TestCase("5", 5)]
        public void Convert_Clamp_HoldsTheResultInsideTheBounds(string value, int expected) =>
            Assert.AreEqual((decimal)expected, ClampedDecimal("0", "10").Convert(value));

        // A bound left blank is no bound at all: an author who fills in one end has not asked for the
        // other to snap to zero.
        [TestCase("42", 42)]
        [TestCase("-1", 0)]
        public void Convert_Clamp_ABlankBoundIsNoBound(string value, int expected) =>
            Assert.AreEqual((decimal)expected, ClampedDecimal("0", string.Empty).Convert(value));

        // The clamp sits after the parse, so the fallback is returned raw — the same bargain the
        // double converter makes.
        [Test]
        public void Convert_Clamp_DoesNotApplyToTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter"));

            Assert.AreEqual(decimal.Zero, ClampedDecimal("5", "10").Convert("abc"));
        }

        // A bound is authored state rather than a bound value, so a mistyped one is reported and the
        // end it names is dropped instead of the whole push failing.
        [Test]
        public void Convert_Clamp_AMalformedBoundIsReportedAndIgnored()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter.*the highest value"));

            Assert.AreEqual(42m, ClampedDecimal("0", "1,5").Convert("42"));
        }

        // The decimal bounds are authored as text rather than as numbers, since Unity cannot
        // serialize a decimal field.
        private static StringToDecimalConverter ClampedDecimal(string min, string max)
        {
            var converter = new StringToDecimalConverter();

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
