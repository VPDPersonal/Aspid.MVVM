using System;
using UnityEngine;
using NUnit.Framework;
using System.Threading;
using System.Reflection;
using System.Globalization;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the parsing converters added after <see cref="ParseConverterTests"/> —
    /// <see cref="StringToDoubleConverter"/>, <see cref="StringToDecimalConverter"/>,
    /// <see cref="StringToTimeSpanConverter"/>, <see cref="StringToVector2Converter"/> and
    /// <see cref="StringToVector3Converter"/> — plus the clamp bounds every numeric parser shares.
    /// </summary>
    /// <remarks>
    /// The mistakes guarded against are the ones that only appear on somebody else's machine: a decimal
    /// separator colliding with the separator between vector components, a fallback authored under one
    /// locale and read under another, a pair of clamp bounds an author can transpose.
    /// <para>
    /// The bounds, the failure mode and the decimal fallback are Inspector state with no constructor
    /// overload, so they are set through <see cref="With{T}"/>; the thread culture is pinned to invariant
    /// per test, because these converters resolve their culture at call time.
    /// </para>
    /// <para>
    /// Expectations come from running the implementation. One contradicts the docs: a reversed clamp pair
    /// reads as the minimum only below the minimum and as the maximum from there up, so it lets nothing
    /// through unchanged.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ParseAdditionsTests
    {
        private CultureInfo _previous;

        [SetUp]
        public void UseInvariantCulture()
        {
            _previous = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [TearDown]
        public void RestoreCulture() =>
            Thread.CurrentThread.CurrentCulture = _previous;

        #region StringToDoubleConverter — reading, clamping and the failure modes

        // NumberStyles.Float carries AllowExponent and the surrounding whitespace, and the family
        // adds AllowThousands on top: "1,000.5" is a spelling of a number rather than a mistake.
        [TestCase("1.5", 1.5d)]
        [TestCase("-0.25", -0.25d)]
        [TestCase("1E5", 100000d)]
        [TestCase("-1.5e-2", -0.015d)]
        [TestCase("1,000.5", 1000.5d)]
        [TestCase("  2.5  ", 2.5d)]
        public void StringToDouble_ReadsTheNumber(string value, double expected) =>
            Assert.AreEqual(expected, new StringToDoubleConverter().Convert(value), delta: 1e-12);

        // Blank text is an unfilled field rather than a malformed number, so it must not reach the
        // console — an empty input in a scene would otherwise report on the first push.
        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void StringToDouble_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(7.5d, new StringToDoubleConverter(7.5d).Convert(value), delta: 1e-12);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void StringToDouble_UnreadableText_FallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            Assert.AreEqual(7.5d, new StringToDoubleConverter(7.5d).Convert("abc"), delta: 1e-12);
        }

        // A binder pushes on every notification, not on every change, and Debug.LogError captures a
        // stack trace: a field left holding bad text would cost a trace per push.
        [Test]
        public void StringToDouble_UnreadableText_ReportsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            var converter = new StringToDoubleConverter();
            converter.Convert("abc");
            converter.Convert("abc");
            converter.Convert("still not a number");
        }

        // 2^24 + 1 is the smallest whole number a float cannot hold; it is the reason the double
        // converter exists next to the float one, and the reason "id that arrived as text" is in
        // its remarks.
        [Test]
        public void StringToDouble_KeepsAWholeNumberTheFloatSiblingRounds()
        {
            Assert.AreEqual(16777217d, new StringToDoubleConverter().Convert("16777217"), delta: 0d);
            Assert.AreEqual(16777216f, new StringToFloatConverter().Convert("16777217"));
        }

        [TestCase("42", 10d)]
        [TestCase("-1", 0d)]
        [TestCase("5", 5d)]
        public void StringToDouble_Clamp_HoldsTheResultInsideTheBounds(string value, double expected) =>
            Assert.AreEqual(
                expected,
                Clamped(new StringToDoubleConverter(), 0d, 10d).Convert(value),
                delta: 1e-12);

        // Math.Clamp throws when max is authored below min, and these are Inspector fields with
        // nothing to validate them. Note that the source comment ("a reversed pair reads as the
        // minimum") only holds below min: anything from min upward is pinned to max instead, so a
        // transposed pair never lets a value through — it snaps to one bound or the other.
        [TestCase("5", 10d)]
        [TestCase("0", 10d)]
        [TestCase("10", 0d)]
        [TestCase("20", 0d)]
        public void StringToDouble_ReversedClampBounds_PinInsteadOfThrowing(string value, double expected) =>
            Assert.AreEqual(
                expected,
                Clamped(new StringToDoubleConverter(), 10d, 0d).Convert(value),
                delta: 1e-12);

        // The clamp sits after the parse, so the fallback is returned raw: a fallback authored
        // outside the bounds comes out outside them.
        [Test]
        public void StringToDouble_Clamp_DoesNotApplyToTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            Assert.AreEqual(0d, Clamped(new StringToDoubleConverter(), 5d, 10d).Convert("abc"), delta: 1e-12);
        }

        // NaN fails both comparisons, so clamping does not fence it in — the bounds hold numbers,
        // not everything a double can be.
        [Test]
        public void StringToDouble_Clamp_LetsNaNThrough() =>
            Assert.IsTrue(double.IsNaN(Clamped(new StringToDoubleConverter(), 0d, 1d).Convert("NaN")));

        // ReturnInput cannot be honoured when the input is text and the output is not; the tooltip
        // promises it behaves as ReturnFallback.
        [Test]
        public void StringToDouble_ReturnInput_BehavesAsReturnFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDoubleConverter"));

            var converter = With(new StringToDoubleConverter(7.5d), "_onFailure", ConverterFailureMode.ReturnInput);

            Assert.AreEqual(7.5d, converter.Convert("abc"), delta: 1e-12);
        }

        [Test]
        public void StringToDouble_Throw_RaisesFormatException()
        {
            var converter = With(new StringToDoubleConverter(), "_onFailure", ConverterFailureMode.Throw);

            Assert.Throws<FormatException>(() => converter.Convert("abc"));
        }

        // Both halves resolve the culture at call time from the same field, so the pair agrees on a
        // device that writes one and a half as "1,5".
        [Test]
        public void StringToDouble_RoundTripsUnderACommaDecimalCulture()
        {
            UseGermanDevice();
            var converter = new StringToDoubleConverter(0d, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(1.5d);

            Assert.AreEqual("1,5", text);
            Assert.AreEqual(1.5d, converter.Convert(text), delta: 1e-12);
        }

        #endregion

        #region StringToDecimalConverter — exponents and the invariant fallback

        // AllowExponent on top of Number: a backend that serializes 1E5 would otherwise be readable
        // by the converter with less precision and not by the exact one.
        [TestCase("1E5", 100000)]
        [TestCase("1.5E2", 150)]
        [TestCase("-2e3", -2000)]
        public void StringToDecimal_ReadsAnExponent(string value, int expected) =>
            Assert.AreEqual((decimal)expected, new StringToDecimalConverter().Convert(value));

        [Test]
        public void StringToDecimal_ReadsAFractionalExponent() =>
            Assert.AreEqual(0.02m, new StringToDecimalConverter().Convert("2E-2"));

        // NumberStyles.Number carries AllowThousands, so the player-facing read matches the rest of
        // the family even though the fallback below refuses the same character.
        [Test]
        public void StringToDecimal_ReadsGroupedText() =>
            Assert.AreEqual(1000.5m, new StringToDecimalConverter().Convert("1,000.5"));

        // Eighteen significant digits: the reason to reach for this over the double converter.
        [Test]
        public void StringToDecimal_KeepsDigitsADoubleWouldRound() =>
            Assert.AreEqual(1234567890.12345678m, new StringToDecimalConverter().Convert("1234567890.12345678"));

        // decimal.MaxValue + 1. Out of range is a parse failure like any other rather than an
        // OverflowException escaping the push.
        [Test]
        public void StringToDecimal_OutOfRangeText_FallsBackAndReports()
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
        public void StringToDecimal_CommaInTheFallback_IsRefusedRatherThanReadAsFifteen()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter"));

            var converter = With(new StringToDecimalConverter(), "_fallback", "1,5");

            Assert.AreEqual(decimal.Zero, converter.Convert(null));
        }

        // The fallback is authored once and the player's text is typed every time, so the two are
        // read with different cultures on purpose: "1.5" in the field, "1,5" from a German player.
        [Test]
        public void StringToDecimal_Fallback_IsInvariantWhilePlayerTextIsNot()
        {
            UseGermanDevice();
            var converter = With(new StringToDecimalConverter(), "_fallback", "1.5");

            Assert.AreEqual(1.5m, converter.Convert(null));
            Assert.AreEqual(1.5m, converter.Convert("1,5"));
            LogAssert.NoUnexpectedReceived();
        }

        // The parsed fallback is cached against the string it came from by reference, so a field
        // edited while the game runs must not keep serving the old reading.
        [Test]
        public void StringToDecimal_Fallback_IsRereadWhenTheFieldChanges()
        {
            var converter = With(new StringToDecimalConverter(), "_fallback", "1.5");
            Assert.AreEqual(1.5m, converter.Convert(null));

            With(converter, "_fallback", "2.5");
            Assert.AreEqual(2.5m, converter.Convert(null));
        }

        [Test]
        public void StringToDecimal_Throw_RaisesFormatException()
        {
            var converter = With(new StringToDecimalConverter(), "_onFailure", ConverterFailureMode.Throw);

            Assert.Throws<FormatException>(() => converter.Convert("abc"));
        }

        [Test]
        public void StringToDecimal_RoundTripsUnderACommaDecimalCulture()
        {
            UseGermanDevice();
            var converter = new StringToDecimalConverter(0m, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(1.5m);

            Assert.AreEqual("1,5", text);
            Assert.AreEqual(1.5m, converter.Convert(text));
        }

        #endregion

        #region StringToTimeSpanConverter — the shapes TimeSpan accepts

        [TestCase("00:01:30", 90)]
        [TestCase("00:00:01", 1)]
        [TestCase("1:00:00", 3600)]
        [TestCase("-00:00:30", -30)]
        public void StringToTimeSpan_ReadsAClockDuration(string value, int seconds) =>
            Assert.AreEqual(TimeSpan.FromSeconds(seconds), new StringToTimeSpanConverter().Convert(value));

        [Test]
        public void StringToTimeSpan_ReadsTheInvariantDayForm() =>
            Assert.AreEqual(new TimeSpan(1, 2, 3, 4), new StringToTimeSpanConverter().Convert("1.02:03:04"));

        // The trap this converter is most often walked into: TimeSpan reads a bare number as DAYS,
        // so text that counts seconds wants StringToFloat + SecondsToTimeSpan instead.
        [Test]
        public void StringToTimeSpan_ABareNumber_IsDaysRatherThanSeconds() =>
            Assert.AreEqual(TimeSpan.FromDays(90), new StringToTimeSpanConverter().Convert("90"));

        [Test]
        public void StringToTimeSpan_ExactFormat_ReadsTheShapeItAsksFor() =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(90),
                new StringToTimeSpanConverter("hh\\:mm\\:ss").Convert("00:01:30"));

        // A single-digit hour is a different shape: TryParseExact wants both digits, and the
        // converter reports rather than quietly accepting the loose reading TryParse would allow.
        [Test]
        public void StringToTimeSpan_ExactFormat_RefusesALooserShape()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter"));

            Assert.AreEqual(
                TimeSpan.FromMinutes(5),
                new StringToTimeSpanConverter("hh\\:mm\\:ss", TimeSpan.FromMinutes(5)).Convert("0:01:30"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void StringToTimeSpan_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(
                TimeSpan.FromMinutes(5),
                new StringToTimeSpanConverter(string.Empty, TimeSpan.FromMinutes(5)).Convert(value));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void StringToTimeSpan_UnreadableText_ReportsOncePerInstance()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter"));

            var converter = new StringToTimeSpanConverter();
            converter.Convert("soon");
            converter.Convert("later");
        }

        [Test]
        public void StringToTimeSpan_Throw_RaisesFormatException()
        {
            var converter = With(new StringToTimeSpanConverter(), "_onFailure", ConverterFailureMode.Throw);

            Assert.Throws<FormatException>(() => converter.Convert("soon"));
        }

        #endregion

        #region StringToVector2Converter / StringToVector3Converter — the round trip and its cache

        // The collision the two halves have to agree about: a German device writes one and a half
        // as "1,5", and the separator between the components is a comma too. Written with the
        // device culture the pair would come out "1,5,2,5", which no split recovers — both halves
        // step back to the invariant reading so that what ConvertBack writes, Convert reads.
        [Test]
        public void StringToVector2_RoundTripsWhenTheCultureCollidesWithTheSeparator()
        {
            UseGermanDevice();
            var converter = new StringToVector2Converter(",", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector2(1.5f, 2.5f));

            Assert.AreEqual("1.5,2.5", text);
            Assert.AreEqual(new Vector2(1.5f, 2.5f), converter.Convert(text));
        }

        [Test]
        public void StringToVector3_RoundTripsWhenTheCultureCollidesWithTheSeparator()
        {
            UseGermanDevice();
            var converter = new StringToVector3Converter(",", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector3(1.5f, 2.5f, 3.5f));

            Assert.AreEqual("1.5,2.5,3.5", text);
            Assert.AreEqual(new Vector3(1.5f, 2.5f, 3.5f), converter.Convert(text));
        }

        // The other side of the same branch: with a separator the culture's decimal separator does
        // not collide with, the chosen culture is kept rather than always forced to invariant.
        [Test]
        public void StringToVector2_NonCollidingSeparator_KeepsTheChosenCulture()
        {
            UseGermanDevice();
            var converter = new StringToVector2Converter("; ", default, CultureInfoMode.CurrentCulture);

            var text = converter.ConvertBack(new Vector2(1.5f, 2.5f));

            Assert.AreEqual("1,5; 2,5", text);
            Assert.AreEqual(new Vector2(1.5f, 2.5f), converter.Convert(text));
        }

        // Text copied out of a console or a log arrives wrapped, with a space after the comma.
        [Test]
        public void StringToVector2_ReadsWhatVectorToStringWrites() =>
            Assert.AreEqual(new Vector2(1f, 2f), new StringToVector2Converter().Convert("(1.00, 2.00)"));

        [Test]
        public void StringToVector3_ReadsWhatVectorToStringWrites() =>
            Assert.AreEqual(new Vector3(1f, 2f, 3f), new StringToVector3Converter().Convert("(1.00, 2.00, 3.00)"));

        // The Inspector can clear the separator field, and the stand-in has to be the same on both
        // halves: a write that joined with nothing would put "12" on screen for (1, 2).
        [Test]
        public void StringToVector2_EmptySeparator_StandsInAComma()
        {
            var converter = new StringToVector2Converter(string.Empty);

            var text = converter.ConvertBack(new Vector2(1f, 2f));

            Assert.AreEqual("1,2", text);
            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert(text));
        }

        // Thousands are refused inside a component: the group separator and the separator between
        // components are the same character in most cultures, so accepting both would make "1,5"
        // a vector in one reading and fifteen thousand in the other.
        [Test]
        public void StringToVector2_GroupedComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, new StringToVector2Converter(";").Convert("1,000;2"));
        }

        // Three numbers are not a Vector2: the tail is read as one component and refused, rather
        // than the extra being dropped and a wrong-but-plausible vector pushed on.
        [Test]
        public void StringToVector2_ExtraComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, new StringToVector2Converter().Convert("1,2,3"));
        }

        [Test]
        public void StringToVector3_MissingComponent_IsRefused()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToVector3Converter"));

            Assert.AreEqual(Vector3.zero, new StringToVector3Converter().Convert("1,2"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void StringToVector2_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(
                new Vector2(9f, 9f),
                new StringToVector2Converter(",", new Vector2(9f, 9f)).Convert(value));
            LogAssert.NoUnexpectedReceived();
        }

        // The last reading is cached because splitting allocates on every push. The separator is
        // part of the key: it is editable while the game runs, and a hit that ignored it would
        // freeze the old reading in.
        [Test]
        public void StringToVector2_CachedReading_IsDroppedWhenTheSeparatorChanges()
        {
            var converter = new StringToVector2Converter();
            Assert.AreEqual(new Vector2(1f, 2f), converter.Convert("1,2"));

            With(converter, "_separator", ";");
            LogAssert.Expect(LogType.Error, new Regex("StringToVector2Converter"));

            Assert.AreEqual(Vector2.zero, converter.Convert("1,2"));
        }

        [Test]
        public void StringToVector2_Throw_RaisesFormatException()
        {
            var converter = With(new StringToVector2Converter(), "_onFailure", ConverterFailureMode.Throw);

            Assert.Throws<FormatException>(() => converter.Convert("not a vector"));
        }

        #endregion

        private static void UseGermanDevice() =>
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

        // The clamp is three Inspector fields with no constructor overload, and the transposed pair
        // is the whole point of the test that uses it, so it cannot be authored any other way.
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

            info!.SetValue(converter, value);
            return converter;
        }
    }
}
