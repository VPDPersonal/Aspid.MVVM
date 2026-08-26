using System;
using UnityEngine;
using NUnit.Framework;
using System.Reflection;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for the second catalogue wave — arithmetic, rounding and range mapping.
    /// </summary>
    /// <remarks>
    /// The range converters are asserted as round trips wherever they claim to be reversible, and the
    /// domain edges — a zero-width range, a negative square root, a zero step — get a row each,
    /// because those are the states a half-authored Inspector produces.
    /// </remarks>
    [TestFixture]
    internal sealed class NumberRangeConverterTests
    {
        [TestCase(NumberOperation.Modulo, 7d, 3d, 1d)]
        [TestCase(NumberOperation.Power, 2d, 3d, 8d)]
        [TestCase(NumberOperation.ReverseSubtract, 30d, 100d, 70d)]
        [TestCase(NumberOperation.ReverseDivide, 4d, 100d, 25d)]
        public void Arithmetic_NewOperations(NumberOperation operation, double value, double coefficient, double expected) =>
            Assert.AreEqual(
                expected,
                ((IConverter<double, double>)new ArithmeticNumberConverter(operation, coefficient)).Convert(value),
                delta: 1e-12);

        // C#'s % keeps the sign of the left operand, so -1 % 360 is -1 — never what a wrapped angle
        // wants.
        [Test]
        public void Arithmetic_ModuloIsNonNegative() =>
            Assert.AreEqual(
                359d,
                ((IConverter<double, double>)new ArithmeticNumberConverter(NumberOperation.Modulo, 360)).Convert(-1d),
                delta: 1e-12);

        [TestCase(NumberOperation.Power, 3d)]
        [TestCase(NumberOperation.ReverseSubtract, 100d)]
        [TestCase(NumberOperation.ReverseDivide, 100d)]
        public void Arithmetic_NewOperationsRoundTrip(NumberOperation operation, double coefficient)
        {
            var converter = (ITwoWayConverter<double, double>)new ArithmeticNumberConverter(operation, coefficient);

            Assert.AreEqual(4d, converter.ConvertBack(converter.Convert(4d)), delta: 1e-9);
        }

        // Modulo discards which multiple the value came from, so there is nothing to undo it with.
        [Test]
        public void Arithmetic_ModuloCannotBeUndone()
        {
            var converter = (ITwoWayConverter<double, double>)new ArithmeticNumberConverter(NumberOperation.Modulo, 360);

            Assert.AreEqual(90d, converter.ConvertBack(90d), delta: 1e-12);
        }

        [TestCase(ClampMode.Both, -1f, 0f)]
        [TestCase(ClampMode.Both, 2f, 1f)]
        [TestCase(ClampMode.Both, 0.5f, 0.5f)]
        [TestCase(ClampMode.Min, -1f, 0f)]
        [TestCase(ClampMode.Min, 2f, 2f)]
        [TestCase(ClampMode.Max, -1f, -1f)]
        [TestCase(ClampMode.Max, 2f, 1f)]
        public void Clamp_HoldsTheBound(ClampMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(0f, 1f, mode).Convert(value), delta: 1e-6f);

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

        // A double bound can name a number no int can hold. Saturating keeps the result at the end of
        // the range; a plain cast of 1e18 to int is undefined behavior that differs per runtime.
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

        // What a half-authored Inspector produces. Both bounds are consulted, so the pair really is
        // contradictory: reported, and clamped to the swapped range rather than to whichever bound
        // the two ifs happen to test first.
        [Test]
        public void Clamp_Int_Both_InvertedBounds_ReportsAndClampsToTheSwappedRange()
        {
            LogAssert.Expect(LogType.Error, new Regex("ClampNumberConverter.*minimum 10 is above the maximum 0"));

            Assert.AreEqual(5, new ClampNumberConverter(10d, 0d).Convert(5));
        }

        // A single-bound mode never reads the other bound, so a minimum above the untouched default
        // maximum is authoring rather than a contradiction: the authored bound stands, and nothing is
        // reported. Swapping here would raise the value to the maximum the mode was told to ignore.
        [TestCase(ClampMode.Min, 5d, 1d, 0, 5)]
        [TestCase(ClampMode.Max, 5d, 1d, 9, 1)]
        public void Clamp_Int_SingleBoundMode_KeepsTheAuthoredBound(
            ClampMode mode,
            double min,
            double max,
            int value,
            int expected) =>
            Assert.AreEqual(expected, new ClampNumberConverter(min, max, mode).Convert(value));

        // What a renamed or reordered enum leaves behind in an already-authored asset. Clamping to
        // nothing is the honest answer: falling back to Both would hold the value inside bounds the
        // asset never asked to apply.
        [Test]
        public void Clamp_UndeclaredMode_ReportsAndLetsTheValueThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("ClampNumberConverter.*not a declared ClampMode"));

            Assert.AreEqual(5f, new ClampNumberConverter(0d, 1d, (ClampMode)42).Convert(5f), delta: 1e-6f);
        }

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

        // An exact half is the only input where the two rules differ, and it is the input a score, a
        // price or a percentage hits constantly. 1.5 and 3.5 are in the table because ToEven is not
        // "always down" — half the time it agrees with AwayFromZero.
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
        // exact in binary and stay exact after scaling, so the half is a real half rather than a
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

        // The long overload is the int rule one type wider: the places have nothing to scale on the
        // way to an integer, so asking for two of them still answers 3 rather than 250.
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
        // before the attribute. Rounding to a whole number is the nearest thing to the request —
        // scaling by 10^-2 would silently round to hundreds instead.
        [Test]
        public void Round_NegativeDigits_ReportsAndRoundsToAWholeNumber()
        {
            LogAssert.Expect(LogType.Error, new Regex("RoundNumberConverter.*decimal-place count -2 is negative"));

            Assert.AreEqual(3f, new RoundNumberConverter(RoundMode.Round, digits: -2).Convert(3.14159f), delta: 1e-6f);
        }

        // An undeclared mode has no rounding to fall back on, so the fraction survives — the one
        // result that cannot be mistaken for a rounding this converter meant to perform.
        [Test]
        public void Round_UndeclaredMode_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("RoundNumberConverter.*not a declared RoundMode"));

            Assert.AreEqual(3.14159f, new RoundNumberConverter((RoundMode)42).Convert(3.14159f), delta: 1e-6f);
        }

        [TestCase(0.4f, 0.5f)]
        [TestCase(0.6f, 0.5f)]
        [TestCase(0.8f, 1f)]
        public void Snap_LandsOnTheNearestStep(float value, float expected) =>
            Assert.AreEqual(expected, new SnapToStepConverter(0.5f).Convert(value), delta: 1e-6f);

        // A step of zero snaps nothing, so it is reported on every push rather than passing for a
        // deliberate setting.
        [Test]
        public void Snap_ZeroStepPassesThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("SnapToStepConverter.*the step is zero"));

            Assert.AreEqual(0.37f, new SnapToStepConverter(0f).Convert(0.37f), delta: 1e-6f);
        }

        // The integer overload snaps in double and truncates what comes out, and an out-of-range
        // value saturates rather than taking the undefined (int) cast.
        [TestCase(7d, 5)]
        [TestCase(1e20d, int.MaxValue)]
        [TestCase(-1e20d, int.MinValue)]
        public void Snap_ToInt_SnapsThenSaturates(double value, int expected) =>
            Assert.AreEqual(expected, ((IConverter<double, int>)new SnapToStepConverter(5f)).Convert(value));

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

        // 0.5 and the domain guard are the interesting rows: Log2 of 1 and Log2 of 0 both answer 0, so
        // the guard is indistinguishable from a legitimate result.
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

        // Asin and Acos clamp where every other guarded function zeroes: a value a hair past 1 is a
        // rounding error on the way in, and the nearest legal answer is the right-angle case. The
        // Acos(-2) row proves it — a zero fallback would answer 0, the clamp answers pi.
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
        // it and substitutes zero. A NaN under Acos comes back as pi/2, a perfectly ordinary-looking
        // angle, not as an obvious zero.
        [TestCase(UnaryMathOperation.Asin, 0f)]
        [TestCase(UnaryMathOperation.Acos, 1.5707964f)]
        public void UnaryMath_InverseTrig_NaN_IsTreatedAsZero(UnaryMathOperation operation, float expected) =>
            Assert.AreEqual(expected, new UnaryMathConverter(operation).Convert(float.NaN), delta: 1e-5f);

        // The class remarks say the guarded functions "return zero or clamp ... rather than yielding
        // NaN or infinity". The guard is `value <= 0d`, and a NaN fails every comparison, so it falls
        // straight through to Math.Log. Atan has no guard at all. Pinned as the behavior.
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

        // The int overloads are explicit — two implicit Convert methods cannot differ only by return
        // type — so the call goes through the interface. Sqrt of 10 is 3.16, and the int overload
        // truncates toward zero rather than rounding.
        [Test]
        public void UnaryMath_Int_TruncatesTowardZero() =>
            Assert.AreEqual(
                3,
                ((IConverter<int, int>)new UnaryMathConverter(UnaryMathOperation.Sqrt)).Convert(10));

        // Every declared operation answers something for 3, so an undeclared one returning the input
        // unchanged is the only result that tells the two apart.
        [Test]
        public void UnaryMath_UndeclaredOperation_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("UnaryMathConverter.*not a declared UnaryMathOperation"));

            Assert.AreEqual(3f, new UnaryMathConverter((UnaryMathOperation)42).Convert(3f), delta: 1e-6f);
        }

        [Test]
        public void Remap_MapsBetweenRanges() =>
            Assert.AreEqual(0.5f, new RemapNumberConverter(0f, 200f, 0f, 1f).Convert(100f), delta: 1e-6f);

        [Test]
        public void Remap_ClampsByDefault() =>
            Assert.AreEqual(1f, new RemapNumberConverter(0f, 200f, 0f, 1f).Convert(400f), delta: 1e-6f);

        [Test]
        public void Remap_ExtrapolatesWhenAsked() =>
            Assert.AreEqual(2f, new RemapNumberConverter(0f, 200f, 0f, 1f, clamp: false).Convert(400f), delta: 1e-6f);

        [Test]
        public void Remap_RoundTrips()
        {
            var converter = new RemapNumberConverter(0f, 200f, 10f, 20f);

            Assert.AreEqual(100f, converter.ConvertBack(converter.Convert(100f)), delta: 1e-4f);
        }

        // A zero-width incoming range is what a half-filled Inspector looks like.
        [Test]
        public void Remap_DegenerateRangeYieldsTheOutgoingLowEnd() =>
            Assert.AreEqual(7f, new RemapNumberConverter(5f, 5f, 7f, 9f).Convert(5f), delta: 1e-6f);

        // The int overloads are new surface, and every overload shares the one double pipeline — so a
        // whole-number map has to land back on the number it started on.
        [Test]
        public void Remap_Int_RoundTrips()
        {
            var converter = (ITwoWayConverter<int, int>)new RemapNumberConverter(0f, 200f, 0f, 1000f);

            Assert.AreEqual(500, converter.Convert(100));
            Assert.AreEqual(100, converter.ConvertBack(500));
        }

        // An extrapolating range leaves int behind entirely. Saturating is what keeps the answer the
        // nearest int rather than the platform-dependent result of an unchecked cast.
        [Test]
        public void Remap_Int_OutOfRange_SaturatesAtTheBound() =>
            Assert.AreEqual(
                int.MaxValue,
                ((ITwoWayConverter<int, int>)new RemapNumberConverter(0f, 1e10f, 0f, 1f, clamp: false)).ConvertBack(1));

        [Test]
        public void InverseLerp_LocatesTheValue() =>
            Assert.AreEqual(0.25f, new InverseLerpConverter(0f, 100f).Convert(25f), delta: 1e-6f);

        [Test]
        public void InverseLerp_RoundTrips()
        {
            var converter = new InverseLerpConverter(0f, 100f);

            Assert.AreEqual(25f, converter.ConvertBack(converter.Convert(25f)), delta: 1e-4f);
        }

        [Test]
        public void Lerp_PositionsInTheRange() =>
            Assert.AreEqual(25f, new LerpNumberConverter(0f, 100f).Convert(0.25f), delta: 1e-6f);

        // An int position is a coarse dial: truncation leaves 0 and 1 as the only positions there
        // are, so the round trip is asserted on the end the range actually reaches.
        [Test]
        public void Lerp_Int_RoundTripsOnTheEndOfTheRange()
        {
            var converter = (ITwoWayConverter<int, int>)new LerpNumberConverter(0f, 100f);

            Assert.AreEqual(100, converter.Convert(1));
            Assert.AreEqual(1, converter.ConvertBack(100));
        }

        // A hair-thin range makes the reverse pass explode: locating 1 in 0..1e-10 is four orders of
        // magnitude past int, and saturating holds it at the bound.
        [Test]
        public void Lerp_Int_ReverseOutOfRange_SaturatesAtTheBound() =>
            Assert.AreEqual(
                int.MaxValue,
                ((ITwoWayConverter<int, int>)new LerpNumberConverter(0f, 1e-10f, clamp: false)).ConvertBack(1));

        [Test]
        public void NormalizedToPercent_RoundTrips()
        {
            var converter = new NormalizedPercentConverter();

            Assert.AreEqual(73.5f, converter.Convert(0.735f), delta: 1e-4f);
            Assert.AreEqual(0.735f, converter.ConvertBack(73.5f), delta: 1e-6f);
        }

        [Test]
        public void NormalizedToPercent_RoundsWhenAsked() =>
            Assert.AreEqual(74f, new NormalizedPercentConverter(round: true).Convert(0.735f), delta: 1e-4f);

        [TestCase(NumberWrapMode.Repeat, 1.25f, 0.25f)]
        [TestCase(NumberWrapMode.Repeat, -0.25f, 0.75f)]
        [TestCase(NumberWrapMode.PingPong, 1.25f, 0.75f)]
        public void Wrap_FoldsIntoTheRange(NumberWrapMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new WrapNumberConverter(mode, 0f, 1f).Convert(value), delta: 1e-5f);

        [Test]
        public void Wrap_DegenerateRangeYieldsItsLowEnd() =>
            Assert.AreEqual(5f, new WrapNumberConverter(NumberWrapMode.Repeat, 5f, 5f).Convert(9f), delta: 1e-6f);

        // An int folds through the same double path: 12 over 0..10 comes back as 2, not clamped to 10.
        [Test]
        public void Wrap_Int_FoldsIntoTheRange()
        {
            var converter = (IConverter<int, int>)new WrapNumberConverter(NumberWrapMode.Repeat, 0f, 10f);

            Assert.AreEqual(2, converter.Convert(12));
        }

        // 9 is outside 0..1 under either declared mode, so an unchanged 9 is proof the fold was
        // skipped rather than performed with the wrong rule.
        [Test]
        public void Wrap_UndeclaredMode_ReportsAndReturnsTheValueUnchanged()
        {
            LogAssert.Expect(LogType.Error, new Regex("WrapNumberConverter.*not a declared NumberWrapMode"));

            Assert.AreEqual(9f, new WrapNumberConverter((NumberWrapMode)42, 0f, 1f).Convert(9f), delta: 1e-6f);
        }

        [TestCase(30f, 0.5f)]
        [TestCase(0f, 0f)]
        [TestCase(90f, 1f)]
        public void Countdown_ReportsWhatIsLeft(float secondsLeft, float expected) =>
            Assert.AreEqual(expected, new CountdownProgressConverter(60f).Convert(secondsLeft), delta: 1e-6f);

        [Test]
        public void Countdown_ReportsWhatIsGoneWhenAsked() =>
            Assert.AreEqual(0.5f, new CountdownProgressConverter(60f, elapsed: true).Convert(30f), delta: 1e-6f);

        [Test]
        public void Countdown_ZeroDurationIsAFinishedTimer() =>
            Assert.AreEqual(0f, new CountdownProgressConverter(0f).Convert(10f), delta: 1e-6f);

        // [Min(0f)] holds the Inspector at zero, so a negative duration is older authored data. A
        // finished timer is the safe read: dividing by it would drive a fill bar past its own end.
        [TestCase(false, 0f)]
        [TestCase(true, 1f)]
        public void Countdown_NegativeDuration_ReportsAndReadsAsFinished(bool elapsed, float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("CountdownProgressConverter.*duration -1 is negative"));

            Assert.AreEqual(expected, new CountdownProgressConverter(-1f, elapsed).Convert(10f), delta: 1e-6f);
        }

        [Test]
        public void AnimationCurve_EvaluatesTheCurve() =>
            Assert.AreEqual(0.5f, new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f)).Convert(0.5f), delta: 1e-5f);

        // The curve is the whole of what this converter does, so a keyless one is a broken converter
        // rather than a neutral setting: it passes the value through and says so every time.
        [Test]
        public void AnimationCurve_WithoutACurvePassesThrough()
        {
            LogAssert.Expect(LogType.Error, new Regex("AnimationCurveConverter.*no curve is assigned"));

            Assert.AreEqual(0.37f, new AnimationCurveConverter(new AnimationCurve()).Convert(0.37f), delta: 1e-6f);
        }

        // The input range is read only while the value is normalized, and an empty one has no
        // position to map to. Reading the curve at its start keeps the result on the curve; the
        // division it replaces would hand a NaN to whatever the curve drives.
        [Test]
        public void AnimationCurve_EmptyInputRange_ReportsAndReadsTheCurveAtItsStart()
        {
            var converter = new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f));
            SetField(converter, "_normalizeInput", true);
            SetField(converter, "_inputMin", 5f);
            SetField(converter, "_inputMax", 5f);

            LogAssert.Expect(LogType.Error, new Regex("AnimationCurveConverter.*input range is empty"));

            Assert.AreEqual(0f, converter.Convert(7f), delta: 1e-6f);
        }

        // A linear slider wired straight to a mixer sounds wrong: half the slider is nearly silent.
        [Test]
        public void AudioLinearToDecibel_HalfVolumeIsAboutMinusSixDecibels() =>
            Assert.AreEqual(-6.02f, new AudioLinearDecibelConverter().Convert(0.5f), delta: 0.05f);

        [Test]
        public void AudioLinearToDecibel_SilenceIsTheFloor() =>
            Assert.AreEqual(-80f, new AudioLinearDecibelConverter().Convert(0f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_FullVolumeIsZero() =>
            Assert.AreEqual(0f, new AudioLinearDecibelConverter().Convert(1f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_RoundTrips()
        {
            var converter = new AudioLinearDecibelConverter();

            Assert.AreEqual(0.5f, converter.ConvertBack(converter.Convert(0.5f)), delta: 1e-3f);
        }

        [Test]
        public void AudioLinearToDecibel_Inverted_IsTheOtherDirection()
        {
            var toDecibels = new AudioLinearDecibelConverter();
            var toLinear = new AudioLinearDecibelConverter(isInvert: true);

            Assert.AreEqual(0.5f, toLinear.Convert(toDecibels.Convert(0.5f)), delta: 1e-3f);
        }

        // Silence has to sit below full volume or the clamp collapses and every slider position
        // answers the same number — a fader that looks wired up and moves nothing. The default range
        // is what keeps the fader working; -6.02 dB at half volume is the ordinary -80..0 answer.
        [Test]
        public void AudioLinearToDecibel_RangeThatIsNotARange_ReportsAndUsesTheDefaultRange()
        {
            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));

            Assert.AreEqual(-6.02f, new AudioLinearDecibelConverter(0f, -80f).Convert(0.5f), delta: 0.05f);
        }

        // The pair is read on every push rather than once at load, so a broken range keeps saying so
        // instead of going quiet after the first conversion.
        [Test]
        public void AudioLinearToDecibel_RangeThatIsNotARange_ReportsOnEveryPush()
        {
            var converter = new AudioLinearDecibelConverter(0f, -80f);

            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));
            LogAssert.Expect(LogType.Error, new Regex("AudioLinearDecibelConverter.*decibel range is not a range"));

            converter.Convert(0.5f);
            converter.Convert(0.25f);
        }

        // RoundNumberConverter implements the float-to-int conversion explicitly, so it is only
        // reachable through the interface.
        private static IConverter<float, int> ToInt(RoundMode mode, int digits, MidpointRounding midpoint) =>
            new RoundNumberConverter(mode, digits, midpoint);

        // The curve converter's normalization settings are serialized only, so a test reaches them
        // the way the Inspector does.
        private static void SetField(object target, string name, object value)
        {
            var field = target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field, $"{target.GetType().Name} has no field {name}");
            field.SetValue(target, value);
        }

        // The double width goes through the double Map, so it keeps digits the float one cannot.
        [Test]
        public void InverseLerp_Double_LocatesTheValueInTheRange() =>
            Assert.AreEqual(
                0.25d,
                ((IConverter<double, double>)new InverseLerpConverter(0f, 100f)).Convert(25d),
                1e-12d);

        [Test]
        public void InverseLerp_Double_ConvertBack_ReturnsTheValueAtThatPosition() =>
            Assert.AreEqual(
                25d,
                ((ITwoWayConverter<double, double>)new InverseLerpConverter(0f, 100f)).ConvertBack(0.25d),
                1e-12d);

        [Test]
        public void CountdownProgress_Double_ReadsTheSameProgress() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new CountdownProgressConverter(10f)).Convert(5d),
                1e-12d);

        // The double widths below are explicit interface implementations, so nothing but a cast
        // reaches them — a width dropped from the base list would compile and simply stop existing.
        // Each is asserted at a double's tolerance rather than a float's, which is the whole point of
        // having it: 1e-12 is four orders past what the float overload could promise.
        [Test]
        public void Lerp_Double_PositionsInTheRange() =>
            Assert.AreEqual(
                25d,
                ((IConverter<double, double>)new LerpNumberConverter(0f, 100f)).Convert(0.25d),
                1e-12d);

        [Test]
        public void Lerp_Double_ConvertBack_LocatesThePosition() =>
            Assert.AreEqual(
                0.25d,
                ((ITwoWayConverter<double, double>)new LerpNumberConverter(0f, 100f)).ConvertBack(25d),
                1e-12d);

        [Test]
        public void Remap_Double_MapsBetweenRanges() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new RemapNumberConverter(0f, 200f, 0f, 1f)).Convert(100d),
                1e-12d);

        [Test]
        public void Remap_Double_ConvertBack_MapsTheOtherWay() =>
            Assert.AreEqual(
                100d,
                ((ITwoWayConverter<double, double>)new RemapNumberConverter(0f, 200f, 0f, 1f)).ConvertBack(0.5d),
                1e-12d);

        // The decimal places are the reason the double width earns its keep: 3.14159 rounded to two
        // places is 3.14 exactly here, where the float overload would land a few ulps off it.
        [Test]
        public void Round_Double_KeepsTheRequestedDecimals() =>
            Assert.AreEqual(
                3.14d,
                ((IConverter<double, double>)new RoundNumberConverter(RoundMode.Round, digits: 2)).Convert(3.14159d),
                1e-12d);

        [Test]
        public void Snap_Double_LandsOnTheNearestStep() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new SnapToStepConverter(0.5f)).Convert(0.4d),
                1e-12d);

        [Test]
        public void Wrap_Double_FoldsIntoTheRange() =>
            Assert.AreEqual(
                0.25d,
                ((IConverter<double, double>)new WrapNumberConverter(NumberWrapMode.Repeat, 0f, 1f)).Convert(1.25d),
                1e-12d);

        // AnimationCurve and Unity's audio math both evaluate in float, so these two widths narrow on
        // the way in and carry a float's precision back out. Asserted at a float's tolerance to say so.
        [Test]
        public void AnimationCurve_Double_EvaluatesTheCurve() =>
            Assert.AreEqual(
                0.5d,
                ((IConverter<double, double>)new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f)))
                    .Convert(0.5d),
                1e-5d);

        [Test]
        public void AudioLinearToDecibel_Double_ReadsTheSameCurve() =>
            Assert.AreEqual(
                -6.02d,
                ((IConverter<double, double>)new AudioLinearDecibelConverter()).Convert(0.5d),
                0.05d);

        [Test]
        public void AudioLinearToDecibel_Double_ConvertBack_ReadsTheSliderPosition() =>
            Assert.AreEqual(
                0.5d,
                ((ITwoWayConverter<double, double>)new AudioLinearDecibelConverter()).ConvertBack(-6.02d),
                1e-3d);

    }
}
