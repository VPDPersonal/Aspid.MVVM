using System;
using System.Threading;
using NUnit.Framework;
using System.Globalization;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the number-formatting and time converters.
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
            Assert.AreEqual(expected, new PluralizeConverter(PluralRule.English, "item", "items").Convert(count));

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
                new PluralizeConverter(PluralRule.Slavic, "предмет", "предметов", "предмета").Convert(count));

        // The teens are the exception: 11 takes the many form despite ending in 1.
        [TestCase(11, "11 предметов")]
        [TestCase(12, "12 предметов")]
        [TestCase(14, "14 предметов")]
        [TestCase(111, "111 предметов")]
        public void Pluralize_SlavicTeensTakeTheManyForm(int count, string expected) =>
            Assert.AreEqual(
                expected,
                new PluralizeConverter(PluralRule.Slavic, "предмет", "предметов", "предмета").Convert(count));

        [Test]
        public void Currency_PlacesTheSymbol()
        {
            Assert.AreEqual("$1,500", new CurrencyConverter("$").Convert(1500d));
            Assert.AreEqual("1,500₽", new CurrencyConverter("₽", SymbolPosition.After).Convert(1500d));
        }

        [Test]
        public void Percent_ScalesAndSuffixes() =>
            Assert.AreEqual("73.5%", new PercentStringConverter(1).Convert(0.735f));

        [Test]
        public void Percent_CanTakeAnAlreadyScaledValue() =>
            Assert.AreEqual("73%", new PercentStringConverter(0, inputIsNormalized: false).Convert(73f));

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

        [TestCase(1, "1st")]
        [TestCase(2, "2nd")]
        [TestCase(3, "3rd")]
        [TestCase(4, "4th")]
        [TestCase(11, "11th")]
        [TestCase(12, "12th")]
        [TestCase(13, "13th")]
        [TestCase(21, "21st")]
        [TestCase(101, "101st")]
        public void Ordinal_HandlesTheTeens(int value, string expected) =>
            Assert.AreEqual(expected, new OrdinalConverter().Convert(value));

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

        // The trap TimeSpanToStringConverter still has: this takes the pattern directly.
        [Test]
        public void TimeSpanFormat_TakesAPlainPattern() =>
            Assert.AreEqual("05:05", new TimeSpanFormatConverter(@"mm\:ss").Convert(TimeSpan.FromSeconds(305)));

        [Test]
        public void TimeSpanFormat_BrokenPatternFallsBackInsteadOfThrowing()
        {
            UnityEngine.TestTools.LogAssert.Expect(
                UnityEngine.LogType.Error,
                new System.Text.RegularExpressions.Regex("is not a TimeSpan format"));

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
        public void DateTimeToBool_ComparesWithNow()
        {
            Assert.IsTrue(new DateTimeToBoolConverter(Comparisons.GreaterThan).Convert(DateTime.Now.AddHours(1)));
            Assert.IsFalse(new DateTimeToBoolConverter(Comparisons.GreaterThan).Convert(DateTime.Now.AddHours(-1)));
        }

        [Test]
        public void DateTimeToBool_ComparesWithAnAuthoredMoment()
        {
            var reference = new DateTime(2024, 1, 1);

            Assert.IsTrue(new DateTimeToBoolConverter(Comparisons.LessThan, reference).Convert(new DateTime(2023, 1, 1)));
            Assert.IsFalse(new DateTimeToBoolConverter(Comparisons.LessThan, reference).Convert(new DateTime(2025, 1, 1)));
        }
    }
}
