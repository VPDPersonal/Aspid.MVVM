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
    /// Coverage for <see cref="RoundNumberConverter"/> — the round modes, the midpoint rule, decimal
    /// places, the integer and long overloads, and the misconfigured-mode guards.
    /// </summary>
    [TestFixture]
    public sealed class RoundNumberConverterTests
    {
        [TestCase(RoundMode.Round, 2.5f, 2f)]
        [TestCase(RoundMode.Round, 2.6f, 3f)]
        [TestCase(RoundMode.Floor, 2.9f, 2f)]
        [TestCase(RoundMode.Floor, -2.1f, -3f)]
        [TestCase(RoundMode.Ceil, 2.1f, 3f)]
        [TestCase(RoundMode.Truncate, -2.9f, -2f)]
        public void Round_DropsTheFractionAsAsked(RoundMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new RoundNumberConverter(mode).Convert(value), delta: 1e-6f);

        [Test]
        public void Round_KeepsTheRequestedDecimals() =>
            Assert.AreEqual(3.14f, new RoundNumberConverter(RoundMode.Round, digits: 2).Convert(3.14159f), delta: 1e-5f);

        [Test]
        public void Round_ToInt() =>
            Assert.AreEqual(3, ((IConverter<float, int>)new RoundNumberConverter(RoundMode.Round)).Convert(2.6f));

        // An exact half is the only input where ToEven and AwayFromZero can differ; 1.5 and 3.5 are
        // in the table because ToEven is not "always down" — half the time it agrees with AwayFromZero.
        [TestCase(MidpointRounding.ToEven, 0.5f, 0f)]
        [TestCase(MidpointRounding.ToEven, 1.5f, 2f)]
        [TestCase(MidpointRounding.ToEven, 2.5f, 2f)]
        [TestCase(MidpointRounding.ToEven, 3.5f, 4f)]
        [TestCase(MidpointRounding.ToEven, -2.5f, -2f)]
        [TestCase(MidpointRounding.AwayFromZero, 0.5f, 1f)]
        [TestCase(MidpointRounding.AwayFromZero, 1.5f, 2f)]
        [TestCase(MidpointRounding.AwayFromZero, 2.5f, 3f)]
        [TestCase(MidpointRounding.AwayFromZero, 3.5f, 4f)]
        [TestCase(MidpointRounding.AwayFromZero, -2.5f, -3f)]
        public void Round_Midpoint_DecidesWhichWayAnExactHalfGoes(MidpointRounding midpoint, float value, float expected) =>
            Assert.AreEqual(
                expected,
                new RoundNumberConverter(RoundMode.Round, digits: 0, midpoint: midpoint).Convert(value),
                delta: 1e-6f);

        // Only Round consults the midpoint rule; Floor of 2.5 stays 2 however emphatically the
        // midpoint says "away from zero".
        [TestCase(RoundMode.Floor, 2.5f, 2f)]
        [TestCase(RoundMode.Floor, -2.5f, -3f)]
        [TestCase(RoundMode.Ceil, 2.5f, 3f)]
        [TestCase(RoundMode.Truncate, -2.5f, -2f)]
        public void Round_Midpoint_IsIgnoredOutsideRoundMode(RoundMode mode, float value, float expected) =>
            Assert.AreEqual(
                expected,
                new RoundNumberConverter(mode, digits: 0, midpoint: MidpointRounding.AwayFromZero).Convert(value),
                delta: 1e-6f);

        // With digits the midpoint applies at the scaled place, not the units place: 0.125 * 100 is
        // exactly 12.5, a real half rather than a representation artefact.
        [TestCase(MidpointRounding.ToEven, 2, 0.125f, 0.12f)]
        [TestCase(MidpointRounding.AwayFromZero, 2, 0.125f, 0.13f)]
        [TestCase(MidpointRounding.ToEven, 1, 0.25f, 0.2f)]
        [TestCase(MidpointRounding.AwayFromZero, 1, 0.25f, 0.3f)]
        public void Round_Midpoint_AppliesAtTheScaledDecimalPlace(
            MidpointRounding midpoint,
            int digits,
            float value,
            float expected) =>
            Assert.AreEqual(
                expected,
                new RoundNumberConverter(RoundMode.Round, digits, midpoint: midpoint).Convert(value),
                delta: 1e-6f);

        [TestCase(MidpointRounding.ToEven, 2)]
        [TestCase(MidpointRounding.AwayFromZero, 3)]
        public void Round_ToInt_ConsultsTheMidpoint(MidpointRounding midpoint, int expected) =>
            Assert.AreEqual(expected, ToInt(RoundMode.Round, digits: 0, midpoint: midpoint).Convert(2.5f));

        // The digits field has nothing to scale on the way to an int, so it is ignored rather than
        // producing 250 from a shared scaling path.
        [Test]
        public void Round_ToInt_IgnoresTheDigits() =>
            Assert.AreEqual(3, ToInt(RoundMode.Round, digits: 2, midpoint: MidpointRounding.AwayFromZero).Convert(2.5f));

        // A plain (int) cast of an out-of-range float is undefined in C#; saturating makes the answer
        // the same on every platform.
        [TestCase(1e20f, int.MaxValue)]
        [TestCase(-1e20f, int.MinValue)]
        public void Round_ToInt_OutOfRange_Saturates(float value, int expected) =>
            Assert.AreEqual(expected, ToInt(RoundMode.Round, digits: 0, midpoint: MidpointRounding.ToEven).Convert(value));

        [Test]
        public void Round_ToInt_NaN_IsZero() =>
            Assert.AreEqual(0, ToInt(RoundMode.Round, digits: 0, midpoint: MidpointRounding.ToEven).Convert(float.NaN));

        // The long overload shares the int rule: the places have nothing to scale on the way to an
        // integer, so two digits still answer 3 rather than 250.
        [Test]
        public void Round_ToLong_IgnoresTheDigits()
        {
            var converter = (IConverter<double, long>)new RoundNumberConverter(
                RoundMode.Round,
                digits: 2,
                midpoint: MidpointRounding.AwayFromZero);

            Assert.AreEqual(3L, converter.Convert(2.5d));
        }

        // [Min(0)] holds the Inspector at zero, so a negative count only arrives from data authored
        // before the attribute; rounding to a whole number is the nearest thing to the request.
        [Test]
        public void Round_NegativeDigits_ReportsAndRoundsToAWholeNumber()
        {
            LogAssert.Expect(LogType.Error, new Regex("RoundNumberConverter.*decimal-place count -2 is negative"));

            Assert.AreEqual(3f, new RoundNumberConverter(RoundMode.Round, digits: -2).Convert(3.14159f), delta: 1e-6f);
        }

        [Test]
        public void Round_UndeclaredMode_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("RoundNumberConverter.*not a declared RoundMode"));

            Assert.AreEqual(3.14159f, new RoundNumberConverter((RoundMode)42).Convert(3.14159f), delta: 1e-6f);
        }

        // The decimal places are the reason the double width earns its keep: 3.14159 rounded to two
        // places lands exactly on 3.14 here, where the float overload would be a few ulps off.
        [Test]
        public void Round_Double_KeepsTheRequestedDecimals() =>
            Assert.AreEqual(
                3.14d,
                ((IConverter<double, double>)new RoundNumberConverter(RoundMode.Round, digits: 2)).Convert(3.14159d),
                1e-12d);

        // RoundNumberConverter implements the float-to-int conversion explicitly, so it is only
        // reachable through the interface.
        private static IConverter<float, int> ToInt(RoundMode mode, int digits, MidpointRounding midpoint) =>
            new RoundNumberConverter(mode, digits, midpoint);
    }
}
