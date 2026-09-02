using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="InverseLerpConverter"/> — locating a value's position in a range and
    /// the round trip through <c>ConvertBack</c>.
    /// </summary>
    [TestFixture]
    public sealed class InverseLerpConverterTests
    {
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
    }
}
