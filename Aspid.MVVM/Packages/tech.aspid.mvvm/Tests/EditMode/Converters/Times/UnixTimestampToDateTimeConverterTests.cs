using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="UnixTimestampToDateTimeConverter"/> — the seconds/milliseconds and
    /// UTC/local readings, the int and double overloads, and the clamp for a value outside what the
    /// other side holds.
    /// </summary>
    [TestFixture]
    public sealed class UnixTimestampToDateTimeConverterTests
    {
        [Test]
        public void Convert_Seconds_Utc_ReadsTheEpochCount() =>
            Assert.AreEqual(
                new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(1704067200L));

        [Test]
        public void Convert_Milliseconds_ReadsTheFinerUnit() =>
            Assert.AreEqual(
                new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc),
                new UnixTimestampToDateTimeConverter(milliseconds: true, utc: true).Convert(1704067200500L));

        [Test]
        public void ConvertBack_RoundTripsATimestamp()
        {
            var converter = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);

            Assert.AreEqual(1_700_000_000L, converter.ConvertBack(converter.Convert(1_700_000_000L)));
        }

        // Both kinds are read as local, so a moment that lost its Kind — through serialization, or
        // through a struct copy in a ViewModel — round-trips the same as one stamped Local. Midday
        // mid-June is used because a DST transition never lands there, so the local reading cannot
        // be ambiguous.
        [Test]
        public void ConvertBack_AnUnspecifiedKind_IsReadAsLocal()
        {
            var converter = new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);
            var moment = new DateTime(2024, 6, 15, 12, 0, 0);

            Assert.AreEqual(
                converter.ConvertBack(DateTime.SpecifyKind(moment, DateTimeKind.Local)),
                converter.ConvertBack(DateTime.SpecifyKind(moment, DateTimeKind.Unspecified)));
        }

        // FromUnixTime* throws outside the DateTime range, and the timestamp arrives from the
        // ViewModel — a broken one should show the wrong date, not stop the binder.
        [Test]
        public void Convert_BeyondTheDateTimeRange_ClampsAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("outside the range a DateTime covers"));

            Assert.AreEqual(DateTime.MaxValue.Date, new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(long.MaxValue).Date);
        }

        [Test]
        public void Convert_Int_ReadsTheEpochCount() =>
            Assert.AreEqual(
                new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(1_704_067_200));

        // An int counts 25 days of milliseconds, so the whole type maps into January 1970 — every
        // date the setting produces is wrong, which the console has to say out loud.
        [Test]
        public void Convert_Int_InMilliseconds_Reports()
        {
            LogAssert.Expect(LogType.Error, new Regex("cannot hold a millisecond timestamp"));

            Assert.AreEqual(
                1970,
                new UnixTimestampToDateTimeConverter(milliseconds: true, utc: true).Convert(1_704_067_200).Year);
        }

        [Test]
        public void Convert_Double_CarriesTheFractionOfASecond() =>
            Assert.AreEqual(
                new DateTime(2024, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc),
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(1704067200.5d));

        [Test]
        public void Convert_Double_NonFinite_UsesTheEpochAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("not a finite timestamp"));

            Assert.AreEqual(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true).Convert(double.NaN));
        }

        [Test]
        public void ConvertBack_Double_RoundTripsTheFraction()
        {
            ITwoWayConverter<double, DateTime> converter =
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);

            Assert.AreEqual(1704067200.25d, converter.ConvertBack(converter.Convert(1704067200.25d)));
        }

        // An int stops counting seconds in 2038, so a later date saturates instead of wrapping into
        // a moment on the wrong side of the epoch, and the console says the count was cut.
        [Test]
        public void ConvertBack_Int_BeyondTheIntRange_ClampsAndReports()
        {
            LogAssert.Expect(LogType.Error, new Regex("outside the range an int covers"));

            ITwoWayConverter<int, DateTime> converter =
                new UnixTimestampToDateTimeConverter(milliseconds: false, utc: true);

            Assert.AreEqual(int.MaxValue, converter.ConvertBack(new DateTime(2100, 1, 1, 0, 0, 0, DateTimeKind.Utc)));
        }
    }
}
