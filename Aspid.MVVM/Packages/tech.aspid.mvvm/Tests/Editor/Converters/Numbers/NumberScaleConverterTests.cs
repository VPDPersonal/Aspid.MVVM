using NUnit.Framework;

namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="PercentToNormalizedConverter"/> and
    /// <see cref="SumConstantThenScaleConverter"/> — the direction of the percentage scaling, the
    /// order in which the offset and the scale are applied, and the inverse of both.
    /// </summary>
    /// <remarks>
    /// Both classes are affine maps of float onto float, so every available mistake type-checks and still
    /// moves in the right direction in Play mode. The two that hide there are a percentage scaled the
    /// wrong way, and <c>(x + a) * b</c> reassociated into <c>x + (a * b)</c>, which agrees with the
    /// correct reading whenever <c>x</c> is zero or the scale is one — so every order-of-operations row
    /// is picked to make the two readings disagree. The reciprocal pairing with
    /// <see cref="NormalizedToPercentConverter"/> is asserted rather than assumed.
    /// </remarks>
    [TestFixture]
    internal sealed class NumberScaleConverterTests
    {
        // Convert takes the percentage and hands back the fraction. An inverted body returns 5000 for
        // 50, which a consumer that clamps (Image.fillAmount, a normalized slider) renders as a full
        // bar — indistinguishable from correct until the value drops below 100.
        [TestCase(50f, 0.5f)]
        [TestCase(0f, 0f)]
        [TestCase(100f, 1f)]
        [TestCase(12.5f, 0.125f)]
        public void PercentToNormalized_Convert_DividesByOneHundred(float percent, float expected) =>
            Assert.AreEqual(expected, new PercentToNormalizedConverter().Convert(percent), delta: 1e-6f);

        // Documented as unclamped, and it has to stay that way: a caller who wants 0..1 enforced adds a
        // ClampNumberConverter, whereas a clamp baked in here cannot be removed by configuration.
        [TestCase(150f, 1.5f)]
        [TestCase(-50f, -0.5f)]
        public void PercentToNormalized_Convert_OutsideZeroToOneHundred_DoesNotClamp(float percent, float expected) =>
            Assert.AreEqual(expected, new PercentToNormalizedConverter().Convert(percent), delta: 1e-6f);

        [TestCase(0.5f, 50f)]
        [TestCase(0f, 0f)]
        [TestCase(1f, 100f)]
        [TestCase(2f, 200f)]
        public void PercentToNormalized_ConvertBack_MultipliesByOneHundred(float normalized, float expected) =>
            Assert.AreEqual(expected, new PercentToNormalizedConverter().ConvertBack(normalized), delta: 1e-6f);

        // The interface contract this class opts into is ConvertBack(Convert(x)) == x. A slider bound
        // TwoWay to a percentage runs this loop on every drag frame, so any drift compounds.
        [TestCase(37f)]
        [TestCase(0.5f)]
        [TestCase(-12.25f)]
        [TestCase(1000f)]
        public void PercentToNormalized_ConvertBack_UndoesConvert(float percent)
        {
            var converter = new PercentToNormalizedConverter();

            Assert.AreEqual(percent, converter.ConvertBack(converter.Convert(percent)), delta: 1e-4f);
        }

        // The class exists as the other half of NormalizedToPercentConverter, so the two must agree on
        // every input: the same number is fed to both, once read as a fraction and once as a percentage,
        // and the crossed methods have to match. Swapping which converter a binding uses is only safe
        // while this holds.
        [TestCase(0f)]
        [TestCase(0.735f)]
        [TestCase(1f)]
        [TestCase(2.5f)]
        public void PercentToNormalized_IsTheMirrorOfNormalizedToPercent(float value)
        {
            var percentToNormalized = new PercentToNormalizedConverter();
            var normalizedToPercent = new NormalizedToPercentConverter();

            Assert.AreEqual(normalizedToPercent.Convert(value), percentToNormalized.ConvertBack(value), delta: 1e-4f);
            Assert.AreEqual(normalizedToPercent.ConvertBack(value), percentToNormalized.Convert(value), delta: 1e-6f);
        }

        [TestCase(0.25f)]
        [TestCase(0.735f)]
        [TestCase(-0.5f)]
        public void PercentToNormalized_Convert_RecoversWhatNormalizedToPercentProduced(float normalized) =>
            Assert.AreEqual(
                normalized,
                new PercentToNormalizedConverter().Convert(new NormalizedToPercentConverter().Convert(normalized)),
                delta: 1e-5f);

        // The mirror holds only for the unrounded partner. NormalizedToPercentConverter has a round
        // flag and this class has no counterpart to it, so a TwoWay pair built from the rounding
        // converter quantises the source to whole percents while a pair built from this one does not.
        // That is a behavioural difference, not an equivalence — the round trip below lands on 0.74.
        [Test]
        public void PercentToNormalized_Convert_AfterARoundedPercent_KeepsTheRoundedValue()
        {
            var rounding = new NormalizedToPercentConverter(round: true);
            var converter = new PercentToNormalizedConverter();

            Assert.AreEqual(74f, rounding.Convert(0.735f), delta: 1e-4f);
            Assert.AreEqual(0.74f, converter.Convert(rounding.Convert(0.735f)), delta: 1e-5f);
            Assert.AreEqual(73.5f, converter.ConvertBack(0.735f), delta: 1e-4f);
        }

        // A binder reaches ConvertBack only through the interface; an explicit implementation, or a
        // dropped interface, silently downgrades a TwoWay binding to an unconverted write-back.
        [Test]
        public void PercentToNormalized_ConvertBack_IsReachableThroughTheTwoWayInterface() =>
            Assert.AreEqual(
                50f,
                ((ITwoWayConverter<float, float>)new PercentToNormalizedConverter()).ConvertBack(0.5f),
                delta: 1e-6f);

        // The reason the class is worth having as one node. Each row disagrees with x + (a * b):
        // 30 vs 21, 9 vs 1, 2.5 vs 3.5, 0 vs 18, -21 vs 9.
        [TestCase(1f, 2f, 10f, 30f)]
        [TestCase(4f, -1f, 3f, 9f)]
        [TestCase(2f, 3f, 0.5f, 2.5f)]
        [TestCase(-3f, 3f, 7f, 0f)]
        [TestCase(10f, 0.5f, -2f, -21f)]
        public void SumConstantThenScale_Convert_AddsTheOffsetBeforeScaling(float value, float offset, float scale, float expected) =>
            Assert.AreEqual(expected, new SumConstantThenScaleConverter(offset, scale).Convert(value), delta: 1e-5f);

        // The scale field initialises to 1, not to default(float). A converter that started at scale 0
        // would flatten every value to nothing the instant it was picked in the Inspector, and the
        // author would blame the binding rather than the freshly added node.
        [TestCase(0f)]
        [TestCase(7.5f)]
        [TestCase(-7.5f)]
        public void SumConstantThenScale_DefaultConstructed_IsIdentity(float value) =>
            Assert.AreEqual(value, new SumConstantThenScaleConverter().Convert(value), delta: 1e-6f);

        [Test]
        public void SumConstantThenScale_ScaleOmitted_DefaultsToOne() =>
            Assert.AreEqual(9f, new SumConstantThenScaleConverter(5f).Convert(4f), delta: 1e-6f);

        [TestCase(7f, 0.1f, 3f)]
        [TestCase(1f, 2f, 10f)]
        [TestCase(-4f, -2.5f, 0.25f)]
        [TestCase(100f, 0f, -2f)]
        public void SumConstantThenScale_ConvertBack_UndoesConvert(float value, float offset, float scale)
        {
            var converter = new SumConstantThenScaleConverter(offset, scale);

            Assert.AreEqual(value, converter.ConvertBack(converter.Convert(value)), delta: 1e-4f);
        }

        // The inverse has its own order to get wrong: value / b - a, never (value - a) / b. The wrong
        // form round-trips perfectly whenever the offset is zero, so a round-trip test alone would let
        // it through — hence this fixed pair, where the two forms give 1 and 2.8.
        [Test]
        public void SumConstantThenScale_ConvertBack_DividesBeforeSubtractingTheOffset() =>
            Assert.AreEqual(1f, new SumConstantThenScaleConverter(2f, 10f).ConvertBack(30f), delta: 1e-6f);

        // Scale zero annihilates the input, offset included — the result is 0 and not the offset.
        [TestCase(5f)]
        [TestCase(-5f)]
        [TestCase(0f)]
        public void SumConstantThenScale_ZeroScale_FlattensEveryValue(float value) =>
            Assert.AreEqual(0f, new SumConstantThenScaleConverter(3f, 0f).Convert(value), delta: 1e-6f);

        // The zero-scale branch is what stops ConvertBack producing Infinity or NaN and sending it to a
        // Transform. The price is that the ITwoWayConverter contract is broken here: the forward pass
        // discarded the input, so the round trip lands on 0 rather than recovering 42. Asserted so the
        // guard is not "fixed" into a division, and so the drift is a documented result rather than a
        // surprise in the field.
        [Test]
        public void SumConstantThenScale_ZeroScale_ConvertBackReturnsTheInputUnchanged()
        {
            var converter = new SumConstantThenScaleConverter(3f, 0f);

            Assert.AreEqual(42f, converter.ConvertBack(42f), delta: 1e-6f);
            Assert.AreEqual(0f, converter.ConvertBack(converter.Convert(42f)), delta: 1e-6f);
        }

        // The class remark tells authors that x * b + a is reachable by dividing the offset by the
        // scale. If the order of operations ever flipped, this identity would stop holding and the
        // remark would start misconfiguring bindings: 3 * 2 + 5 == (3 + 5 / 2) * 2 == 11.
        [Test]
        public void SumConstantThenScale_OffsetDividedByScale_ExpressesScaleThenOffset() =>
            Assert.AreEqual(3f * 2f + 5f, new SumConstantThenScaleConverter(5f / 2f, 2f).Convert(3f), delta: 1e-5f);

        [Test]
        public void SumConstantThenScale_IsUsableThroughTheTwoWayInterface()
        {
            var converter = (ITwoWayConverter<float, float>)new SumConstantThenScaleConverter(2f, 10f);

            Assert.AreEqual(30f, converter.Convert(1f), delta: 1e-6f);
            Assert.AreEqual(1f, converter.ConvertBack(30f), delta: 1e-6f);
        }
    }
}
