using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="RemapNumberConverter"/> — mapping between two ranges, the default
    /// clamp and its unclamped extrapolation, and the degenerate-range and integer-overload behavior.
    /// </summary>
    [TestFixture]
    public sealed class RemapNumberConverterTests
    {
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
        public void Remap_Int_RoundTrips()
        {
            var converter = (ITwoWayConverter<int, int>)new RemapNumberConverter(0f, 200f, 0f, 1000f);

            Assert.AreEqual(500, converter.Convert(100));
            Assert.AreEqual(100, converter.ConvertBack(500));
        }

        // An extrapolating range leaves int behind entirely; saturating keeps the answer the nearest
        // int rather than the platform-dependent result of an unchecked cast.
        [Test]
        public void Remap_Int_OutOfRange_SaturatesAtTheBound() =>
            Assert.AreEqual(
                int.MaxValue,
                ((ITwoWayConverter<int, int>)new RemapNumberConverter(0f, 1e10f, 0f, 1f, clamp: false)).ConvertBack(1));

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
    }
}
