using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="PowerNumberConverter"/> — the sign-preserving default, the
    /// zero-exponent and zero-input edge cases, the reversal through <c>ConvertBack</c>, and the
    /// integer overloads.
    /// </summary>
    [TestFixture]
    public sealed class PowerNumberConverterTests
    {
        [TestCase(2f, 3f, 9f)]
        [TestCase(3f, 2f, 8f)]
        [TestCase(0.5f, 9f, 3f)]
        public void Convert_RaisesToTheExponent(float exponent, float value, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(exponent).Convert(value), delta: 1e-4f);

        // The sign switch makes the curve odd rather than even, which is the whole point: a stat that
        // dips below zero keeps its direction instead of folding back up.
        [TestCase(true, -4f)]
        [TestCase(false, 4f)]
        public void Convert_PreserveSign_DecidesWhereANegativeBaseLands(bool preserveSign, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(2f, preserveSign).Convert(-2f), delta: 1e-4f);

        // Math.Pow of a negative base and a fractional exponent has no real answer. Raising the
        // magnitude and putting the sign back is what keeps a NaN out of a Transform.
        [Test]
        public void Convert_PreserveSign_FractionalExponentOfANegative_AvoidsNaN() =>
            Assert.AreEqual(-2f, new PowerNumberConverter(0.5f).Convert(-4f), delta: 1e-4f);

        [Test]
        public void Convert_WithoutPreserveSign_FractionalExponentOfANegative_IsNaN() =>
            Assert.IsTrue(
                float.IsNaN(new PowerNumberConverter(0.5f, preserveSign: false).Convert(-4f)),
                "A negative base with a fractional exponent was expected to yield a NaN.");

        // Undocumented and easy to trip over: the sign-preserving path short-circuits on a zero input
        // BEFORE it looks at the exponent, so it answers 0 where the arithmetic answer is 1. Turning
        // the flag off gives the Math.Pow answer.
        [TestCase(true, 0f)]
        [TestCase(false, 1f)]
        public void Convert_ZeroExponent_ZeroInput_DependsOnPreserveSign(bool preserveSign, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(0f, preserveSign).Convert(0f), delta: 1e-4f);

        // The same short circuit is what stops a negative exponent turning a zero into an infinity.
        [Test]
        public void Convert_WithoutPreserveSign_NegativeExponentOfZero_IsInfinite() =>
            Assert.IsTrue(
                float.IsPositiveInfinity(new PowerNumberConverter(-1f, preserveSign: false).Convert(0f)),
                "A zero raised to a negative exponent was expected to yield an infinity.");

        [Test]
        public void Convert_Double_KeepsTheSignToo() =>
            Assert.AreEqual(-8d, new PowerNumberConverter(3f).Convert(-2d), delta: 1e-9);

        [TestCase(9f, 3f)]
        [TestCase(-9f, -3f)]
        public void ConvertBack_TakesTheRoot(float value, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(2f).ConvertBack(value), delta: 1e-4f);

        [TestCase(2f, -3f)]
        [TestCase(3f, 2.5f)]
        public void ConvertBack_UndoesConvert(float exponent, float value)
        {
            var converter = new PowerNumberConverter(exponent);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), delta: 1e-4f);
        }

        // Without the sign preserved the forward pass throws the sign away, so the round trip comes
        // back positive. A TwoWay binding on a value that can go negative needs the flag on.
        [Test]
        public void ConvertBack_WithoutPreserveSign_RoundTripLosesTheSign()
        {
            var converter = new PowerNumberConverter(2f, preserveSign: false);

            Assert.AreEqual(3f, converter.ConvertBack(converter.Convert(-3f)), delta: 1e-4f);
        }

        // Every input maps to 1, so there is nothing to recover the original from; handing the value
        // back unchanged at least keeps a TwoWay binding from writing 1 into the ViewModel — and says
        // so on every write rather than degrading in silence.
        [Test]
        public void ConvertBack_ZeroExponent_ReturnsTheInput()
        {
            LogAssert.Expect(LogType.Error, new Regex("the exponent is zero"));

            Assert.AreEqual(5f, new PowerNumberConverter(0f).ConvertBack(5f), delta: 1e-6f);
        }

        // The int overloads are explicit — two implicit Convert methods cannot differ only by return
        // type — so the round trip goes through the interface.
        [Test]
        public void Convert_Int_RoundTripsThroughConvertBack()
        {
            var converter = (ITwoWayConverter<int, int>)new PowerNumberConverter(2f);

            Assert.AreEqual(9, converter.Convert(3));
            Assert.AreEqual(3, converter.ConvertBack(9));
        }

        // 100000 squared is 1e10, far past int.MaxValue. Saturating pins the result at the bound
        // instead of the answer a plain (int) cast leaves undefined.
        [Test]
        public void Convert_Int_OutOfRange_SaturatesAtTheBound() =>
            Assert.AreEqual(
                int.MaxValue,
                ((IConverter<int, int>)new PowerNumberConverter(2f)).Convert(100_000));
    }
}
