using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ModuloNumberConverter"/> — the euclidean default, the reported
    /// zero-divisor pass-through, and the int/long overloads' saturation and precision behavior.
    /// </summary>
    [TestFixture]
    public sealed class ModuloNumberConverterTests
    {
        // C#'s % takes the sign of the left operand, so -1 % 360 is -1 — never what a wrapped angle,
        // a cycling page index or an alternating row color wants. Both negative-divisor rows fold
        // upwards too: euclidean means non-negative, not "same sign as the divisor".
        [TestCase(3d, 7d, 1d)]
        [TestCase(360d, -1d, 359d)]
        [TestCase(-3d, -7d, 2d)]
        [TestCase(-3d, 7d, 1d)]
        public void Convert_Euclidean_IsNeverNegative(double divisor, double value, double expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value), delta: 1e-12);

        [TestCase(360d, -1d, -1d)]
        [TestCase(-3d, -7d, -1d)]
        public void Convert_WithoutEuclidean_KeepsTheValuesSign(double divisor, double value, double expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor, euclidean: false).Convert(value), delta: 1e-12);

        [Test]
        public void Convert_ZeroDivisor_ReportsAndPassesTheDoubleThrough()
        {
            ExpectZeroDivisor();

            Assert.AreEqual(7d, new ModuloNumberConverter(0d).Convert(7d), delta: 1e-12);
        }

        [Test]
        public void Convert_ZeroDivisor_ReportsAndPassesTheIntThrough()
        {
            ExpectZeroDivisor();

            Assert.AreEqual(7, new ModuloNumberConverter(0d).Convert(7));
        }

        [TestCase(2d, 5.5f, 1.5f)]
        [TestCase(360d, -1f, 359f)]
        public void Convert_Float_RoutesThroughTheDoublePath(double divisor, float value, float expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value), delta: 1e-5f);

        [TestCase(360d, -1, 359)]
        [TestCase(-3d, -7, 2)]
        [TestCase(3d, 7, 1)]
        public void Convert_Int_Euclidean_IsNeverNegative(double divisor, int value, int expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value));

        [Test]
        public void Convert_Int_WithoutEuclidean_KeepsTheValuesSign() =>
            Assert.AreEqual(-1, new ModuloNumberConverter(360d, euclidean: false).Convert(-1));

        // Why the long overload does its own arithmetic instead of borrowing the double one: 2^53 + 1
        // has no double, so the shared path would answer about the wrong number. The second assert is
        // the counter-example, and the two must not agree.
        [Test]
        public void Convert_Long_StaysExactWhereTheDoubleOverloadCannot()
        {
            var converter = new ModuloNumberConverter(2d);

            Assert.AreEqual(1L, converter.Convert(9007199254740993L));
            Assert.AreEqual(0d, converter.Convert(9007199254740993d), delta: 1e-12);
        }

        // The integer overloads take the whole-number part of the divisor — truncated, not rounded —
        // so 3.9 divides by 3 while the double overload divides by 3.9. Same converter, two answers.
        [Test]
        public void Convert_Int_TruncatesTheDivisor()
        {
            var converter = new ModuloNumberConverter(3.9d);

            Assert.AreEqual(1, converter.Convert(7));
            Assert.AreEqual(3.1d, converter.Convert(7d), delta: 1e-12);
        }

        // The trap that falls out of that truncation: any divisor below 1 becomes 0 on the integer
        // overloads, and a zero divisor means "pass through". An author who types 0.5 gets no modulo
        // at all on an int binding while the double binding wraps to zero — which is why each
        // integer overload reports it rather than passing the value on quietly.
        [Test]
        public void Convert_FractionalDivisorBelowOne_ReportsAndDisablesTheIntegerOverloads()
        {
            var converter = new ModuloNumberConverter(0.5d);

            ExpectNoWholePart();
            Assert.AreEqual(7, converter.Convert(7));

            ExpectNoWholePart();
            Assert.AreEqual(7L, converter.Convert(7L));

            Assert.AreEqual(0d, converter.Convert(7d), delta: 1e-12);
        }

        // The euclidean fold can push the result past int range even though the input fits: -1 folded
        // by a divisor of 1e18 is nearly 1e18. Casting that would wrap it round to a negative — the
        // exact fault this converter exists to prevent — so it saturates instead.
        [Test]
        public void Convert_Int_EuclideanFoldBeyondIntRange_Saturates() =>
            Assert.AreEqual(int.MaxValue, new ModuloNumberConverter(1e18d).Convert(-1));

        [Test]
        public void Convert_Long_EuclideanFoldBeyondIntRange_IsExact() =>
            Assert.AreEqual(999999999999999999L, new ModuloNumberConverter(1e18d).Convert(-1L));

        // long.MinValue % -1L faults on the hardware divide because the quotient does not fit; the
        // remainder is plainly zero and has to be answered directly rather than crashing.
        [Test]
        public void Convert_Long_DivisorOfNegativeOne_DoesNotFaultOnLongMinValue() =>
            Assert.AreEqual(0L, new ModuloNumberConverter(-1d).Convert(long.MinValue));

        private static void ExpectZeroDivisor() =>
            LogAssert.Expect(LogType.Error, new Regex("ModuloNumberConverter.*divisor is zero"));

        private static void ExpectNoWholePart() =>
            LogAssert.Expect(LogType.Error, new Regex("ModuloNumberConverter.*no whole-number part"));
    }
}
