using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="DateTimeToUnixTimestampConverter"/> — the epoch count in seconds and
    /// milliseconds, the pre-epoch and unspecified-kind readings, the int and double overloads, the
    /// clamp for a value the other side cannot hold, and the pairing with
    /// <see cref="UnixTimestampToDateTimeConverter"/>.
    /// </summary>
    [TestFixture]
    internal sealed class DateTimeToUnixTimestampConverterTests
    {
        private static readonly DateTime _utcMoment = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [TestCase(false, 1704067200L)]
        [TestCase(true, 1704067200000L)]
        public void Convert_AUtcMoment_IsTheEpochCount(bool milliseconds, long expected) =>
            Assert.AreEqual(expected, new DateTimeToUnixTimestampConverter(milliseconds).Convert(_utcMoment));

        // Seconds are floored rather than rounded: half past the second is still that second.
        [TestCase(false, 1704067200L)]
        [TestCase(true, 1704067200500L)]
        public void Convert_ASubSecondMoment_TruncatesToTheUnit(bool milliseconds, long expected) =>
            Assert.AreEqual(
                expected,
                new DateTimeToUnixTimestampConverter(milliseconds)
                    .Convert(new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc)));

        // Epoch counts run backwards too; a converter that used an unsigned intermediate would not.
        [Test]
        public void Convert_APreEpochMoment_IsNegative() =>
            Assert.AreEqual(
                -315619200L,
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(1960, 1, 1, 0, 0, 0, DateTimeKind.Utc)));

        // A moment with no Kind is read as local, matching what UnixTimestampToDateTimeConverter
        // hands back — reading it as UTC instead would shift every timestamp by the zone offset.
        // Midday mid-June is used because a DST transition never lands there and so cannot make the
        // local reading ambiguous.
        [Test]
        public void Convert_AnUnspecifiedKind_IsReadAsLocal() =>
            Assert.AreEqual(
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Local)),
                new DateTimeToUnixTimestampConverter().Convert(new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Unspecified)));

        // This converter exists only because the pair's reverse runs in TwoWay and OneWayToSource
        // binders alone, so the two have to agree digit for digit.
        [TestCase(false)]
        [TestCase(true)]
        public void Convert_MatchesTheReverseOfUnixTimestampToDateTime(bool milliseconds) =>
            Assert.AreEqual(
                new UnixTimestampToDateTimeConverter(milliseconds).ConvertBack(_utcMoment),
                new DateTimeToUnixTimestampConverter(milliseconds).Convert(_utcMoment));

        [Test]
        public void Convert_Int_MatchesTheReverseOfUnixTimestampToDateTime()
        {
            IConverter<DateTime, int> forward = new DateTimeToUnixTimestampConverter();
            ITwoWayConverter<int, DateTime> reverse = new UnixTimestampToDateTimeConverter();

            Assert.AreEqual(reverse.ConvertBack(_utcMoment), forward.Convert(_utcMoment));
        }

        // The double leg is the only one that keeps a fraction of a second, and both sides own the
        // same mapping of it.
        [Test]
        public void Convert_Double_MatchesTheReverseOfUnixTimestampToDateTime()
        {
            IConverter<DateTime, double> forward = new DateTimeToUnixTimestampConverter();
            ITwoWayConverter<double, DateTime> reverse = new UnixTimestampToDateTimeConverter();
            var moment = _utcMoment.AddTicks(TimeSpan.TicksPerSecond / 4);

            Assert.AreEqual(reverse.ConvertBack(moment), forward.Convert(moment));
        }

        // An int stops counting seconds in 2038, so a later date saturates instead of wrapping into
        // a moment on the wrong side of the epoch, and the console says the count was cut.
        [Test]
        public void Convert_Int_BeyondTheIntRange_ClampsAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("outside the range an int covers"));

            IConverter<DateTime, int> converter = new DateTimeToUnixTimestampConverter();

            Assert.AreEqual(int.MaxValue, converter.Convert(new DateTime(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
        }

        // The local leg is the one that can go wrong: the DateTime carries Kind Local and has to be
        // taken back to UTC before counting, or the round trip is out by the zone offset.
        [TestCase(true)]
        [TestCase(false)]
        public void Convert_RoundTripsAUnixTimestamp(bool utc)
        {
            const long timestamp = 1_700_000_000L;
            var moment = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: utc).Convert(timestamp);

            Assert.AreEqual(timestamp, new DateTimeToUnixTimestampConverter().Convert(moment));
        }

        // ConvertBack is the sibling's Convert, so the same timestamp has to name the same moment.
        [TestCase(false, 1_700_000_000L)]
        [TestCase(true, 1_700_000_000_000L)]
        public void ConvertBack_MatchesUnixTimestampToDateTime(bool milliseconds, long timestamp) =>
            Assert.AreEqual(
                new UnixTimestampToDateTimeConverter(milliseconds, utc: true).Convert(timestamp),
                new DateTimeToUnixTimestampConverter(milliseconds, utc: true).ConvertBack(timestamp));

        // Every int lands inside the calendar, read as seconds or as milliseconds alike, so the int
        // leg only has to agree with the long one.
        [Test]
        public void ConvertBack_Int_MatchesTheLongLeg()
        {
            var converter = new DateTimeToUnixTimestampConverter(milliseconds: false, utc: true);
            ITwoWayConverter<DateTime, int> intLeg = converter;

            Assert.AreEqual(converter.ConvertBack(1_700_000_000L), intLeg.ConvertBack(1_700_000_000));
        }

        [Test]
        public void ConvertBack_Double_RoundTripsTheFraction()
        {
            ITwoWayConverter<DateTime, double> converter =
                new DateTimeToUnixTimestampConverter(milliseconds: false, utc: true);

            Assert.AreEqual(1704067200.25d, converter.Convert(converter.ConvertBack(1704067200.25d)));
        }

        // A timestamp no DateTime can hold clamps like the sibling instead of taking the fallback:
        // the fallback is configured here and must lose.
        [TestCase(long.MaxValue, 9999)]
        [TestCase(long.MinValue, 1)]
        public void ConvertBack_BeyondTheDateTimeRange_ClampsRatherThanFallingBack(long timestamp, int expectedYear)
        {
            LogAssert.Expect(LogType.Error, new Regex("outside the range a DateTime covers"));

            var converter = new DateTimeToUnixTimestampConverter(
                milliseconds: false,
                utc: true,
                convertBackFallback: new ConverterFallback<DateTime>(_utcMoment));

            Assert.AreEqual(expectedYear, converter.ConvertBack(timestamp).Year);
        }

        // The double leg carries its own clamp, so it has to answer an impossible timestamp the same
        // way the long one does rather than reaching for the fallback.
        [Test]
        public void ConvertBack_Double_BeyondTheDateTimeRange_ClampsRatherThanFallingBack()
        {
            LogAssert.Expect(LogType.Error, new Regex("outside the range a DateTime covers"));

            ITwoWayConverter<DateTime, double> converter = new DateTimeToUnixTimestampConverter(
                milliseconds: false,
                utc: true,
                convertBackFallback: new ConverterFallback<DateTime>(_utcMoment));

            Assert.AreEqual(9999, converter.ConvertBack(1e18d).Year);
        }

        // A timestamp that is not a number has no nearest bound to clamp to, which leaves it the one
        // failure the fallback still answers.
        [Test]
        public void ConvertBack_Double_NotFinite_UsesTheFallbackAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("not a finite timestamp"));

            ITwoWayConverter<DateTime, double> converter =
                new DateTimeToUnixTimestampConverter(milliseconds: false, utc: true);

            Assert.AreEqual(DateTime.MinValue, converter.ConvertBack(double.NaN));
        }

        [Test]
        public void ConvertBack_Double_NotFinite_UsesTheConfiguredFallback()
        {
            LogAssert.Expect(LogType.Error, new Regex("not a finite timestamp"));

            ITwoWayConverter<DateTime, double> converter = new DateTimeToUnixTimestampConverter(
                milliseconds: false,
                utc: true,
                convertBackFallback: new ConverterFallback<DateTime>(_utcMoment));

            Assert.AreEqual(_utcMoment, converter.ConvertBack(double.PositiveInfinity));
        }
    }
}
