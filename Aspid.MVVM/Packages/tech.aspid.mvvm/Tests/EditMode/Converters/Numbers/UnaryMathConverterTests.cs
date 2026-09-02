using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="UnaryMathConverter"/> — the declared operations, the domain guards
    /// that return zero or clamp instead of yielding NaN, and the undeclared-operation guard.
    /// </summary>
    [TestFixture]
    public sealed class UnaryMathConverterTests
    {
        [TestCase(UnaryMathOperation.Abs, -3f, 3f)]
        [TestCase(UnaryMathOperation.Negate, 3f, -3f)]
        [TestCase(UnaryMathOperation.Sign, -3f, -1f)]
        [TestCase(UnaryMathOperation.Sign, 0f, 0f)]
        [TestCase(UnaryMathOperation.Sqrt, 9f, 3f)]
        [TestCase(UnaryMathOperation.Reciprocal, 4f, 0.25f)]
        [TestCase(UnaryMathOperation.Log10, 100f, 2f)]
        public void UnaryMath_AppliesTheFunction(UnaryMathOperation operation, float value, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(value), delta: 1e-5f);

        // A NaN reaching a Transform corrupts it silently; a zero is merely wrong.
        [TestCase(UnaryMathOperation.Sqrt, -1f)]
        [TestCase(UnaryMathOperation.Reciprocal, 0f)]
        [TestCase(UnaryMathOperation.Log, 0f)]
        [TestCase(UnaryMathOperation.Log10, -1f)]
        public void UnaryMath_OutsideTheDomainYieldsZeroNotNaN(UnaryMathOperation operation, float value) =>
            Assert.AreEqual(0f, new UnaryMathConverter(operation).Convert(value), delta: 1e-6f);

        // 0.5 and the domain guard are the interesting rows: Log2 of 1 and Log2 of 0 both answer 0,
        // so the guard is indistinguishable from a legitimate result.
        [TestCase(8f, 3f)]
        [TestCase(1024f, 10f)]
        [TestCase(1f, 0f)]
        [TestCase(0.5f, -1f)]
        [TestCase(0f, 0f)]
        [TestCase(-4f, 0f)]
        public void UnaryMath_Log2_TakesTheBaseTwoLogarithm(float value, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(UnaryMathOperation.Log2).Convert(value), delta: 1e-5f);

        // Log2 is Math.Log(value, 2) rather than Math.Log2, so the result is a division of two
        // logarithms and lands a few ulps off an exact power of two.
        [Test]
        public void UnaryMath_Log2_Double_MatchesAnExactPowerOfTwo() =>
            Assert.AreEqual(10d, new UnaryMathConverter(UnaryMathOperation.Log2).Convert(1024d), delta: 1e-12);

        [TestCase(UnaryMathOperation.Asin, 0f, 0f)]
        [TestCase(UnaryMathOperation.Asin, 1f, 1.5707964f)]
        [TestCase(UnaryMathOperation.Asin, -1f, -1.5707964f)]
        [TestCase(UnaryMathOperation.Acos, 1f, 0f)]
        [TestCase(UnaryMathOperation.Acos, 0f, 1.5707964f)]
        [TestCase(UnaryMathOperation.Acos, -1f, 3.1415927f)]
        [TestCase(UnaryMathOperation.Atan, 0f, 0f)]
        [TestCase(UnaryMathOperation.Atan, 1f, 0.7853982f)]
        [TestCase(UnaryMathOperation.Atan, -1f, -0.7853982f)]
        public void UnaryMath_InverseTrig_ReturnsRadians(UnaryMathOperation operation, float value, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(value), delta: 1e-5f);

        // Asin and Acos clamp where every other guarded function zeroes: a value a hair past 1 is a
        // rounding error on the way in, and the nearest legal answer is the right-angle case.
        [TestCase(UnaryMathOperation.Asin, 2f, 1.5707964f)]
        [TestCase(UnaryMathOperation.Asin, -2f, -1.5707964f)]
        [TestCase(UnaryMathOperation.Acos, 2f, 0f)]
        [TestCase(UnaryMathOperation.Acos, -2f, 3.1415927f)]
        public void UnaryMath_InverseTrig_OutsideTheDomain_ClampsToTheBoundary(
            UnaryMathOperation operation,
            float value,
            float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(value), delta: 1e-5f);

        // Asin and Acos are the only functions here that survive a NaN, because their clamp catches
        // it and substitutes zero.
        [TestCase(UnaryMathOperation.Asin, 0f)]
        [TestCase(UnaryMathOperation.Acos, 1.5707964f)]
        public void UnaryMath_InverseTrig_NaN_IsTreatedAsZero(UnaryMathOperation operation, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(float.NaN), delta: 1e-5f);

        // The domain guard is `value <= 0d`, and a NaN fails every comparison, so it falls straight
        // through to Math.Log. Atan has no guard at all.
        [TestCase(UnaryMathOperation.Log2)]
        [TestCase(UnaryMathOperation.Atan)]
        public void UnaryMath_NaN_IsNotCaughtByTheDomainGuard(UnaryMathOperation operation) =>
            Assert.IsTrue(
                float.IsNaN(new UnaryMathConverter(operation).Convert(float.NaN)),
                $"{operation} was expected to pass a NaN through unchanged.");

        // The same hole at the other end: the guard rejects zero and negatives, not an infinity.
        [Test]
        public void UnaryMath_Log2_Infinity_StaysInfinite() =>
            Assert.IsTrue(
                float.IsPositiveInfinity(new UnaryMathConverter(UnaryMathOperation.Log2).Convert(float.PositiveInfinity)),
                "Log2 was expected to pass a positive infinity through unchanged.");

        // The int overloads are explicit interface implementations, so the call goes through the
        // interface. Sqrt of 10 is 3.16, and the int overload truncates toward zero rather than rounding.
        [Test]
        public void UnaryMath_Int_TruncatesTowardZero() =>
            Assert.AreEqual(
                3,
                ((IConverter<int, int>)new UnaryMathConverter(UnaryMathOperation.Sqrt)).Convert(10));

        [Test]
        public void UnaryMath_UndeclaredOperation_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("UnaryMathConverter.*not a declared UnaryMathOperation"));

            Assert.AreEqual(3f, new UnaryMathConverter((UnaryMathOperation)42).Convert(3f), delta: 1e-6f);
        }
    }
}
