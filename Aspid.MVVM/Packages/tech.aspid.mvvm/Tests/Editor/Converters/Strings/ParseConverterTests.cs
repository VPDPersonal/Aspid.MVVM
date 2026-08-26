using System;
using UnityEngine;
using System.Threading;
using NUnit.Framework;
using System.Reflection;
using System.Globalization;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the parsing converters — the direction from text back into a value:
    /// <see cref="StringToIntConverter"/>, <see cref="StringToLongConverter"/>,
    /// <see cref="StringToFloatConverter"/>, <see cref="StringToDoubleConverter"/>,
    /// <see cref="StringToDecimalConverter"/>, <see cref="StringToBoolConverter"/>,
    /// <see cref="StringToEnumConverter{TEnum}"/>, <see cref="StringToDateTimeConverter"/>,
    /// <see cref="StringToTimeSpanConverter"/>, <see cref="StringToVector2Converter"/> and
    /// <see cref="StringToVector3Converter"/> — plus the clamp bounds the numeric parsers share.
    /// </summary>
    /// <remarks>
    /// Parsing currently lives inside <c>InputFieldBinder</c>, hard-coded: no culture, no fallback,
    /// and a failed parse silently swallows the event. These make those decisions authorable, and the
    /// failure rows below are the ones the binder gets wrong today.
    /// <para>
    /// The mistakes guarded against further down are the ones that only appear on somebody else's
    /// machine: a decimal separator colliding with the separator between vector components, a
    /// fallback authored under one locale and read under another, a pair of clamp bounds an author
    /// can transpose. The bounds, the failure mode and the decimal fallback are Inspector state with
    /// no constructor overload, so they are set through <see cref="With{T}"/>.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class ParseConverterTests
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

        // Blank text is an unfilled field rather than a malformed number, so it takes the fallback
        // without a word; text that is present but unreadable is reported.
        [TestCase("42", 42)]
        [TestCase("-7", -7)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        public void StringToInt_ReadsOrFallsBackQuietly(string value, int expected) =>
            Assert.AreEqual(expected, new StringToIntConverter().Convert(value));

        [TestCase("abc", 0)]
        [TestCase("1.5", 0)]
        public void StringToInt_UnreadableTextFallsBackAndReports(string value, int expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter"));
            Assert.AreEqual(expected, new StringToIntConverter().Convert(value));
        }

        [Test]
        public void StringToInt_UsesTheAuthoredFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToIntConverter"));
            Assert.AreEqual(-1, new StringToIntConverter(-1).Convert("nonsense"));
        }

        [Test]
        public void StringToInt_RoundTrips()
        {
            var converter = new StringToIntConverter();

            Assert.AreEqual(42, converter.Convert(converter.ConvertBack(42)));
        }

        [TestCase("42", 10)]
        [TestCase("-1", 0)]
        [TestCase("5", 5)]
        public void StringToInt_Clamp_HoldsTheResultInsideTheBounds(string value, int expected) =>
            Assert.AreEqual(expected, Clamped(new StringToIntConverter(), 0, 10).Convert(value));

        [Test]
        public void StringToLong_Reads() =>
            Assert.AreEqual(9_000_000_000L, new StringToLongConverter().Convert("9000000000"));

        [Test]
        public void StringToLong_UnreadableTextFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToLongConverter"));
            Assert.AreEqual(-1L, new StringToLongConverter(-1L).Convert("nonsense"));
        }

        [Test]
        public void StringToLong_RoundTrips()
        {
            var converter = new StringToLongConverter();

            Assert.AreEqual(9_000_000_000L, converter.Convert(converter.ConvertBack(9_000_000_000L)));
        }

        [TestCase("42", 10L)]
        [TestCase("-1", 0L)]
        [TestCase("5", 5L)]
        public void StringToLong_Clamp_HoldsTheResultInsideTheBounds(string value, long expected) =>
            Assert.AreEqual(expected, Clamped(new StringToLongConverter(), 0L, 10L).Convert(value));

        [TestCase("1.5", 1.5f)]
        [TestCase("-0.25", -0.25f)]
        public void StringToFloat_ReadsOrFallsBack(string value, float expected) =>
            Assert.AreEqual(expected, new StringToFloatConverter().Convert(value), 1e-5f);

        [Test]
        public void StringToFloat_UnreadableTextFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToFloatConverter"));
            Assert.AreEqual(0f, new StringToFloatConverter().Convert("abc"));
        }

        // A German player typing "1,5" means one and a half; reading it as invariant gives fifteen
        // or nothing at all.
        [Test]
        public void StringToFloat_HonoursTheCulture()
        {
            var german = new StringToFloatConverter(0f);
            var previous = Thread.CurrentThread.CurrentCulture;

            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");
                Assert.AreEqual(1.5f, german.Convert("1,5"), 1e-5f);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = previous;
            }
        }

        [TestCase("true", true)]
        [TestCase("TRUE", true)]
        [TestCase("1", true)]
        [TestCase("yes", true)]
        [TestCase("on", true)]
        [TestCase("false", false)]
        [TestCase("0", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void StringToBool_ReadsTheUsualSpellings(string value, bool expected) =>
            Assert.AreEqual(expected, new StringToBoolConverter().Convert(value));

        [Test]
        public void StringToBool_TakesAuthoredSpellings() =>
            Assert.IsTrue(new StringToBoolConverter(new[] { "да" }).Convert("ДА"));

        [TestCase("Rain", Weather.Rain)]
        [TestCase("rain", Weather.Rain)]
        [TestCase("", Weather.Clear)]
        public void StringToEnum_ReadsTheMember(string value, Weather expected) =>
            Assert.AreEqual(expected, new StringToEnumConverter<Weather>(Weather.Clear).Convert(value));

        [Test]
        public void StringToEnum_UnknownNameFallsBackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));
            Assert.AreEqual(Weather.Clear, new StringToEnumConverter<Weather>(Weather.Clear).Convert("nonsense"));
        }

        // Enum.TryParse accepts a bare number and hands back an undeclared member for it, which is
        // rarely what a name-shaped input means.
        [Test]
        public void StringToEnum_RejectsANumberThatNamesNoMember()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));
            Assert.AreEqual(Weather.Clear, new StringToEnumConverter<Weather>(Weather.Clear).Convert("99"));
        }

        [Test]
        public void StringToEnum_RoundTrips()
        {
            var converter = new StringToEnumConverter<Weather>(Weather.Clear);

            Assert.AreEqual(Weather.Snow, converter.Convert(converter.ConvertBack(Weather.Snow)));
        }

        // A combination of flags is a legal value that is not a member of its own, so the check that
        // keeps a bare number out cannot be Enum.IsDefined: it would throw the combination away and
        // report a perfectly good input as a failure.
        [TestCase("Red, Blue", Palette.Red | Palette.Blue)]
        [TestCase("red, blue", Palette.Red | Palette.Blue)]
        [TestCase("Red", Palette.Red)]
        [TestCase("None", Palette.None)]
        public void StringToEnum_ReadsACombinationOfFlags(string value, Palette expected) =>
            Assert.AreEqual(expected, new StringToEnumConverter<Palette>(Palette.None).Convert(value));

        [Test]
        public void StringToEnum_FlagsRoundTrip()
        {
            const Palette value = Palette.Red | Palette.Blue;
            var converter = new StringToEnumConverter<Palette>(Palette.None);

            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)));
        }

        // Bits no member of the enum declares are still refused: 8 is outside the mask the members
        // build, so it names nothing even though the enum is read as bits.
        [Test]
        public void StringToEnum_FlagsRejectBitsNoMemberDeclares()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToEnumConverter"));

            Assert.AreEqual(Palette.None, new StringToEnumConverter<Palette>(Palette.None).Convert("8"));
        }

        [Test]
        public void StringToDateTime_ReadsAnExactFormat() =>
            Assert.AreEqual(
                new DateTime(2024, 12, 25),
                new StringToDateTimeConverter("dd.MM.yyyy").Convert("25.12.2024"));

        [Test]
        public void StringToDateTime_WrongFormatGivesTheFallback()
        {
            var fallback = new DateTime(2000, 1, 1);
            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter"));

            Assert.AreEqual(
                fallback,
                new StringToDateTimeConverter("dd.MM.yyyy", fallback).Convert("2024-12-25"));
        }

        [Test]
        public void StringToDateTime_BlankGivesTheFallback() =>
            Assert.AreEqual(default(DateTime), new StringToDateTimeConverter().Convert(null));

        // Both halves read the same format and culture fields, so what ConvertBack writes, Convert
        // reads — the property a two-way binding on an input field depends on.
        [Test]
        public void StringToDateTime_RoundTripsAnExactFormat()
        {
            var converter = new StringToDateTimeConverter("dd.MM.yyyy");
            var date = new DateTime(2024, 12, 25);

            Assert.AreEqual("25.12.2024", converter.ConvertBack(date));
            Assert.AreEqual(date, converter.Convert(converter.ConvertBack(date)));
        }

        // With no format authored the culture's general form is written, which carries the time down
        // to the second and no further.
        [Test]
        public void StringToDateTime_WithNoFormat_RoundTripsToTheSecond()
        {
            var converter = new StringToDateTimeConverter();
            var date = new DateTime(2024, 12, 25, 13, 45, 30);

            Assert.AreEqual(date, converter.Convert(converter.ConvertBack(date)));
        }

        // A format the reading half merely refuses is one the writing half throws on, and a binder
        // pushing back must not be the thing that stops.
        [Test]
        public void StringToDateTime_ConvertBack_AnUnusableFormat_IsReported()
        {
            var converter = new StringToDateTimeConverter("Q");
            var date = new DateTime(2024, 12, 25);

            LogAssert.Expect(LogType.Error, new Regex("StringToDateTimeConverter.*not a DateTime format"));
            Assert.AreEqual(date.ToString(CultureInfo.CurrentCulture), converter.ConvertBack(date));
        }

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

        // Every push that fails is reported. Text that only starts failing once a locale changes, or
        // once a backend answers differently, is the case a report-once rule would hide.
        [Test]
        public void StringToDouble_UnreadableText_ReportsEveryPush()
        {
            for (var i = 0; i < 3; i++)
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

        // Both halves resolve the culture at call time from the same field, so the pair agrees on a
        // device that writes one and a half as "1,5".
        [Test]
        public void StringToDouble_RoundTripsUnderACommaDecimalCulture()
        {
            UseGermanDevice();
            var converter = new StringToDoubleConverter(0d);

            var text = converter.ConvertBack(1.5d);

            Assert.AreEqual("1,5", text);
            Assert.AreEqual(1.5d, converter.Convert(text), delta: 1e-12);
        }

        // ConvertBack writes the round-trip format rather than the default one. The default stops at
        // fifteen significant digits for a double and seven for a float, so a value pushed through an
        // input field and back would drift a little further on every trip.
        [Test]
        public void StringToDouble_ConvertBack_KeepsEveryDigitTheDefaultFormatWouldDrop()
        {
            const double value = 0.1d + 0.2d;
            var converter = new StringToDoubleConverter();

            Assert.AreNotEqual(value.ToString(CultureInfo.InvariantCulture), converter.ConvertBack(value));
            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)), delta: 0d);
        }

        [Test]
        public void StringToFloat_ConvertBack_RoundTripsExactly()
        {
            const float value = 1.1f;
            var converter = new StringToFloatConverter();

            Assert.AreEqual(value, converter.Convert(converter.ConvertBack(value)));
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
        public void StringToDecimal_RoundTripsUnderACommaDecimalCulture()
        {
            UseGermanDevice();
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
        public void StringToDecimal_Clamp_HoldsTheResultInsideTheBounds(string value, int expected) =>
            Assert.AreEqual((decimal)expected, ClampedDecimal("0", "10").Convert(value));

        // A bound left blank is no bound at all: an author who fills in one end has not asked for the
        // other to snap to zero.
        [TestCase("42", 42)]
        [TestCase("-1", 0)]
        public void StringToDecimal_Clamp_ABlankBoundIsNoBound(string value, int expected) =>
            Assert.AreEqual((decimal)expected, ClampedDecimal("0", string.Empty).Convert(value));

        // The clamp sits after the parse, so the fallback is returned raw — the same bargain the
        // double converter makes.
        [Test]
        public void StringToDecimal_Clamp_DoesNotApplyToTheFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter"));

            Assert.AreEqual(decimal.Zero, ClampedDecimal("5", "10").Convert("abc"));
        }

        // A bound is authored state rather than a bound value, so a mistyped one is reported and the
        // end it names is dropped instead of the whole push failing.
        [Test]
        public void StringToDecimal_Clamp_AMalformedBoundIsReportedAndIgnored()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToDecimalConverter.*the highest value"));

            Assert.AreEqual(42m, ClampedDecimal("0", "1,5").Convert("42"));
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
        public void StringToTimeSpan_UnreadableText_ReportsEveryPush()
        {
            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter"));

            var converter = new StringToTimeSpanConverter();
            converter.Convert("soon");
            converter.Convert("later");
        }

        // Both halves read the same format and culture fields, so what ConvertBack writes, Convert
        // reads back — including on a device whose fractions are written with a comma, which the
        // constant "c" form would ignore.
        [Test]
        public void StringToTimeSpan_WithNoFormat_RoundTrips()
        {
            var converter = new StringToTimeSpanConverter();
            var duration = new TimeSpan(1, 2, 3, 4);

            Assert.AreEqual(duration, converter.Convert(converter.ConvertBack(duration)));
        }

        [Test]
        public void StringToTimeSpan_WithNoFormat_RoundTripsUnderACommaDecimalCulture()
        {
            UseGermanDevice();
            var converter = new StringToTimeSpanConverter();
            var duration = new TimeSpan(0, 0, 1, 30, 500);

            Assert.AreEqual(duration, converter.Convert(converter.ConvertBack(duration)));
        }

        [Test]
        public void StringToTimeSpan_ExactFormat_RoundTripsInTheAuthoredShape()
        {
            var converter = new StringToTimeSpanConverter("hh\\:mm\\:ss");

            Assert.AreEqual("00:01:30", converter.ConvertBack(TimeSpan.FromSeconds(90)));
            Assert.AreEqual(TimeSpan.FromSeconds(90), converter.Convert("00:01:30"));
        }

        // A format the reading half merely refuses is one the writing half throws on, and a binder
        // pushing back must not be the thing that stops.
        [Test]
        public void StringToTimeSpan_ConvertBack_AnUnusableFormat_IsReported()
        {
            var converter = new StringToTimeSpanConverter("yyyy");
            var duration = TimeSpan.FromSeconds(90);

            LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter.*not a TimeSpan format"));
            Assert.AreEqual(duration.ToString("g", CultureInfo.CurrentCulture), converter.ConvertBack(duration));
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

        // The decimal bounds are authored as text rather than as numbers — Unity cannot serialize a
        // decimal field — so the shared Clamped helper above cannot set them.
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

            // Unity reads the object again after an Inspector edit, which is where a converter
            // holding a cache built from its settings drops it.
            if (converter is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();

            return converter;
        }
    }
}
