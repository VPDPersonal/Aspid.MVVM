using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorTintConverter"/> — the multiply and replace blend modes.
    /// </summary>
    [TestFixture]
    public sealed class ColorTintConverterTests
    {
        [Test]
        public void ColorTint_Multiplies() =>
            Assert.AreEqual(
                new Color(0.5f, 0f, 0f, 1f),
                new ColorTintConverter(Color.red).Convert(new Color(0.5f, 0.5f, 0.5f, 1f)));

        [Test]
        public void ColorTint_ReplaceKeepsTheOriginalAlpha()
        {
            var result = new ColorTintConverter(Color.red, ColorBlend.Replace).Convert(new Color(0f, 0f, 1f, 0.3f));

            Assert.AreEqual(1f, result.r, 1e-5f);
            Assert.AreEqual(0.3f, result.a, 1e-5f);
        }
    }
}
