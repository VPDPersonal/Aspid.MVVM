using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="AngleDifferenceConverter"/> — the shortest-way-round wrap at every
    /// boundary, the signed/unsigned split, and the default reference.
    /// </summary>
    [TestFixture]
    public sealed class AngleDifferenceConverterTests
    {
        // The whole reason the converter is not a subtraction. Rows 2 and 3 straddle zero, where the
        // plain difference reads ±340 for what is twenty degrees the other way.
        [TestCase(0f, 10f, 10f)]
        [TestCase(350f, 10f, 20f)]
        [TestCase(10f, 350f, -20f)]
        [TestCase(-170f, 170f, -20f)]
        [TestCase(170f, -170f, 20f)]
        // A full turn is no difference at all, however many times it was taken.
        [TestCase(0f, 360f, 0f)]
        [TestCase(0f, 720f, 0f)]
        [TestCase(45f, -315f, 0f)]
        public void Convert_Signed_TakesTheShortWayRound(float reference, float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference).Convert(value), 1e-3f);

        // The wrap across 180, where the sign changes hands. Exactly half a turn is reported as
        // +180 and not -180 — Mathf.DeltaAngle's fold is `> 180`, not `>=` — and a half turn taken
        // the other way (-180) folds onto that same +180. One degree past the boundary in either
        // direction is where the answer jumps the full 358° to the opposite sign.
        [TestCase(0f, 180f, 180f)]
        [TestCase(0f, -180f, 180f)]
        [TestCase(0f, 179f, 179f)]
        [TestCase(0f, 181f, -179f)]
        [TestCase(0f, -181f, 179f)]
        [TestCase(90f, 270f, 180f)]
        [TestCase(90f, 271f, -179f)]
        [TestCase(90f, 269f, 179f)]
        public void Convert_Signed_HalfATurnStaysPositiveAndFlipsOneDegreeLater(
            float reference,
            float value,
            float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference).Convert(value), 1e-3f);

        // Unsigned is the magnitude of the signed answer, so the ±179 pair collapses onto one number
        // and the half turn survives as 180 — the largest value this converter can ever report.
        [TestCase(0f, 181f, 179f)]
        [TestCase(0f, -181f, 179f)]
        [TestCase(0f, 180f, 180f)]
        [TestCase(0f, 190f, 170f)]
        [TestCase(10f, 350f, 20f)]
        [TestCase(350f, 10f, 20f)]
        public void Convert_Unsigned_ReportsHowFarOffWhicheverWay(float reference, float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter(reference, signed: false).Convert(value), 1e-3f);

        [TestCase(45f, 45f)]
        [TestCase(-45f, -45f)]
        [TestCase(350f, -10f)]
        public void Convert_DefaultConstructed_MeasuresFromZero(float value, float expected) =>
            Assert.AreEqual(expected, new AngleDifferenceConverter().Convert(value), 1e-3f);
    }
}
