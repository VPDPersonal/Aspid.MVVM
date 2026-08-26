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
    /// Coverage for the number-formatting and time converters, including
    /// <see cref="ThousandsSeparatorConverter"/> and <see cref="DecimalFormatConverter"/>.
    /// </summary>
    /// <remarks>
    /// The culture is pinned to invariant for the whole fixture, because almost every assertion here
    /// would otherwise pass or fail on the decimal separator rather than on the thing being tested.
    /// The two converters that exist <i>because</i> of culture have their own rows.
    /// </remarks>
    [TestFixture]
    internal sealed class FormatAndTimeConverterTests
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

        // "N0" here is the specifier everyone expects; GenericToStringConverter would return it as a literal.
        [Test]
        public void NumberFormat_TakesAPlainSpecifier() =>
            Assert.AreEqual("1,234,567", new NumberFormatConverter("N0").Convert(1234567));

        [Test]
        public void NumberFormat_HonoursTheCulture() =>
            Assert.AreEqual(
                "1 234 567",
                new NumberFormatConverter("N0", CultureInfoMode.InvariantCulture)
                    .Convert(1234567)
                    .Replace(' ', ' ')
                    .Replace(",", " "));

        [TestCase(999d, "999")]
        [TestCase(1234d, "1.23K")]
        [TestCase(1234567d, "1.23M")]
        [TestCase(1200000d, "1.2M")]
        [TestCase(-1234567d, "-1.23M")]
        [TestCase(1234567890123d, "1.23T")]
        public void Abbreviated_ShortensLargeNumbers(double value, string expected) =>
            Assert.AreEqual(expected, new AbbreviatedNumberConverter().Convert(value));

        // Past the last suffix the number keeps growing rather than inventing a unit.
        [Test]
        public void Abbreviated_BeyondTheLastSuffix_KeepsScaling() =>
            Assert.AreEqual("1230T", new AbbreviatedNumberConverter(2).Convert(1.23e15));

        // Trailing zeros are trimmed by default, which is why 1.20M reads as 1.2M above.
        [Test]
        public void Abbreviated_TrimsTrailingZeros() =>
            Assert.AreEqual("2M", new AbbreviatedNumberConverter(2).Convert(2000000d));

        [TestCase(1, "1 item")]
        [TestCase(2, "2 items")]
        [TestCase(0, "0 items")]
        public void Pluralize_English(int count, string expected) =>
            Assert.AreEqual(expected, new PluralizeConverter(new EnglishPluralRule("item", "items")).Convert(count));

        [TestCase(1, "1 предмет")]
        [TestCase(2, "2 предмета")]
        [TestCase(4, "4 предмета")]
        [TestCase(5, "5 предметов")]
        [TestCase(21, "21 предмет")]
        [TestCase(22, "22 предмета")]
        [TestCase(25, "25 предметов")]
        public void Pluralize_Slavic(int count, string expected) =>
            Assert.AreEqual(
                expected,
                new PluralizeConverter(new EastSlavicPluralRule("предмет", "предмета", "предметов")).Convert(count));

        // The teens are the exception: 11 takes the many form despite ending in 1.
        [TestCase(11, "11 предметов")]
        [TestCase(12, "12 предметов")]
        [TestCase(14, "14 предметов")]
        [TestCase(111, "111 предметов")]
        public void Pluralize_SlavicTeensTakeTheManyForm(int count, string expected) =>
            Assert.AreEqual(
                expected,
                new PluralizeConverter(new EastSlavicPluralRule("предмет", "предмета", "предметов")).Convert(count));

        [Test]
        public void Currency_PlacesTheSymbol()
        {
            Assert.AreEqual("$1,500", new CurrencyConverter("$").Convert(1500d));
            Assert.AreEqual("1,500₽", new CurrencyConverter("₽", SymbolPosition.After).Convert(1500d));
        }

        // A debt reads as a minus in front of the symbol. Formatting the signed amount straight into
        // a leading-symbol layout writes $-1,500 instead.
        [Test]
        public void Currency_NegativeAmount_KeepsTheSignInFront()
        {
            Assert.AreEqual("-$1,500", new CurrencyConverter("$").Convert(-1500d));
            Assert.AreEqual("-1,500₽", new CurrencyConverter("₽", SymbolPosition.After).Convert(-1500d));
        }

        [Test]
        public void Ratio_WritesAgainstTheMaximum() =>
            Assert.AreEqual("35 / 100", new RatioToStringConverter(100f).Convert(35f));

        [TestCase(15f, "+15")]
        [TestCase(-3f, "-3")]
        [TestCase(0f, "+0")]
        public void SignedNumber_ShowsTheSign(float value, string expected) =>
            Assert.AreEqual(expected, new SignedNumberStringConverter("0.##").Convert(value));

        [Test]
        public void SignedNumber_CanHideZero() =>
            Assert.AreEqual(string.Empty, new SignedNumberStringConverter("0.##", hideZero: true).Convert(0f));

        [TestCase(7, "007")]
        [TestCase(123, "123")]
        [TestCase(-7, "-007")]
        public void Padded_PadsToWidth(int value, string expected) =>
            Assert.AreEqual(expected, new PaddedNumberConverter(3).Convert(value));

        // Widened to a long before the sign is dropped, so the one input with no positive
        // counterpart of its own width prints instead of overflowing.
        [Test]
        public void Padded_IntMinValue_Formats() =>
            Assert.AreEqual("-2147483648", new PaddedNumberConverter(3).Convert(int.MinValue));

        [TestCase(0, "0th")]
        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(21, "21st")]
        [TestCase(22, "22nd")]
        [TestCase(23, "23rd")]
        [TestCase(100, "100th")]
        [TestCase(101, "101st")]
        public void Ordinal_LastDigitDecidesTheSuffix(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The classic trap: 11, 12 and 13 end in 1, 2 and 3 and still take "th". The exception is not
        // about the teens but about the last two digits, so it comes back in every hundred — 111 and
        // 1011 are the cases a "value < 20" guard gets wrong while looking correct.
        [TestCase(11, "11th")]
        [TestCase(12, "12th")]
        [TestCase(13, "13th")]
        [TestCase(111, "111th")]
        [TestCase(112, "112th")]
        [TestCase(113, "113th")]
        [TestCase(211, "211th")]
        [TestCase(1011, "1011th")]
        [TestCase(1012, "1012th")]
        [TestCase(1013, "1013th")]
        public void Ordinal_TheTeensAndEveryHundredAfterThem_TakeTh(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The other edge of the same exception: 14 is outside it despite being a teen, and 121 is
        // outside it despite ending in 21.
        [TestCase(14, "14th")]
        [TestCase(114, "114th")]
        [TestCase(121, "121st")]
        [TestCase(122, "122nd")]
        [TestCase(123, "123rd")]
        public void Ordinal_JustOutsideTheTeens_ReturnsToTheLastDigitRule(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // The suffix is picked from the magnitude while the sign survives the formatting, so a
        // negative rank keeps the suffix its positive counterpart would take.
        [TestCase(-1, "-1st")]
        [TestCase(-3, "-3rd")]
        [TestCase(-11, "-11th")]
        public void Ordinal_Negative_KeepsTheSuffixOfItsMagnitude(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

        // Only the last two digits are ever negated, so the one input with no positive counterpart
        // of its own width prints instead of overflowing.
        [Test]
        public void Ordinal_IntMinValue_Formats() =>
            Assert.AreEqual("-2147483648th", new OrdinalConverter().Convert(int.MinValue));

        [Test]
        public void Ordinal_IntMaxValue_Formats() =>
            Assert.AreEqual("2147483647th", new OrdinalConverter().Convert(int.MaxValue));

        // The culture reaches the digits and nothing else, and .NET writes ASCII digits for an
        // integer whichever culture is picked — which is what makes the field decorative for any
        // ordinal that is not negative.
        [Test]
        public void Ordinal_TheCultureChangesNothingForAPositiveNumber()
        {
            Assert.AreEqual("1234th", new OrdinalConverter(CultureInfoMode.InvariantCulture).Convert(1234));

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1234th", new OrdinalConverter(CultureInfoMode.CurrentCulture).Convert(1234));
        }

        // The separator field ships empty, and empty is the one value that cannot mean "no
        // separator": it means "whatever the device is set to", which is exactly what a project
        // reaching for this converter is trying to escape.
        [Test]
        public void Thousands_EmptySeparator_FollowsTheDeviceCulture()
        {
            Assert.AreEqual("1,234,567", new ThousandsSeparatorConverter().Convert(1234567L));

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1.234.567", new ThousandsSeparatorConverter().Convert(1234567L));
        }

        [TestCase("_", "1_234_567")]
        [TestCase("'", "1'234'567")]
        [TestCase(" ", "1 234 567")]
        // Written escaped because the character a game most often reaches for here is invisible.
        [TestCase("\u2009", "1\u2009234\u2009567")]
        public void Thousands_AuthoredSeparator_ReplacesTheCultureOne(string separator, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter(separator).Convert(1234567L));

        // The reason the converter exists at all: a score authored with a thin space has to read the
        // same in a screenshot taken on a German machine as on an English one.
        [Test]
        public void Thousands_AuthoredSeparator_SurvivesTheDeviceCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("1_234_567", new ThousandsSeparatorConverter("_").Convert(1234567L));
        }

        [TestCase(0L, "0")]
        [TestCase(999L, "999")]
        [TestCase(1000L, "1_000")]
        [TestCase(-1234567L, "-1_234_567")]
        public void Thousands_BelowAGroupAndBelowZero(long value, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter("_").Convert(value));

        // Nothing on this path negates the input, so the value with no positive counterpart formats
        // instead of throwing.
        [Test]
        public void Thousands_LongMinValue_Formats() =>
            Assert.AreEqual(
                "-9_223_372_036_854_775_808",
                new ThousandsSeparatorConverter("_").Convert(long.MinValue));

        [TestCase(1234567, "1_234_567")]
        [TestCase(int.MinValue, "-2_147_483_648")]
        public void Thousands_IntOverload_GroupsTheSameWay(int value, string expected) =>
            Assert.AreEqual(expected, new ThousandsSeparatorConverter("_").Convert(value));

        // A NumberFormatInfo taken from a culture is shared process-wide and read-only. Setting the
        // separator on it rather than on a clone would either throw here or silently re-format every
        // number the rest of the game prints.
        [Test]
        public void Thousands_AuthoredSeparator_LeavesTheSharedCultureFormatAlone()
        {
            new ThousandsSeparatorConverter("_", CultureInfoMode.InvariantCulture).Convert(1234567L);

            Assert.AreEqual(",", CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator);
        }

        // The clone is cached because a binder pushes on every notification rather than on every
        // change. Re-authoring the separator has to invalidate that cache; a stale clone would keep
        // writing the old separator for the rest of the session.
        [Test]
        public void Thousands_SeparatorReauthored_RebuildsTheCachedFormat()
        {
            var converter = new ThousandsSeparatorConverter("_");
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));

            SetSeparator(converter, "'");

            Assert.AreEqual("1'234'567", converter.Convert(1234567L));
        }

        // The same cache, from the other side: repeated pushes must not drift.
        [Test]
        public void Thousands_RepeatedCalls_KeepFormattingTheSameWay()
        {
            var converter = new ThousandsSeparatorConverter("_");

            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
            Assert.AreEqual("7_654_321", converter.Convert(7654321L));
            Assert.AreEqual("1_234_567", converter.Convert(1234567L));
        }

        // An authored separator replaces the culture's separator but not its grouping. India groups
        // the last three digits and then in pairs, so the authored character lands in Indian places —
        // the half of the tooltip's promise that the previous row cannot show.
        [Test]
        public void Thousands_AuthoredSeparator_KeepsTheCultureGrouping()
        {
            CultureInfo india;

            try
            {
                india = CultureInfo.GetCultureInfo("en-IN");
            }
            catch (CultureNotFoundException)
            {
                Assert.Ignore("en-IN is not present in this runtime's culture data.");
                return;
            }

            Assume.That(india.NumberFormat.NumberGroupSizes, Is.EqualTo(new[] { 3, 2 }));

            Thread.CurrentThread.CurrentCulture = india;

            Assert.AreEqual("12_34_567", new ThousandsSeparatorConverter("_").Convert(1234567L));
        }

        // The default format is N2, so the row also pins the rounding: .678 becomes .68 rather than
        // being cut to .67.
        [Test]
        public void Decimal_DefaultFormat_GroupsAndRoundsToTwoDecimals() =>
            Assert.AreEqual("12,345.68", new DecimalFormatConverter().Convert(12345.678m));

        [Test]
        public void Decimal_HonoursTheCulture()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("de-DE");

            Assert.AreEqual("12.345,68", new DecimalFormatConverter().Convert(12345.678m));
            Assert.AreEqual(
                "12,345.68",
                new DecimalFormatConverter("N2", CultureInfoMode.InvariantCulture).Convert(12345.678m));
        }

        // Why the converter refuses to reach decimal through double: this amount carries 24
        // significant digits and a double holds 15-17, so the double route prints
        // 123,456,789,012,345,685,803,008 — wrong from the eighteenth digit on.
        [Test]
        public void Decimal_KeepsDigitsADoubleWouldLose() =>
            Assert.AreEqual(
                "123,456,789,012,345,678,901,234",
                new DecimalFormatConverter("N0", CultureInfoMode.InvariantCulture)
                    .Convert(123456789012345678901234m));

        // A decimal carries its scale, so an authored 1.50 is not the same value as 1.5 and a price
        // keeps the cent column it was written with.
        [Test]
        public void Decimal_GeneralFormat_PreservesTheScale() =>
            Assert.AreEqual("1.50", GeneralDecimalFormat().Convert(1.50m));

        // The basket that drifts: in double, 0.1 + 0.2 is 0.30000000000000004.
        [Test]
        public void Decimal_GeneralFormat_ShowsNoBinaryDrift() =>
            Assert.AreEqual("0.3", GeneralDecimalFormat().Convert(0.1m + 0.2m));

        // Clearing the format field in the Inspector is not a crash — an empty numeric format string
        // is defined to mean the general format.
        [Test]
        public void Decimal_EmptyFormat_FallsBackToTheGeneralFormat() =>
            Assert.AreEqual("12345.678", GeneralDecimalFormat().Convert(12345.678m));

        // A one-character format is read as a standard specifier, and an unknown one makes .NET throw.
        // The converter catches that, reports it and writes the amount with the general format, so a
        // typo in the Inspector surfaces as a console line and plain digits rather than as a dead binder.
        [Test]
        public void Decimal_UnknownStandardSpecifier_ReportsAndFallsBackToTheGeneralFormat()
        {
            LogAssert.Expect(LogType.Error, new Regex("DecimalFormatConverter.*is not a numeric format"));

            Assert.AreEqual("12.5", new DecimalFormatConverter("q").Convert(12.5m));
        }

        // The report is not once-only: a format that breaks is broken on every push, and a converter
        // that says so once leaves the rest of the session looking healthy.
        [Test]
        public void Decimal_UnknownStandardSpecifier_ReportsEveryPush()
        {
            var converter = new DecimalFormatConverter("q");

            for (var i = 0; i < 2; i++)
                LogAssert.Expect(LogType.Error, new Regex("DecimalFormatConverter.*is not a numeric format"));

            Assert.AreEqual("12.5", converter.Convert(12.5m));
            Assert.AreEqual("12.5", converter.Convert(12.5m));
        }

        // Two characters make it a custom format string instead, where an unrecognised character is
        // copied to the output verbatim: the very same typo now prints itself and loses the number
        // entirely, without throwing anything anyone could notice.
        [Test]
        public void Decimal_UnknownCustomFormat_PrintsItselfInsteadOfTheAmount() =>
            Assert.AreEqual("qq", new DecimalFormatConverter("qq").Convert(12.5m));

        // C is what the culture field is worth having for. The symbol and the side it sits on come
        // from culture data that differs between runtimes, so the assertion is limited to what is
        // invariant: the amount is there and something was added to it.
        [Test]
        public void Decimal_CurrencyFormat_AddsASymbolToTheAmount()
        {
            var text = new DecimalFormatConverter("C2", CultureInfoMode.InvariantCulture).Convert(12.5m);

            StringAssert.Contains("12.50", text);
            Assert.AreNotEqual("12.50", text);
        }

        [TestCase(512L, "512 B")]
        [TestCase(2048L, "2.0 KiB")]
        [TestCase(1048576L, "1.0 MiB")]
        public void ByteSize_ScalesToUnits(long value, string expected) =>
            Assert.AreEqual(expected, new ByteSizeConverter().Convert(value));

        [TestCase(1, "I")]
        [TestCase(4, "IV")]
        [TestCase(9, "IX")]
        [TestCase(14, "XIV")]
        [TestCase(1987, "MCMLXXXVII")]
        [TestCase(3999, "MMMCMXCIX")]
        public void Roman_WritesTheNumeral(int value, string expected) =>
            Assert.AreEqual(expected, new RomanNumeralConverter().Convert(value));

        [TestCase(0)]
        [TestCase(4000)]
        [TestCase(-1)]
        public void Roman_OutsideItsRangeFallsBackToDigits(int value) =>
            Assert.AreEqual(value.ToString(CultureInfo.InvariantCulture), new RomanNumeralConverter().Convert(value));

        // The converter every project writes for itself: 95.4 seconds is 01:36 on a countdown.
        [Test]
        public void SecondsToTime_CeilsByDefault() =>
            Assert.AreEqual("01:36", new SecondsToTimeStringConverter().Convert(95.4f));

        // A floored countdown shows 0:00 for a whole second before it fires.
        [Test]
        public void SecondsToTime_FloorsWhenAsked() =>
            Assert.AreEqual(
                "01:35",
                new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds, RoundMode.Floor).Convert(95.4f));

        [TestCase(TimeLayout.Seconds, 95f, "95")]
        [TestCase(TimeLayout.MinutesSeconds, 95f, "01:35")]
        [TestCase(TimeLayout.HoursMinutesSeconds, 3725f, "01:02:05")]
        [TestCase(TimeLayout.DaysHoursMinutesSeconds, 90061f, "01:01:01:01")]
        public void SecondsToTime_Layouts(TimeLayout layout, float seconds, string expected) =>
            Assert.AreEqual(expected, new SecondsToTimeStringConverter(layout, RoundMode.Floor).Convert(seconds));

        [TestCase(95f, "01:35")]
        [TestCase(3725f, "01:02:05")]
        [TestCase(90061f, "01:01:01:01")]
        public void SecondsToTime_AutoPicksTheShortestFit(float seconds, string expected) =>
            Assert.AreEqual(expected, new SecondsToTimeStringConverter(TimeLayout.Auto, RoundMode.Floor).Convert(seconds));

        [Test]
        public void SecondsToTime_NegativeIsClampedToZero() =>
            Assert.AreEqual("00:00", new SecondsToTimeStringConverter().Convert(-5f));

        // The negative text is decided after the rounding: the last fraction of a countdown ceils to
        // zero, and showing "--:--" there would flash it for the final frame of every timer.
        [Test]
        public void SecondsToTime_NegativeFractionCeiledToZero_ReadsAsZero() =>
            Assert.AreEqual("00:00", WithNegativeText("--:--").Convert(-0.3f));

        // The other half of the same rule: a duration still negative once rounded is the case the
        // text exists for.
        [Test]
        public void SecondsToTime_StillNegativeAfterRounding_ShowsTheNegativeText() =>
            Assert.AreEqual("--:--", WithNegativeText("--:--").Convert(-5f));

        // Unlike a composite format, this takes the TimeSpan pattern directly.
        [Test]
        public void TimeSpanFormat_TakesAPlainPattern() =>
            Assert.AreEqual("05:05", new TimeSpanFormatConverter(@"mm\:ss").Convert(TimeSpan.FromSeconds(305)));

        [Test]
        public void TimeSpanFormat_BrokenPatternFallsBackInsteadOfThrowing()
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a TimeSpan format"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(305).ToString(),
                new TimeSpanFormatConverter("qq").Convert(TimeSpan.FromSeconds(305)));
        }

        [Test]
        public void SecondsToTimeSpan_RoundTrips()
        {
            var converter = new SecondsToTimeSpanConverter();

            Assert.AreEqual(90f, converter.ConvertBack(converter.Convert(90f)), delta: 1e-3f);
        }

        // A NaN reaches TimeSpan.FromSeconds from any division by zero upstream, and it throws there.
        // Zero is the documented answer, and the console line is what keeps it from passing for a
        // legitimate zero.
        [TestCase(float.NaN)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.NegativeInfinity)]
        public void SecondsToTimeSpan_NotAFiniteNumber_ReportsAndReturnsZero(float seconds)
        {
            LogAssert.Expect(LogType.Error, new Regex("SecondsToTimeSpanConverter.*finite"));

            Assert.AreEqual(TimeSpan.Zero, new SecondsToTimeSpanConverter().Convert(seconds));
        }

        [TestCase(TimeUnit.TotalSeconds, 90f)]
        [TestCase(TimeUnit.TotalMinutes, 1.5f)]
        [TestCase(TimeUnit.Seconds, 30f)]
        public void TimeSpanToNumber_Measures(TimeUnit unit, float expected) =>
            Assert.AreEqual(
                expected,
                new TimeSpanToNumberConverter(unit).Convert(TimeSpan.FromSeconds(90)),
                delta: 1e-4f);

        [Test]
        public void UnixTimestamp_RoundTrips()
        {
            var converter = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);
            const long timestamp = 1_700_000_000L;

            Assert.AreEqual(timestamp, converter.ConvertBack(converter.Convert(timestamp)));
        }

        // A DateTime that lost its Kind — through serialization, or through a struct copy in a
        // ViewModel — is read as local, which is what DateTimeToUnixTimestampConverter does with it
        // on the way out; the pair has to agree. Midday mid-June is used because a DST transition
        // never lands there and so cannot make the local reading ambiguous.
        [Test]
        public void UnixTimestamp_ConvertBack_AnUnspecifiedKind_IsReadAsLocal()
        {
            var converter = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);
            var moment = new DateTime(2024, 6, 15, 12, 0, 0);

            Assert.AreEqual(
                converter.ConvertBack(DateTime.SpecifyKind(moment, DateTimeKind.Local)),
                converter.ConvertBack(DateTime.SpecifyKind(moment, DateTimeKind.Unspecified)));
        }

        // A timestamp no DateTime can hold makes DateTimeOffset throw, and it arrives from the
        // ViewModel — the clamp shows the wrong date and says so instead of stopping the binder.
        [Test]
        public void UnixTimestamp_PastTheEndOfTime_ReportsAndClamps()
        {
            LogAssert.Expect(LogType.Error, new Regex("UnixTimestampToDateTimeConverter.*outside the range"));

            var moment = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(long.MaxValue);

            Assert.AreEqual(9999, moment.Year);
        }

        [Test]
        public void DateTimeFormat_FormatsWithThePattern() =>
            Assert.AreEqual("25.12.2024", new DateTimeFormatConverter("dd.MM.yyyy").Convert(new DateTime(2024, 12, 25)));

        [Test]
        public void RelativeTime_DescribesThePast() =>
            Assert.AreEqual("5m ago", new RelativeTimeConverter().Convert(DateTime.Now.AddMinutes(-5)));

        [Test]
        public void RelativeTime_DescribesTheFuture() =>
            Assert.AreEqual("in 2h", new RelativeTimeConverter().Convert(DateTime.Now.AddHours(2).AddSeconds(1)));

        [Test]
        public void RelativeTime_NowIsNow() =>
            Assert.AreEqual("now", new RelativeTimeConverter().Convert(DateTime.Now));

        [Test]
        public void DateTimeCompare_ComparesWithNow()
        {
            Assert.IsTrue(new DateTimeCompareConverter(ComparisonMode.GreaterThan).Convert(DateTime.Now.AddHours(1)));
            Assert.IsFalse(new DateTimeCompareConverter(ComparisonMode.GreaterThan).Convert(DateTime.Now.AddHours(-1)));
        }

        [Test]
        public void DateTimeCompare_ComparesWithAnAuthoredMoment()
        {
            var reference = new DateTime(2024, 1, 1);

            Assert.IsTrue(new DateTimeCompareConverter(ComparisonMode.LessThan, reference).Convert(new DateTime(2023, 1, 1)));
            Assert.IsFalse(new DateTimeCompareConverter(ComparisonMode.LessThan, reference).Convert(new DateTime(2025, 1, 1)));
        }

        private static DecimalFormatConverter GeneralDecimalFormat() =>
            new DecimalFormatConverter(string.Empty, CultureInfoMode.InvariantCulture);

        // The separator has a constructor, but re-authoring it on a live instance is Inspector state
        // with no setter, so the test writes it the way the Inspector does.
        private static void SetSeparator(ThousandsSeparatorConverter converter, string separator)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var field = typeof(ThousandsSeparatorConverter).GetField("_separator", flags);
            if (field is null) throw new InvalidOperationException("ThousandsSeparatorConverter has no _separator field.");

            field.SetValue(converter, separator);

            // Unity reads the object again after an Inspector edit, which is where a converter
            // holding a cache built from its settings drops it.
            if (converter is ISerializationCallbackReceiver receiver) receiver.OnAfterDeserialize();
        }

        // The negative text has no constructor parameter, so the test writes it the way the Inspector does.
        private static SecondsToTimeStringConverter WithNegativeText(string text)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var converter = new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds);
            var field = typeof(SecondsToTimeStringConverter).GetField("_negativeText", flags);
            if (field is null) throw new InvalidOperationException("SecondsToTimeStringConverter has no _negativeText field.");

            field.SetValue(converter, text);

            return converter;
        }
    }
}
