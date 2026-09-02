using System;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SecondsToTimeSpanConverter"/> — the round trip of every numeric
    /// overload, the non-finite guard, and the out-of-range clamp.
    /// </summary>
    [TestFixture]
    public sealed class SecondsToTimeSpanConverterTests
    {
        [Test]
        public void Convert_SecondsToDuration() =>
            Assert.AreEqual(TimeSpan.FromSeconds(90), new SecondsToTimeSpanConverter().Convert(90f));

        [Test]
        public void Convert_IntSecondsToDuration() =>
            Assert.AreEqual(TimeSpan.FromSeconds(90), new SecondsToTimeSpanConverter().Convert(90));

        [Test]
        public void Convert_LongSecondsToDuration() =>
            Assert.AreEqual(TimeSpan.FromHours(1), new SecondsToTimeSpanConverter().Convert(3600L));

        [Test]
        public void Convert_DoubleSecondsToDuration_KeepsTheFraction() =>
            Assert.AreEqual(TimeSpan.FromSeconds(1.5d), new SecondsToTimeSpanConverter().Convert(1.5d));

        [Test]
        public void ConvertBack_DurationToSeconds() =>
            Assert.AreEqual(90f, new SecondsToTimeSpanConverter().ConvertBack(TimeSpan.FromSeconds(90)));

        [Test]
        public void ConvertBack_DurationToDoubleSeconds() =>
            Assert.AreEqual(1.5d, Back<double>(TimeSpan.FromSeconds(1.5d)));

        [Test]
        public void ConvertBack_DurationToIntSeconds_DropsTheFraction() =>
            Assert.AreEqual(90, Back<int>(TimeSpan.FromSeconds(90.6d)));

        [Test]
        public void ConvertBack_DurationToLongSeconds_DropsTheFraction() =>
            Assert.AreEqual(90L, Back<long>(TimeSpan.FromSeconds(90.6d)));

        [Test]
        public void ConvertBack_DurationBeyondIntRange_Saturates() =>
            Assert.AreEqual(int.MaxValue, Back<int>(TimeSpan.MaxValue));

        [Test]
        public void RoundTrips() =>
            Assert.AreEqual(
                123f,
                new SecondsToTimeSpanConverter().ConvertBack(new SecondsToTimeSpanConverter().Convert(123f)));

        [Test]
        public void RoundTrips_Int() =>
            Assert.AreEqual(123, Back<int>(new SecondsToTimeSpanConverter().Convert(123)));

        [Test]
        public void RoundTrips_Long() =>
            Assert.AreEqual(123L, Back<long>(new SecondsToTimeSpanConverter().Convert(123L)));

        [TestCase(float.NaN)]
        [TestCase(float.PositiveInfinity)]
        [TestCase(float.NegativeInfinity)]
        public void Convert_NonFinite_ReportsAndReturnsZero(float value)
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a finite number of seconds"));

            Assert.AreEqual(TimeSpan.Zero, new SecondsToTimeSpanConverter().Convert(value));
        }

        [TestCase(double.NaN)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity)]
        public void Convert_NonFiniteDouble_ReportsAndReturnsZero(double value)
        {
            LogAssert.Expect(LogType.Error, new Regex("is not a finite number of seconds"));

            Assert.AreEqual(TimeSpan.Zero, new SecondsToTimeSpanConverter().Convert(value));
        }

        [Test]
        public void Convert_BeyondTimeSpanRange_ClampsToTheNearestBound()
        {
            LogAssert.Expect(LogType.Error, new Regex("past what a TimeSpan holds"));

            Assert.AreEqual(TimeSpan.MaxValue, new SecondsToTimeSpanConverter().Convert(1e20f));
        }

        [Test]
        public void Convert_LongBeyondTimeSpanRange_ClampsToTheNearestBound()
        {
            LogAssert.Expect(LogType.Error, new Regex("past what a TimeSpan holds"));

            Assert.AreEqual(TimeSpan.MaxValue, new SecondsToTimeSpanConverter().Convert(long.MaxValue));
        }

        private static T Back<T>(TimeSpan value) =>
            ((ITwoWayConverter<T, TimeSpan>)(object)new SecondsToTimeSpanConverter()).ConvertBack(value);
    }
}
