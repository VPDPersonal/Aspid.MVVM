using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="LerpNumberConverter"/> — positioning a value in a range and the
    /// integer overload's coarse round trip and saturation.
    /// </summary>
    [TestFixture]
    public sealed class LerpNumberConverterTests
    {
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
    }
}
