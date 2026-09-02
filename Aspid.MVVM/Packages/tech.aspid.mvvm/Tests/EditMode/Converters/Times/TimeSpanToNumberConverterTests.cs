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
    /// Coverage for <see cref="TimeSpanToNumberConverter"/> — the five <see cref="TimeUnit"/> readings,
    /// the numeric output family and the undeclared-unit fallback.
    /// </summary>
    [TestFixture]
    public sealed class TimeSpanToNumberConverterTests
    {
        private static readonly TimeSpan _duration = new(1, 2, 3, 4);

        [Test]
        public void Convert_Seconds_ReadsTheWholeSecondsWithinTheMinute() =>
            Assert.AreEqual(4f, new TimeSpanToNumberConverter(TimeUnit.Seconds).Convert(_duration));

        [Test]
        public void Convert_TotalSeconds_ReadsTheWholeDurationInSeconds() =>
            Assert.AreEqual((float)_duration.TotalSeconds, new TimeSpanToNumberConverter(TimeUnit.TotalSeconds).Convert(_duration));

        [Test]
        public void Convert_TotalMinutes_ReadsTheWholeDurationInMinutes() =>
            Assert.AreEqual((float)_duration.TotalMinutes, new TimeSpanToNumberConverter(TimeUnit.TotalMinutes).Convert(_duration));

        [Test]
        public void Convert_TotalHours_ReadsTheWholeDurationInHours() =>
            Assert.AreEqual((float)_duration.TotalHours, new TimeSpanToNumberConverter(TimeUnit.TotalHours).Convert(_duration));

        [Test]
        public void Convert_TotalDays_ReadsTheWholeDurationInDays() =>
            Assert.AreEqual((float)_duration.TotalDays, new TimeSpanToNumberConverter(TimeUnit.TotalDays).Convert(_duration));

        [Test]
        public void Convert_ToDouble_KeepsThePrecisionTheFloatOutputLoses() =>
            Assert.AreEqual(
                _duration.TotalSeconds,
                ((IConverter<TimeSpan, double>)new TimeSpanToNumberConverter(TimeUnit.TotalSeconds)).Convert(_duration));

        [Test]
        public void Convert_ToInt_TruncatesTowardZero() =>
            Assert.AreEqual(
                26,
                ((IConverter<TimeSpan, int>)new TimeSpanToNumberConverter(TimeUnit.TotalHours)).Convert(_duration));

        [Test]
        public void Convert_ToLong_TruncatesTowardZero() =>
            Assert.AreEqual(
                (long)_duration.TotalSeconds,
                ((IConverter<TimeSpan, long>)new TimeSpanToNumberConverter(TimeUnit.TotalSeconds)).Convert(_duration));

        [Test]
        public void Convert_ToInt_SaturatesADurationPastTheIntRange() =>
            Assert.AreEqual(
                int.MaxValue,
                ((IConverter<TimeSpan, int>)new TimeSpanToNumberConverter(TimeUnit.TotalSeconds)).Convert(TimeSpan.MaxValue));

        [Test]
        public void Convert_UndeclaredUnit_ReportsAndMeasuresInTotalSeconds()
        {
            LogAssert.Expect(LogType.Error, new Regex("TimeSpanToNumberConverter.*not a declared TimeUnit"));

            Assert.AreEqual(
                (float)_duration.TotalSeconds,
                new TimeSpanToNumberConverter((TimeUnit)99).Convert(_duration));
        }
    }
}
