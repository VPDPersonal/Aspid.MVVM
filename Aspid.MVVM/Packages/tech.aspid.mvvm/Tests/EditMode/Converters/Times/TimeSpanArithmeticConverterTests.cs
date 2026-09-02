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
    /// Coverage for <see cref="TimeSpanArithmeticConverter"/> — the eight <see cref="NumberOperation"/>
    /// branches applied to a duration, the euclidean modulo, the tick-precision pipeline, and the
    /// division-by-zero guards.
    /// </summary>
    [TestFixture]
    public sealed class TimeSpanArithmeticConverterTests
    {
        [TestCase(NumberOperation.Plus, 30f, 60d, 90d)]
        [TestCase(NumberOperation.Minus, 30f, 60d, 30d)]
        // Subtracting past zero is allowed to go negative rather than clamping — a progress ring that
        // overran shows a negative duration, not a frozen one.
        [TestCase(NumberOperation.Minus, 90f, 60d, -30d)]
        // The total-minus-elapsed case the class exists for.
        [TestCase(NumberOperation.ReverseSubtract, 30f, 10d, 20d)]
        [TestCase(NumberOperation.ReverseSubtract, 10f, 60d, -50d)]
        // For Multiply and Division the operand is a plain factor, not a number of seconds.
        [TestCase(NumberOperation.Multiply, 2f, 60d, 120d)]
        [TestCase(NumberOperation.Multiply, 0f, 60d, 0d)]
        [TestCase(NumberOperation.Division, 2f, 60d, 30d)]
        [TestCase(NumberOperation.Division, -2f, 60d, -30d)]
        [TestCase(NumberOperation.Modulo, 60f, 90d, 30d)]
        [TestCase(NumberOperation.Modulo, 60f, 30d, 30d)]
        // Power and ReverseDivide read the duration as its number of seconds and the result back as
        // seconds, which is the only meaning they can have for a duration.
        [TestCase(NumberOperation.Power, 2f, 4d, 16d)]
        [TestCase(NumberOperation.ReverseDivide, 60f, 4d, 15d)]
        public void Convert_AppliesTheOperation(
            NumberOperation operation,
            float operand,
            double seconds,
            double expected) =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(expected),
                new TimeSpanArithmeticConverter(operation, operand).Convert(TimeSpan.FromSeconds(seconds)));

        // C#'s % keeps the sign of the left operand, so a naive implementation answers "-30s into the
        // cycle" for a duration behind the epoch it is measured from.
        [TestCase(60f)]
        [TestCase(-60f)]
        public void Convert_ModuloOfANegativeDuration_IsNonNegative(float operand) =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(30),
                new TimeSpanArithmeticConverter(NumberOperation.Modulo, operand).Convert(TimeSpan.FromSeconds(-90)));

        [Test]
        public void Convert_DefaultConstructed_LeavesTheDurationUnchanged() =>
            Assert.AreEqual(TimeSpan.FromSeconds(60), new TimeSpanArithmeticConverter().Convert(TimeSpan.FromSeconds(60)));

        // The point of holding the sum in ticks: a year is 31,536,000 seconds, and a float carries
        // about seven digits, so adding one second to it through a float number of seconds would
        // round the second away entirely.
        [Test]
        public void Convert_Plus_KeepsASecondAFloatOfSecondsWouldLose() =>
            Assert.AreEqual(
                TimeSpan.FromDays(365) + TimeSpan.FromSeconds(1),
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 1f).Convert(TimeSpan.FromDays(365)));

        // The operand is a number of seconds, not a whole one.
        [Test]
        public void Convert_Plus_KeepsAFractionalOperand() =>
            Assert.AreEqual(
                TimeSpan.FromSeconds(1.5),
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 0.5f).Convert(TimeSpan.FromSeconds(1)));

        // Left to TimeSpan.FromTicks the overflow would wrap into a negative duration, which is wrong
        // in a way that still looks like a plausible reading on a label.
        [Test]
        public void Convert_Multiply_PastWhatADurationHolds_SaturatesAtMaxValue() =>
            Assert.AreEqual(
                TimeSpan.MaxValue,
                new TimeSpanArithmeticConverter(NumberOperation.Multiply, 1e10f).Convert(TimeSpan.FromDays(1000)));

        [Test]
        public void Convert_Multiply_PastWhatADurationHolds_SaturatesAtMinValue() =>
            Assert.AreEqual(
                TimeSpan.MinValue,
                new TimeSpanArithmeticConverter(NumberOperation.Multiply, 1e10f).Convert(TimeSpan.FromDays(-1000)));

        // A negative base to a fractional exponent has no real result, and casting the NaN to long is
        // an arbitrary number of ticks rather than an error — so the operand is reported and the
        // duration comes back untouched.
        [Test]
        public void Convert_Power_ProducingNaN_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("has no real result"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(-4),
                new TimeSpanArithmeticConverter(NumberOperation.Power, 0.5f).Convert(TimeSpan.FromSeconds(-4)));
        }

        // Math.Pow(0, -1) is positive infinity, which lands on the same saturation guard as an overflow.
        [Test]
        public void Convert_Power_ProducingInfinity_SaturatesAtMaxValue() =>
            Assert.AreEqual(
                TimeSpan.MaxValue,
                new TimeSpanArithmeticConverter(NumberOperation.Power, -1f).Convert(TimeSpan.Zero));

        // The double the sum is held in runs out of precision before a tick count does: a duration a
        // hundred microseconds short of TimeSpan.MaxValue comes back 23 ticks short of where it went
        // in, with a zero operand and without tripping the saturation guard.
        [Test]
        public void Convert_ADurationNearMaxValue_LosesTicksToTheDoublePipeline() =>
            Assert.AreEqual(
                9223372036854774784L,
                new TimeSpanArithmeticConverter(NumberOperation.Plus, 0f).Convert(new TimeSpan(long.MaxValue - 1000L)).Ticks);

        [Test]
        public void Convert_Division_ByAZeroOperand_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(60),
                new TimeSpanArithmeticConverter(NumberOperation.Division, 0f).Convert(TimeSpan.FromSeconds(60)));
        }

        [Test]
        public void Convert_Modulo_ByAZeroOperand_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(60),
                new TimeSpanArithmeticConverter(NumberOperation.Modulo, 0f).Convert(TimeSpan.FromSeconds(60)));
        }

        // A misconfigured operand is reported every time it is hit rather than once per instance: a
        // report that stops arriving reads as a fixed setting, and the console is the only place the
        // wrong reading is explained.
        [Test]
        public void Convert_Division_ByAZeroOperand_LogsOnEveryConversion()
        {
            for (var index = 0; index < 3; index++)
                LogAssert.Expect(LogType.Error, new Regex("division by a zero operand"));

            var converter = new TimeSpanArithmeticConverter(NumberOperation.Division, 0f);
            converter.Convert(TimeSpan.FromSeconds(1));
            converter.Convert(TimeSpan.FromSeconds(2));
            converter.Convert(TimeSpan.FromSeconds(3));
        }

        // ReverseDivide divides by the bound duration rather than by the operand, so a zero duration
        // is the divide-by-zero here and the operand has nowhere to go.
        [Test]
        public void Convert_ReverseDivide_ByAZeroDuration_ReturnsTheDuration()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by a zero duration"));

            Assert.AreEqual(
                TimeSpan.Zero,
                new TimeSpanArithmeticConverter(NumberOperation.ReverseDivide, 60f).Convert(TimeSpan.Zero));
        }

        [Test]
        public void Convert_AnUndeclaredOperation_ReturnsTheDurationUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("TimeSpanArithmeticConverter.*not a declared"));

            Assert.AreEqual(
                TimeSpan.FromSeconds(60),
                new TimeSpanArithmeticConverter((NumberOperation)99, 1f).Convert(TimeSpan.FromSeconds(60)));
        }
    }
}
