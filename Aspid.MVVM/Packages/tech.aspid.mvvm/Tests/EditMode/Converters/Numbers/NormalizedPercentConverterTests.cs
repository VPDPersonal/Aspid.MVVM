using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="NormalizedPercentConverter"/> — both directions of the scaling, the
    /// rounding flag, and the two directions agreeing as mirrors of each other.
    /// </summary>
    [TestFixture]
    public sealed class NormalizedPercentConverterTests
    {
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

        [Test]
        public void NormalizedToPercent_Double_KeepsThePrecisionTheFloatWidthLoses() =>
            Assert.AreEqual(
                12.3456789d,
                ((IConverter<double, double>)new NormalizedPercentConverter(round: false)).Convert(0.123456789d),
                1e-9d);

        [Test]
        public void NormalizedToPercent_Double_ConvertBack_ReturnsTheFraction() =>
            Assert.AreEqual(
                0.25d,
                ((ITwoWayConverter<double, double>)new NormalizedPercentConverter(round: false)).ConvertBack(25d),
                1e-12d);

        // Inverted, Convert takes the percentage and hands back the fraction. An inverted body
        // returning 5000 for 50 would be rendered by a clamping consumer as a full bar —
        // indistinguishable from correct until the value drops below 100.
        [TestCase(50f, 0.5f)]
        [TestCase(0f, 0f)]
        [TestCase(100f, 1f)]
        [TestCase(12.5f, 0.125f)]
        public void PercentToNormalizedByFlag_Convert_DividesByOneHundred(float percent, float expected) =>
            Assert.AreEqual(expected, new NormalizedPercentConverter(round: false, isInvert: true).Convert(percent), delta: 1e-6f);

        // Documented as unclamped, and it has to stay that way: a caller who wants 0..1 enforced adds
        // a ClampNumberConverter, whereas a clamp baked in here could not be removed by configuration.
        [TestCase(150f, 1.5f)]
        [TestCase(-50f, -0.5f)]
        public void PercentToNormalizedByFlag_Convert_OutsideZeroToOneHundred_DoesNotClamp(float percent, float expected) =>
            Assert.AreEqual(expected, new NormalizedPercentConverter(round: false, isInvert: true).Convert(percent), delta: 1e-6f);

        [TestCase(0.5f, 50f)]
        [TestCase(0f, 0f)]
        [TestCase(1f, 100f)]
        [TestCase(2f, 200f)]
        public void PercentToNormalizedByFlag_ConvertBack_MultipliesByOneHundred(float normalized, float expected) =>
            Assert.AreEqual(expected, new NormalizedPercentConverter(round: false, isInvert: true).ConvertBack(normalized), delta: 1e-6f);

        [TestCase(37f)]
        [TestCase(0.5f)]
        [TestCase(-12.25f)]
        [TestCase(1000f)]
        public void PercentToNormalizedByFlag_ConvertBack_UndoesConvert(float percent)
        {
            var converter = new NormalizedPercentConverter(round: false, isInvert: true);

            Assert.AreEqual(percent, converter.ConvertBack(converter.Convert(percent)), delta: 1e-4f);
        }

        // The flag makes one class serve both directions, so the same value fed to both — once read
        // as a fraction, once as a percentage — has to match on the crossed methods.
        [TestCase(0f)]
        [TestCase(0.735f)]
        [TestCase(1f)]
        [TestCase(2.5f)]
        public void PercentToNormalizedByFlag_IsTheMirrorOfNormalizedToPercent(float value)
        {
            var percentToNormalized = new NormalizedPercentConverter(round: false, isInvert: true);
            var normalizedToPercent = new NormalizedPercentConverter();

            Assert.AreEqual(normalizedToPercent.Convert(value), percentToNormalized.ConvertBack(value), delta: 1e-4f);
            Assert.AreEqual(normalizedToPercent.ConvertBack(value), percentToNormalized.Convert(value), delta: 1e-6f);
        }

        [TestCase(0.25f)]
        [TestCase(0.735f)]
        [TestCase(-0.5f)]
        public void PercentToNormalizedByFlag_Convert_RecoversWhatNormalizedToPercentProduced(float normalized) =>
            Assert.AreEqual(
                normalized,
                new NormalizedPercentConverter(round: false, isInvert: true).Convert(new NormalizedPercentConverter().Convert(normalized)),
                delta: 1e-5f);

        // The mirror holds only for the unrounded form: rounding belongs to the percent, whichever
        // direction produces it, so a TwoWay pair built from the rounding converter quantises the
        // source to whole percents while the unrounded inverted form does not.
        [Test]
        public void PercentToNormalizedByFlag_Convert_AfterARoundedPercent_KeepsTheRoundedValue()
        {
            var rounding = new NormalizedPercentConverter(round: true);
            var converter = new NormalizedPercentConverter(round: false, isInvert: true);

            Assert.AreEqual(74f, rounding.Convert(0.735f), delta: 1e-4f);
            Assert.AreEqual(0.74f, converter.Convert(rounding.Convert(0.735f)), delta: 1e-5f);
            Assert.AreEqual(73.5f, converter.ConvertBack(0.735f), delta: 1e-4f);
        }

        // Rounding belongs to the percent whichever direction produces it, so on the inverted form
        // it lands in ConvertBack and leaves the fraction-producing Convert untouched.
        [Test]
        public void PercentToNormalizedByFlag_Rounding_AppliesToThePercentSide()
        {
            var converter = new NormalizedPercentConverter(round: true, isInvert: true);

            Assert.AreEqual(74f, converter.ConvertBack(0.735f), delta: 1e-4f);
            Assert.AreEqual(0.735f, converter.Convert(73.5f), delta: 1e-6f);
        }

        // A binder reaches ConvertBack only through the interface; an explicit implementation, or a
        // dropped interface, silently downgrades a TwoWay binding to an unconverted write-back.
        [Test]
        public void PercentToNormalizedByFlag_ConvertBack_IsReachableThroughTheTwoWayInterface() =>
            Assert.AreEqual(50f, new NormalizedPercentConverter(round: false, isInvert: true).ConvertBack(0.5f), delta: 1e-6f);
    }
}
