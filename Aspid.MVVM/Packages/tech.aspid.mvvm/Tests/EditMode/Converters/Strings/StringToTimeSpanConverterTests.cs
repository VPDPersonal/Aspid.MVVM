using System;
using UnityEngine;
using NUnit.Framework;
using System.Globalization;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="StringToTimeSpanConverter"/> — the shapes <see cref="TimeSpan"/>
    /// accepts, the exact-format refusal and the round trip.
    /// </summary>
    [TestFixture]
    [SetCulture("")]
    public sealed class StringToTimeSpanConverterTests
    {

        [TestCase("00:01:30", 90)]
        [TestCase("00:00:01", 1)]
        [TestCase("1:00:00", 3600)]
        [TestCase("-00:00:30", -30)]
        public void Convert_ReadsAClockDuration(string value, int seconds) =>
            Assert.AreEqual(TimeSpan.FromSeconds(seconds), new StringToTimeSpanConverter().Convert(value));

        [Test]
        public void Convert_ReadsTheInvariantDayForm() =>
            Assert.AreEqual(new TimeSpan(1, 2, 3, 4), new StringToTimeSpanConverter().Convert("1.02:03:04"));

        // The trap this converter is most often walked into: TimeSpan reads a bare number as DAYS,
        // so text that counts seconds wants StringToFloat + SecondsToTimeSpan instead.
        [Test]
        public void Convert_ABareNumber_IsDaysRatherThanSeconds() =>
            Assert.AreEqual(TimeSpan.FromDays(90), new StringToTimeSpanConverter().Convert("90"));

        [Test]
        public void Convert_ExactFormat_ReadsTheShapeItAsksFor() =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(90),
                new StringToTimeSpanConverter("hh\\:mm\\:ss").Convert("00:01:30"));

        // A single-digit hour is a different shape: TryParseExact wants both digits, and the
        // converter reports rather than quietly accepting the loose reading TryParse would allow.
        [Test]
        public void Convert_ExactFormat_RefusesALooserShape()
        {
            LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter"));

            Assert.AreEqual(
                TimeSpan.FromMinutes(5),
                new StringToTimeSpanConverter("hh\\:mm\\:ss", TimeSpan.FromMinutes(5)).Convert("0:01:30"));
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase(null)]
        public void Convert_BlankText_TakesTheFallbackQuietly(string value)
        {
            Assert.AreEqual(
                TimeSpan.FromMinutes(5),
                new StringToTimeSpanConverter(string.Empty, TimeSpan.FromMinutes(5)).Convert(value));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void Convert_UnreadableText_ReportsEveryPush()
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
        public void Convert_WithNoFormat_RoundTrips()
        {
            var converter = new StringToTimeSpanConverter();
            var duration = new TimeSpan(1, 2, 3, 4);

            Assert.AreEqual(duration, converter.Convert(converter.ConvertBack(duration)));
        }

        [Test]
        [SetCulture("de-DE")]
        public void Convert_WithNoFormat_RoundTripsUnderACommaDecimalCulture()
        {
            var converter = new StringToTimeSpanConverter();
            var duration = new TimeSpan(0, 0, 1, 30, 500);

            Assert.AreEqual(duration, converter.Convert(converter.ConvertBack(duration)));
        }

        [Test]
        public void Convert_ExactFormat_RoundTripsInTheAuthoredShape()
        {
            var converter = new StringToTimeSpanConverter("hh\\:mm\\:ss");

            Assert.AreEqual("00:01:30", converter.ConvertBack(TimeSpan.FromSeconds(90)));
            Assert.AreEqual(TimeSpan.FromSeconds(90), converter.Convert("00:01:30"));
        }

        // A format the reading half merely refuses is one the writing half throws on, and a binder
        // pushing back must not be the thing that stops.
        [Test]
        public void ConvertBack_AnUnusableFormat_IsReported()
        {
            var converter = new StringToTimeSpanConverter("yyyy");
            var duration = TimeSpan.FromSeconds(90);

            LogAssert.Expect(LogType.Error, new Regex("StringToTimeSpanConverter.*not a TimeSpan format"));
            Assert.AreEqual(duration.ToString("g", CultureInfo.CurrentCulture), converter.ConvertBack(duration));
        }
    }
}
