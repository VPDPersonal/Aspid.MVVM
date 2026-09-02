using System;
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
    /// Coverage for <see cref="SecondsToTimeStringConverter"/> — the four layouts, Auto, the rounding
    /// modes, and the negative-duration text.
    /// </summary>
    [TestFixture]
    public sealed class SecondsToTimeStringConverterTests
    {
        [Test]
        public void Convert_Seconds_WritesTheWholeCountPadded() =>
            Assert.AreEqual("07", new SecondsToTimeStringConverter(TimeLayout.Seconds).Convert(7f));

        [Test]
        public void Convert_MinutesSeconds_WritesMmSs() =>
            Assert.AreEqual("01:05", new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds).Convert(65f));

        // The leading unit pads to two digits by default, same as every other unit.
        [Test]
        public void Convert_HoursMinutesSeconds_WritesHMmSs() =>
            Assert.AreEqual("01:00:05", new SecondsToTimeStringConverter(TimeLayout.HoursMinutesSeconds).Convert(3605f));

        [Test]
        public void Convert_DaysHoursMinutesSeconds_WritesDHhMmSs() =>
            Assert.AreEqual(
                "01:00:00:05",
                new SecondsToTimeStringConverter(TimeLayout.DaysHoursMinutesSeconds).Convert(86405f));

        [TestCase(30d, "00:30")]
        [TestCase(3605d, "01:00:05")]
        [TestCase(86405d, "01:00:00:05")]
        public void Convert_Auto_PicksTheShortestFittingLayout(double seconds, string expected) =>
            Assert.AreEqual(expected, new SecondsToTimeStringConverter(TimeLayout.Auto).Convert(seconds));

        // A countdown usually ceils so it does not show 0:00 for a whole second before it fires.
        [Test]
        public void Convert_Ceil_RoundsAFractionUp() =>
            Assert.AreEqual("00:01", new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds, RoundMode.Ceil).Convert(0.1f));

        [Test]
        public void Convert_Floor_DropsTheFraction() =>
            Assert.AreEqual("00:00", new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds, RoundMode.Floor).Convert(0.9f));

        [Test]
        public void Convert_WithoutPaddingTheLeadingUnit() =>
            Assert.AreEqual("1:05", new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds, RoundMode.Ceil, padLeading: false).Convert(65f));

        // Rounded first, so a fraction a countdown ceils up to zero reads as 00:00 rather than as the
        // negative text.
        [Test]
        public void Convert_NegativeDuration_WithoutNegativeText_ReadsAsZero() =>
            Assert.AreEqual("00:00", new SecondsToTimeStringConverter(TimeLayout.MinutesSeconds).Convert(-5f));

        // The negative text is decided after the rounding: the last fraction of a countdown ceils to
        // zero, and showing it there would flash it for the final frame of every timer.
        [Test]
        public void Convert_NegativeFractionCeiledToZero_ReadsAsZero() =>
            Assert.AreEqual("00:00", WithNegativeText("--:--").Convert(-0.3f));

        // The other half of the same rule: a duration still negative once rounded is the case the
        // text exists for.
        [Test]
        public void Convert_StillNegativeAfterRounding_ShowsTheNegativeText() =>
            Assert.AreEqual("--:--", WithNegativeText("--:--").Convert(-5f));

        [Test]
        public void Convert_UndeclaredLayout_ReportsAndWritesMinutesAndSeconds()
        {
            LogAssert.Expect(LogType.Error, new Regex("SecondsToTimeStringConverter.*not a declared TimeLayout"));

            Assert.AreEqual("01:05", new SecondsToTimeStringConverter((TimeLayout)99).Convert(65f));
        }

        // The negative text has no constructor parameter, so the test writes it the way the
        // Inspector does.
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
