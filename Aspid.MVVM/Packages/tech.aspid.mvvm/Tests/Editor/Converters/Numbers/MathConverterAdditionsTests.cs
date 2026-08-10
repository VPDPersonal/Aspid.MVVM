using System;
using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the members added to <c>MathConverters.cs</c> in the third catalogue wave — the
    /// <see cref="ClampNumberConverter"/> integer overloads, the <see cref="RoundNumberConverter"/>
    /// midpoint rule, the four new <see cref="UnaryMathOperation"/> functions,
    /// <see cref="PowerNumberConverter"/> and <see cref="ModuloNumberConverter"/>.
    /// </summary>
    /// <remarks>
    /// Every case here is one a plausible one-line implementation gets wrong: a fractional bound
    /// truncated instead of rounded, so a minimum of 0.5 lets 0 through; a midpoint rule left at the
    /// framework default, so a score of 2.5 silently becomes 2; an integer modulo routed through a
    /// <see cref="double"/>, so a counter past 2^53 answers about the wrong number; a negative base
    /// raised to a fractional exponent, which is a NaN unless the sign is handled first.
    /// <para>
    /// The expectations were taken by running the arithmetic, not from the XML docs, and a few of them
    /// disagree with what those docs promise: a NaN or an infinity walks straight through the
    /// <see cref="UnaryMathConverter"/> domain guards, and <see cref="PowerNumberConverter"/> answers 0
    /// rather than 1 for a zero raised to the zeroth power. Each such test pins the behaviour and says
    /// so where it stands, so that a later fix has to change the test deliberately.
    /// </para>
    /// </remarks>
    [TestFixture]
    internal sealed class MathConverterAdditionsTests
    {
        #region ClampNumberConverter — integer overloads with fractional bounds

        // The bug this whole block exists for: the bounds are authored as doubles, so a minimum of
        // 0.5 has to round UP into the range. Truncating it toward zero would return 0 for the first
        // row and leave the value below the bound the converter promised to hold it above.
        [TestCase(0.5d, 10.5d, 0, 1)]
        [TestCase(0.5d, 10.5d, 1, 1)]
        [TestCase(0.5d, 10.5d, 10, 10)]
        [TestCase(0.5d, 10.5d, 11, 10)]
        // Mirror image: the maximum has to round DOWN. Truncating -0.5 toward zero gives 0, which is
        // outside the range — the one case where the two roundings visibly differ.
        [TestCase(-10.5d, -0.5d, 0, -1)]
        [TestCase(-10.5d, -0.5d, -11, -10)]
        public void Clamp_Int_FractionalBound_RoundsIntoTheRange(double min, double max, int value, int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(value));

        [TestCase(0.5d, 10.5d, 0L, 1L)]
        [TestCase(0.5d, 10.5d, 1L, 1L)]
        [TestCase(0.5d, 10.5d, 10L, 10L)]
        [TestCase(0.5d, 10.5d, 11L, 10L)]
        [TestCase(-10.5d, -0.5d, 0L, -1L)]
        [TestCase(-10.5d, -0.5d, -11L, -10L)]
        public void Clamp_Long_FractionalBound_RoundsIntoTheRange(double min, double max, long value, long expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(value));

        // The rounding belongs to the integer overloads only. The same converter asked for a double
        // hands back the bound as authored, which is what tells the two paths apart.
        [Test]
        public void Clamp_Double_FractionalBound_KeepsTheFraction() =>
            Assert.AreEqual(0.5d, new ClampNumberConverter(0.5d, 10.5d).Convert(0d), delta: 1e-12);

        [TestCase(ClampMode.Min, 0, 1)]
        [TestCase(ClampMode.Min, 100, 100)]
        [TestCase(ClampMode.Max, 0, 0)]
        [TestCase(ClampMode.Max, 11, 10)]
        public void Clamp_Int_SingleBoundMode_LeavesTheOtherEndAlone(ClampMode mode, int value, int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(0.5d, 10.5d, mode).Convert(value));

        // A double bound can name a number no int can hold. Saturating keeps the result at the end of
        // the range; a plain cast of 1e18 to int is undefined behaviour that differs per runtime.
        [TestCase(1e18d, 1e19d, int.MaxValue)]
        [TestCase(-1e19d, -1e18d, int.MinValue)]
        public void Clamp_Int_BoundBeyondIntRange_SaturatesInsteadOfWrapping(double min, double max, int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max).Convert(0));

        [Test]
        public void Clamp_Long_BoundBeyondLongRange_Saturates() =>
            Assert.AreEqual(long.MaxValue, new ClampNumberConverter(1e30d, 1e31d).Convert(0L));

        // The reason the integer overloads exist at all: an in-range value is returned as it came in,
        // never round-tripped through a double. 2^53 + 1 is the first long a double cannot name.
        [Test]
        public void Clamp_Long_InsideTheRange_StaysExactAboveTwoToTheFiftyThree() =>
            Assert.AreEqual(9007199254740993L, new ClampNumberConverter(0d, 1e30d).Convert(9007199254740993L));

        // What a half-authored Inspector produces. The minimum is tested first, so it wins and the
        // result sits above the maximum — worth pinning so a later reorder of the two ifs is noticed.
        [Test]
        public void Clamp_Int_InvertedBounds_MinimumWins() =>
            Assert.AreEqual(10, new ClampNumberConverter(10d, 0d).Convert(5));

        #endregion

        #region RoundNumberConverter — the midpoint rule

        // An exact half is the only input where the two rules differ, and it is the input a score, a
        // price or a percentage hits constantly. 1.5 and 3.5 are in the table because ToEven is not
        // "always down" — half the time it agrees with AwayFromZero, and a test that only looked at
        // 2.5 could be passed by an implementation that always rounds down.
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

        // Direction and midpoint answer different questions: only Round consults the rule. Floor of
        // 2.5 stays 2 however emphatically the midpoint says "away from zero".
        [TestCase(RoundMode.Floor, 2.5f, 2f)]
        [TestCase(RoundMode.Floor, -2.5f, -3f)]
        [TestCase(RoundMode.Ceil, 2.5f, 3f)]
        [TestCase(RoundMode.Truncate, -2.5f, -2f)]
        public void Round_Midpoint_IsIgnoredOutsideRoundMode(RoundMode mode, float value, float expected) =>
            Assert.AreEqual(
                expected,
                new RoundNumberConverter(mode, digits: 0, midpoint: MidpointRounding.AwayFromZero).Convert(value),
                delta: 1e-6f);

        // With digits the midpoint applies at the scaled place, not the units place. Both inputs are
        // exact in binary and stay exact after scaling, so the half is a real half and not a
        // representation artefact — 0.125 * 100 is 12.5 on the nose.
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

        // The digits field has nothing to scale on the way to an int; the tooltip says it is ignored
        // and it is. A shared code path that scaled first would answer 250 here.
        [Test]
        public void Round_ToInt_IgnoresTheDigits() =>
            Assert.AreEqual(3, ToInt(RoundMode.Round, digits: 2, midpoint: MidpointRounding.AwayFromZero).Convert(2.5f));

        // A plain (int) cast of an out-of-range float is undefined in C#, so the same scene would
        // round differently on two platforms. Saturating makes the answer the same everywhere.
        [TestCase(1e20f, int.MaxValue)]
        [TestCase(-1e20f, int.MinValue)]
        public void Round_ToInt_OutOfRange_Saturates(float value, int expected) =>
            Assert.AreEqual(expected, ToInt(RoundMode.Round, digits: 0, midpoint: MidpointRounding.ToEven).Convert(value));

        [Test]
        public void Round_ToInt_NaN_IsZero() =>
            Assert.AreEqual(0, ToInt(RoundMode.Round, digits: 0, midpoint: MidpointRounding.ToEven).Convert(float.NaN));

        #endregion

        #region UnaryMathConverter — Log2, Asin, Acos, Atan

        // 0.5 and the domain guard are the interesting rows: Log2 of 1 and Log2 of 0 both answer 0,
        // so the guard is indistinguishable from a legitimate result and a caller cannot tell them
        // apart. That is the design, not an accident, and it should not change unnoticed.
        [TestCase(8f, 3f)]
        [TestCase(1024f, 10f)]
        [TestCase(1f, 0f)]
        [TestCase(0.5f, -1f)]
        [TestCase(0f, 0f)]
        [TestCase(-4f, 0f)]
        public void UnaryMath_Log2_TakesTheBaseTwoLogarithm(float value, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(UnaryMathOperation.Log2).Convert(value), delta: 1e-5f);

        // Log2 is Math.Log(value, 2) rather than Math.Log2, so the result is a division of two
        // logarithms and lands a few ulps off an exact power of two. Asserted on the double overload
        // because that is where the error would be visible if it ever grew.
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

        // The two inverse trig functions clamp where every other guarded function zeroes: a value a
        // hair past 1 is a rounding error on the way in, and the nearest legal answer is the
        // right-angle case. The Acos(-2) row is the one that proves it — a zero fallback would answer
        // 0 there, the clamp answers pi.
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
        // it and substitutes zero. Note what that means for Acos: a NaN comes back as pi/2, a
        // perfectly ordinary-looking angle, not as an obvious zero.
        [TestCase(UnaryMathOperation.Asin, 0f)]
        [TestCase(UnaryMathOperation.Acos, 1.5707964f)]
        public void UnaryMath_InverseTrig_NaN_IsTreatedAsZero(UnaryMathOperation operation, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(float.NaN), delta: 1e-5f);

        // Contradicts the class remarks, which say the guarded functions "return zero or clamp
        // ... rather than yielding NaN or infinity". The guard is `value <= 0d`, and a NaN fails
        // every comparison, so it falls straight through to Math.Log. Atan has no guard at all.
        // Pinned as the behaviour; if the guards are ever tightened, this test is the reminder.
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

        #endregion

        #region PowerNumberConverter

        [TestCase(2f, 3f, 9f)]
        [TestCase(3f, 2f, 8f)]
        [TestCase(0.5f, 9f, 3f)]
        public void Power_RaisesToTheExponent(float exponent, float value, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(exponent).Convert(value), delta: 1e-4f);

        // The sign switch makes the curve odd rather than even, which is the whole point: a stat that
        // dips below zero keeps its direction instead of folding back up.
        [TestCase(true, -4f)]
        [TestCase(false, 4f)]
        public void Power_PreserveSign_DecidesWhereANegativeBaseLands(bool preserveSign, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(2f, preserveSign).Convert(-2f), delta: 1e-4f);

        // Math.Pow of a negative base and a fractional exponent has no real answer. Raising the
        // magnitude and putting the sign back is what keeps a NaN out of a Transform.
        [Test]
        public void Power_PreserveSign_FractionalExponentOfANegative_AvoidsNaN() =>
            Assert.AreEqual(-2f, new PowerNumberConverter(0.5f).Convert(-4f), delta: 1e-4f);

        [Test]
        public void Power_WithoutPreserveSign_FractionalExponentOfANegative_IsNaN() =>
            Assert.IsTrue(
                float.IsNaN(new PowerNumberConverter(0.5f, preserveSign: false).Convert(-4f)),
                "A negative base with a fractional exponent was expected to yield a NaN.");

        // Undocumented and easy to trip over: the sign-preserving path short-circuits on a zero input
        // BEFORE it looks at the exponent, so it answers 0 where the arithmetic answer is 1. Turning
        // the flag off gives the Math.Pow answer.
        [TestCase(true, 0f)]
        [TestCase(false, 1f)]
        public void Power_ZeroExponent_ZeroInput_DependsOnPreserveSign(bool preserveSign, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(0f, preserveSign).Convert(0f), delta: 1e-4f);

        // The same short circuit is what stops a negative exponent turning a zero into an infinity.
        [Test]
        public void Power_WithoutPreserveSign_NegativeExponentOfZero_IsInfinite() =>
            Assert.IsTrue(
                float.IsPositiveInfinity(new PowerNumberConverter(-1f, preserveSign: false).Convert(0f)),
                "A zero raised to a negative exponent was expected to yield an infinity.");

        [TestCase(9f, 3f)]
        [TestCase(-9f, -3f)]
        public void Power_ConvertBack_TakesTheRoot(float value, float expected) =>
            Assert.AreEqual(expected, new PowerNumberConverter(2f).ConvertBack(value), delta: 1e-4f);

        [TestCase(2f, -3f)]
        [TestCase(3f, 2.5f)]
        public void Power_RoundTrips(float exponent, float value)
        {
            var converter = new PowerNumberConverter(exponent);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), delta: 1e-4f);
        }

        // Without the sign preserved the forward pass throws the sign away, so the round trip comes
        // back positive. A TwoWay binding on a value that can go negative needs the flag on.
        [Test]
        public void Power_WithoutPreserveSign_RoundTripLosesTheSign()
        {
            var converter = new PowerNumberConverter(2f, preserveSign: false);

            Assert.AreEqual(3f, converter.ConvertBack(converter.Convert(-3f)), delta: 1e-4f);
        }

        // Every input maps to 1, so there is nothing to recover the original from; handing the value
        // back unchanged at least keeps a TwoWay binding from writing 1 into the ViewModel.
        [Test]
        public void Power_ZeroExponent_ConvertBack_ReturnsTheInput() =>
            Assert.AreEqual(5f, new PowerNumberConverter(0f).ConvertBack(5f), delta: 1e-6f);

        [Test]
        public void Power_Double_KeepsTheSignToo() =>
            Assert.AreEqual(-8d, new PowerNumberConverter(3f).Convert(-2d), delta: 1e-9);

        #endregion

        #region ModuloNumberConverter

        // C#'s % takes the sign of the left operand, so -1 % 360 is -1 — never what a wrapped angle,
        // a cycling page index or an alternating row colour wants. Both negative-divisor rows fold
        // upwards too: euclidean means non-negative, not "same sign as the divisor".
        [TestCase(3d, 7d, 1d)]
        [TestCase(360d, -1d, 359d)]
        [TestCase(-3d, -7d, 2d)]
        [TestCase(-3d, 7d, 1d)]
        public void Modulo_Euclidean_IsNeverNegative(double divisor, double value, double expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value), delta: 1e-12);

        [TestCase(360d, -1d, -1d)]
        [TestCase(-3d, -7d, -1d)]
        public void Modulo_WithoutEuclidean_KeepsTheValuesSign(double divisor, double value, double expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor, euclidean: false).Convert(value), delta: 1e-12);

        [Test]
        public void Modulo_ZeroDivisor_PassesTheDoubleThrough() =>
            Assert.AreEqual(7d, new ModuloNumberConverter(0d).Convert(7d), delta: 1e-12);

        [Test]
        public void Modulo_ZeroDivisor_PassesTheIntThrough() =>
            Assert.AreEqual(7, new ModuloNumberConverter(0d).Convert(7));

        [TestCase(2d, 5.5f, 1.5f)]
        [TestCase(360d, -1f, 359f)]
        public void Modulo_Float_RoutesThroughTheDoublePath(double divisor, float value, float expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value), delta: 1e-5f);

        [TestCase(360d, -1, 359)]
        [TestCase(-3d, -7, 2)]
        [TestCase(3d, 7, 1)]
        public void Modulo_Int_Euclidean_IsNeverNegative(double divisor, int value, int expected) =>
            Assert.AreEqual(expected, new ModuloNumberConverter(divisor).Convert(value));

        [Test]
        public void Modulo_Int_WithoutEuclidean_KeepsTheValuesSign() =>
            Assert.AreEqual(-1, new ModuloNumberConverter(360d, euclidean: false).Convert(-1));

        // Why the long overload does its own arithmetic instead of borrowing the double one: 2^53 + 1
        // has no double, so the shared path would answer about the wrong number. The second assert is
        // the counter-example, and the two must not agree.
        [Test]
        public void Modulo_Long_StaysExactWhereTheDoubleOverloadCannot()
        {
            var converter = new ModuloNumberConverter(2d);

            Assert.AreEqual(1L, converter.Convert(9007199254740993L));
            Assert.AreEqual(0d, converter.Convert(9007199254740993d), delta: 1e-12);
        }

        // The integer overloads take the whole-number part of the divisor — truncated, not rounded —
        // so 3.9 divides by 3 while the double overload divides by 3.9. Same converter, two answers.
        [Test]
        public void Modulo_Int_TruncatesTheDivisor()
        {
            var converter = new ModuloNumberConverter(3.9d);

            Assert.AreEqual(1, converter.Convert(7));
            Assert.AreEqual(3.1d, converter.Convert(7d), delta: 1e-12);
        }

        // The trap that falls out of that truncation: any divisor below 1 becomes 0 on the integer
        // overloads, and a zero divisor means "pass through". An author who types 0.5 gets no modulo
        // at all on an int binding while the double binding wraps to zero.
        [Test]
        public void Modulo_FractionalDivisorBelowOne_DisablesTheIntegerOverloads()
        {
            var converter = new ModuloNumberConverter(0.5d);

            Assert.AreEqual(7, converter.Convert(7));
            Assert.AreEqual(7L, converter.Convert(7L));
            Assert.AreEqual(0d, converter.Convert(7d), delta: 1e-12);
        }

        // The euclidean fold can push the result past int range even though the input fits: -1 folded
        // by a divisor of 1e18 is nearly 1e18. Casting that would wrap it round to a negative — the
        // exact fault this converter exists to prevent — so it saturates instead.
        [Test]
        public void Modulo_Int_EuclideanFoldBeyondIntRange_Saturates() =>
            Assert.AreEqual(int.MaxValue, new ModuloNumberConverter(1e18d).Convert(-1));

        [Test]
        public void Modulo_Long_EuclideanFoldBeyondIntRange_IsExact() =>
            Assert.AreEqual(999999999999999999L, new ModuloNumberConverter(1e18d).Convert(-1L));

        #endregion

        // RoundNumberConverter implements the float-to-int conversion explicitly, so it is only
        // reachable through the interface.
        private static IConverter<float, int> ToInt(RoundMode mode, int digits, MidpointRounding midpoint) =>
            new RoundNumberConverter(mode, digits, midpoint);
    }
}
