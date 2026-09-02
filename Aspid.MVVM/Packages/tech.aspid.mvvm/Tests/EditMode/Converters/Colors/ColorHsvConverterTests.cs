using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorHsvConverter"/> — the hue shift.
    /// </summary>
    [TestFixture]
    public sealed class ColorHsvConverterTests
    {
        [Test]
        public void ColorHsv_HalfATurnGivesTheOppositeHue()
        {
            var result = new ColorHsvConverter(0.5f).Convert(Color.red);

            Color.RGBToHSV(result, out var hue, out _, out _);
            Assert.AreEqual(0.5f, hue, 1e-3f);
        }
    }
}
