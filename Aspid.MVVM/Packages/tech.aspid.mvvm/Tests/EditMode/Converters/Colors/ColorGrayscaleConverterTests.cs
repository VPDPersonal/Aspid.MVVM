using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorGrayscaleConverter"/> — the luminance weighting and the
    /// preserved alpha.
    /// </summary>
    [TestFixture]
    public sealed class ColorGrayscaleConverterTests
    {
        [Test]
        public void ColorGrayscale_UsesLuminanceWeightsNotAFlatAverage()
        {
            // A flat average of pure green would be 0.333; the eye reads it as 0.587.
            var result = new ColorGrayscaleConverter(0f).Convert(Color.green);

            Assert.AreEqual(0.587f, result.r, 1e-3f);
            Assert.AreEqual(result.r, result.g, 1e-6f);
            Assert.AreEqual(result.r, result.b, 1e-6f);
        }

        [Test]
        public void ColorGrayscale_KeepsAlpha() =>
            Assert.AreEqual(0.4f, new ColorGrayscaleConverter(0f).Convert(new Color(1f, 0f, 0f, 0.4f)).a, 1e-5f);
    }
}
