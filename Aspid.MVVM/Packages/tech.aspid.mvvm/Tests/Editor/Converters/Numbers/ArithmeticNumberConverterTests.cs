using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="ArithmeticNumberConverter"/> — four of the
    /// <see cref="NumberOperation"/> branches, the divide-by-zero fallback, and the narrowing
    /// behavior of the twelve cross-type overloads.
    /// </summary>
    /// <remarks>
    /// Every conversion runs through the single <c>IConverter&lt;double, double&gt;</c>
    /// implementation and is then narrowed with saturation, so the int/long overloads truncate
    /// toward zero rather than round. All sixteen interfaces are implemented explicitly, which is
    /// why every call below goes through a cast.
    /// </remarks>
    [TestFixture]
    internal sealed class ArithmeticNumberConverterTests
    {
        [TestCase(NumberOperation.Plus, 5d)]
        [TestCase(NumberOperation.Minus, 1d)]
        [TestCase(NumberOperation.Multiply, 6d)]
        [TestCase(NumberOperation.Division, 1.5d)]
        public void Convert_Double_AppliesTheOperation(NumberOperation operation, double expected) =>
            Assert.AreEqual(expected, Double(operation, coefficient: 2).Convert(3d), delta: 1e-12);

        [Test]
        public void Convert_DefaultConstructed_IsAnIdentityForPlus() =>
            Assert.AreEqual(3d, Double(NumberOperation.Plus, coefficient: 0).Convert(3d), delta: 1e-12);

        [Test]
        public void Convert_Division_ByZeroCoefficient_LogsAndReturnsTheInput()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by zero coefficient"));

            Assert.AreEqual(7d, Double(NumberOperation.Division, coefficient: 0).Convert(7d), delta: 1e-12);
        }

        [Test]
        public void Convert_WidensIntToDouble() =>
            Assert.AreEqual(5d, Widen(NumberOperation.Plus, coefficient: 2).Convert(3), delta: 1e-12);

        [TestCase(5d, 2)]
        [TestCase(-5d, -2)]
        public void Convert_NarrowsToInt_TruncatingTowardZero(double value, int expected) =>
            Assert.AreEqual(expected, Narrow(NumberOperation.Multiply, coefficient: 0.5).Convert(value));

        [TestCase(5d, 2L)]
        [TestCase(-5d, -2L)]
        public void Convert_NarrowsToLong_TruncatingTowardZero(double value, long expected) =>
            Assert.AreEqual(expected, NarrowLong(NumberOperation.Multiply, coefficient: 0.5).Convert(value));

        [Test]
        public void Convert_NarrowsToFloat_KeepingTheDoubleResult() =>
            Assert.AreEqual(1.5f, NarrowFloat(NumberOperation.Division, coefficient: 2).Convert(3d), delta: 1e-6f);

        // The double pipeline cannot represent every long, so a long round-trip is lossy above
        // 2^53 even when the operation is an identity.
        [Test]
        public void Convert_Long_LosesPrecisionAboveTwoToTheFiftyThree() =>
            Assert.AreEqual(9007199254740992L, Long(NumberOperation.Plus, coefficient: 0).Convert(9007199254740993L));

        [Test]
        public void Convert_NarrowsNaNToZero() =>
            Assert.AreEqual(0, Narrow(NumberOperation.Plus, coefficient: 0).Convert(double.NaN));

        [Test]
        public void Convert_NarrowsOverflowToIntMaxValue() =>
            Assert.AreEqual(int.MaxValue, Narrow(NumberOperation.Plus, coefficient: 0).Convert(1e20d));

        private static IConverter<double, double> Double(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

        private static IConverter<int, double> Widen(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

        private static IConverter<double, int> Narrow(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

        private static IConverter<double, long> NarrowLong(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

        private static IConverter<double, float> NarrowFloat(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

        private static IConverter<long, long> Long(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);
    }
}
