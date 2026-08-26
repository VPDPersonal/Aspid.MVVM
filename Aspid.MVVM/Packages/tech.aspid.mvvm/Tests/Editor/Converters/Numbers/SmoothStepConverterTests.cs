using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="SmoothStepConverter"/> — the endpoints, the midpoint, and the clamp on
    /// an out-of-range position.
    /// </summary>
    [TestFixture]
    internal sealed class SmoothStepConverterTests
    {
        [Test]
        public void Convert_ZeroPosition_ReachesFrom() =>
            Assert.AreEqual(10f, new SmoothStepConverter(10f, 20f).Convert(0f), 1e-5f);

        [Test]
        public void Convert_OnePosition_ReachesTo() =>
            Assert.AreEqual(20f, new SmoothStepConverter(10f, 20f).Convert(1f), 1e-5f);

        [Test]
        public void Convert_Midpoint_IsHalfway() =>
            Assert.AreEqual(15f, new SmoothStepConverter(10f, 20f).Convert(0.5f), 1e-5f);

        // There is no unclamped mode: a position outside 0..1 is held at the nearer end.
        [Test]
        public void Convert_OutOfRangePosition_IsHeldAtTheNearerEnd()
        {
            var converter = new SmoothStepConverter(10f, 20f);

            Assert.AreEqual(10f, converter.Convert(-1f), 1e-5f);
            Assert.AreEqual(20f, converter.Convert(2f), 1e-5f);
        }
        [Test]
        public void SmoothStep_Double_RunsTheSameCurveAsTheFloatWidth() =>
            Assert.AreEqual(
                new SmoothStepConverter(0f, 10f).Convert(0.25f),
                ((IConverter<double, double>)new SmoothStepConverter(0f, 10f)).Convert(0.25d),
                1e-6d);

    }
}
