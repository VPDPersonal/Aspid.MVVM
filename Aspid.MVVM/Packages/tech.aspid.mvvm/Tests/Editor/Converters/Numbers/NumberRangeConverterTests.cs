using UnityEngine;
using NUnit.Framework;

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

        [TestCase(0.4f, 0.5f)]
        [TestCase(0.6f, 0.5f)]
        [TestCase(0.8f, 1f)]
        public void Snap_LandsOnTheNearestStep(float value, float expected) =>
            Assert.AreEqual(expected, new SnapToStepConverter(0.5f).Convert(value), delta: 1e-6f);

        [Test]
        public void Snap_ZeroStepPassesThrough() =>
            Assert.AreEqual(0.37f, new SnapToStepConverter(0f).Convert(0.37f), delta: 1e-6f);

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

        [Test]
        public void NormalizedToPercent_RoundTrips()
        {
            var converter = new NormalizedToPercentConverter();

            Assert.AreEqual(73.5f, converter.Convert(0.735f), delta: 1e-4f);
            Assert.AreEqual(0.735f, converter.ConvertBack(73.5f), delta: 1e-6f);
        }

        [Test]
        public void NormalizedToPercent_RoundsWhenAsked() =>
            Assert.AreEqual(74f, new NormalizedToPercentConverter(round: true).Convert(0.735f), delta: 1e-4f);

        [TestCase(WrapMode.Repeat, 1.25f, 0.25f)]
        [TestCase(WrapMode.Repeat, -0.25f, 0.75f)]
        [TestCase(WrapMode.PingPong, 1.25f, 0.75f)]
        public void Wrap_FoldsIntoTheRange(WrapMode mode, float value, float expected) =>
            Assert.AreEqual(expected, new WrapNumberConverter(mode, 0f, 1f).Convert(value), delta: 1e-5f);

        [Test]
        public void Wrap_DegenerateRangeYieldsItsLowEnd() =>
            Assert.AreEqual(5f, new WrapNumberConverter(WrapMode.Repeat, 5f, 5f).Convert(9f), delta: 1e-6f);

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

        [Test]
        public void AnimationCurve_EvaluatesTheCurve() =>
            Assert.AreEqual(0.5f, new AnimationCurveConverter(AnimationCurve.Linear(0f, 0f, 1f, 1f)).Convert(0.5f), delta: 1e-5f);

        [Test]
        public void AnimationCurve_WithoutACurvePassesThrough() =>
            Assert.AreEqual(0.37f, new AnimationCurveConverter(new AnimationCurve()).Convert(0.37f), delta: 1e-6f);

        // A linear slider wired straight to a mixer sounds wrong: half the slider is nearly silent.
        [Test]
        public void AudioLinearToDecibel_HalfVolumeIsAboutMinusSixDecibels() =>
            Assert.AreEqual(-6.02f, new AudioLinearToDecibelConverter().Convert(0.5f), delta: 0.05f);

        [Test]
        public void AudioLinearToDecibel_SilenceIsTheFloor() =>
            Assert.AreEqual(-80f, new AudioLinearToDecibelConverter().Convert(0f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_FullVolumeIsZero() =>
            Assert.AreEqual(0f, new AudioLinearToDecibelConverter().Convert(1f), delta: 1e-4f);

        [Test]
        public void AudioLinearToDecibel_RoundTrips()
        {
            var converter = new AudioLinearToDecibelConverter();

            Assert.AreEqual(0.5f, converter.ConvertBack(converter.Convert(0.5f)), delta: 1e-3f);
        }

        [Test]
        public void AudioDecibelToLinear_IsTheOtherDirection()
        {
            var toDecibels = new AudioLinearToDecibelConverter();
            var toLinear = new AudioDecibelToLinearConverter();

            Assert.AreEqual(0.5f, toLinear.Convert(toDecibels.Convert(0.5f)), delta: 1e-3f);
        }
    }
}
