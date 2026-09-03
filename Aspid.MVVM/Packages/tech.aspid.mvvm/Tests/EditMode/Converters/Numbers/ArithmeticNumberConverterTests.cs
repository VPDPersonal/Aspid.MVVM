using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ArithmeticNumberConverter"/> — every <see cref="NumberOperation"/>
    /// branch, the divide-by-zero fallback, and the narrowing behavior of the twelve cross-type
    /// overloads.
    /// </summary>
    /// <remarks>
    /// Every conversion runs through the single <c>IConverter&lt;double, double&gt;</c>
    /// implementation and is then narrowed with saturation, so the int/long overloads truncate
    /// toward zero rather than round. All sixteen interfaces are implemented explicitly, which is
    /// why every call below goes through a cast.
    /// </remarks>
    [TestFixture]
    public sealed class ArithmeticNumberConverterTests
    {
        [TestCase(NumberOperation.Add, 5d)]
        [TestCase(NumberOperation.Subtract, 1d)]
        [TestCase(NumberOperation.Multiply, 6d)]
        [TestCase(NumberOperation.Divide, 1.5d)]
        public void Convert_Double_AppliesTheOperation(NumberOperation operation, double expected) =>
            Assert.AreEqual(expected, Double(operation, coefficient: 2).Convert(3d), delta: 1e-12);

        [Test]
        public void Convert_DefaultConstructed_IsAnIdentityForPlus() =>
            Assert.AreEqual(3d, Double(NumberOperation.Add, coefficient: 0).Convert(3d), delta: 1e-12);

        [Test]
        public void Convert_Division_ByZeroCoefficient_LogsAndReturnsTheInput()
        {
            LogAssert.Expect(LogType.Error, new Regex("division by zero coefficient"));

            Assert.AreEqual(7d, Double(NumberOperation.Divide, coefficient: 0).Convert(7d), delta: 1e-12);
        }

        [TestCase(NumberOperation.Modulo, 7d, 3d, 1d)]
        [TestCase(NumberOperation.Power, 2d, 3d, 8d)]
        [TestCase(NumberOperation.ReverseSubtract, 30d, 100d, 70d)]
        [TestCase(NumberOperation.ReverseDivide, 4d, 100d, 25d)]
        public void Convert_Double_AppliesModuloPowerAndReverseOperations(NumberOperation operation, double value, double coefficient, double expected) =>
            Assert.AreEqual(expected, Double(operation, coefficient).Convert(value), delta: 1e-12);

        // C#'s % keeps the sign of the left operand, so -1 % 360 is -1 — never what a wrapped angle
        // wants.
        [Test]
        public void Convert_ModuloIsNonNegative() =>
            Assert.AreEqual(359d, Double(NumberOperation.Modulo, coefficient: 360).Convert(-1d), delta: 1e-12);

        [TestCase(NumberOperation.Power, 3d)]
        [TestCase(NumberOperation.ReverseSubtract, 100d)]
        [TestCase(NumberOperation.ReverseDivide, 100d)]
        public void ConvertBack_UndoesPowerAndReverseOperations(NumberOperation operation, double coefficient)
        {
            var converter = (ITwoWayConverter<double, double>)Double(operation, coefficient);

            Assert.AreEqual(4d, converter.ConvertBack(converter.Convert(4d)), delta: 1e-9);
        }

        // Modulo discards which multiple the value came from, so there is nothing to undo it with.
        [Test]
        public void ConvertBack_ModuloCannotBeUndone()
        {
            var converter = (ITwoWayConverter<double, double>)Double(NumberOperation.Modulo, coefficient: 360);

            Assert.AreEqual(90d, converter.ConvertBack(90d), delta: 1e-12);
        }

        [Test]
        public void Convert_WidensIntToDouble() =>
            Assert.AreEqual(5d, Widen(NumberOperation.Add, coefficient: 2).Convert(3), delta: 1e-12);

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
            Assert.AreEqual(1.5f, NarrowFloat(NumberOperation.Divide, coefficient: 2).Convert(3d), delta: 1e-6f);

        // The double pipeline cannot represent every long, so a long round-trip is lossy above
        // 2^53 even when the operation is an identity.
        [Test]
        public void Convert_Long_LosesPrecisionAboveTwoToTheFiftyThree() =>
            Assert.AreEqual(9007199254740992L, Long(NumberOperation.Add, coefficient: 0).Convert(9007199254740993L));

        [Test]
        public void Convert_NarrowsNaNToZero() =>
            Assert.AreEqual(0, Narrow(NumberOperation.Add, coefficient: 0).Convert(double.NaN));

        [Test]
        public void Convert_NarrowsOverflowToIntMaxValue() =>
            Assert.AreEqual(int.MaxValue, Narrow(NumberOperation.Add, coefficient: 0).Convert(1e20d));

        [TestCase(NumberOperation.Add)]
        [TestCase(NumberOperation.Subtract)]
        [TestCase(NumberOperation.Multiply)]
        [TestCase(NumberOperation.Divide)]
        public void ConvertBack_RoundTripsEveryOperation(NumberOperation operation)
        {
            var converter = TwoWay(operation, coefficient: 4);

            Assert.AreEqual(0.75d, converter.ConvertBack(converter.Convert(0.75d)), delta: 1e-12);
        }

        // ConvertBack must divide rather than multiply a second time, or a x100 converter sends
        // 0.75 back as 7500 instead of undoing its own forward conversion.
        [Test]
        public void ConvertBack_MultiplyUndoesRatherThanCompounds()
        {
            var converter = TwoWay(NumberOperation.Multiply, coefficient: 100);

            Assert.AreEqual(75d, converter.Convert(0.75d), delta: 1e-12);
            Assert.AreEqual(0.75d, converter.ConvertBack(75d), delta: 1e-12);
        }

        [Test]
        public void ConvertBack_PlusUndoes() =>
            Assert.AreEqual(3d, TwoWay(NumberOperation.Add, 2).ConvertBack(5d), delta: 1e-12);

        [Test]
        public void ConvertBack_MinusUndoes() =>
            Assert.AreEqual(3d, TwoWay(NumberOperation.Subtract, 2).ConvertBack(1d), delta: 1e-12);

        [Test]
        public void ConvertBack_DivisionUndoes() =>
            Assert.AreEqual(6d, TwoWay(NumberOperation.Divide, 2).ConvertBack(3d), delta: 1e-12);

        [Test]
        public void Convert_RoundTripsFloat()
        {
            var converter = (ITwoWayConverter<float, float>)new ArithmeticNumberConverter(NumberOperation.Multiply, coefficient: 4);

            Assert.AreEqual(0.75f, converter.ConvertBack(converter.Convert(0.75f)), delta: 1e-6f);
        }

        [Test]
        public void Convert_RoundTripsInt()
        {
            var converter = (ITwoWayConverter<int, int>)new ArithmeticNumberConverter(NumberOperation.Add, coefficient: 7);

            Assert.AreEqual(5, converter.ConvertBack(converter.Convert(5)));
        }

        [Test]
        public void Convert_RoundTripsLong()
        {
            var converter = (ITwoWayConverter<long, long>)new ArithmeticNumberConverter(NumberOperation.Subtract, coefficient: 7);

            Assert.AreEqual(5L, converter.ConvertBack(converter.Convert(5L)));
        }

        private static ITwoWayConverter<double, double> TwoWay(NumberOperation operation, double coefficient) =>
            new ArithmeticNumberConverter(operation, coefficient);

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
