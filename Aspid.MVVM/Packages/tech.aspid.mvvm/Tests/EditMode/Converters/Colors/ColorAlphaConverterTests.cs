using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorAlphaConverter"/> — setting versus multiplying the alpha, and the
    /// undeclared-mode guard.
    /// </summary>
    [TestFixture]
    public sealed class ColorAlphaConverterTests
    {
        [Test]
        public void ColorAlpha_SetsTheAlphaAndLeavesTheHue()
        {
            var result = new ColorAlphaConverter(0.5f).Convert(new Color(0.2f, 0.4f, 0.6f, 1f));

            Assert.AreEqual(0.5f, result.a, 1e-5f);
            Assert.AreEqual(0.2f, result.r, 1e-5f);
        }

        [Test]
        public void ColorAlpha_Multiplies() =>
            Assert.AreEqual(
                0.25f,
                new ColorAlphaConverter(0.5f, AlphaMode.Multiply).Convert(new Color(1f, 1f, 1f, 0.5f)).a,
                1e-5f);

        // The [Range] screens the serialized field but not a constructor argument, and the two other
        // modes clamp already — Set holding to 0..1 is what makes the three agree on their output.
        [TestCase(2f, 1f)]
        [TestCase(-1f, 0f)]
        public void ColorAlpha_Set_HoldsTheAlphaToTheZeroOneRange(float alpha, float expected) =>
            Assert.AreEqual(
                expected,
                new ColorAlphaConverter(alpha).Convert(new Color(0.2f, 0.4f, 0.6f, 0.5f)).a,
                1e-5f);

        // The hue is left alone whatever happens to the alpha, so an unchanged 0.5 alpha alongside
        // the original hue is the only reading that says the mode was skipped rather than applied.
        [Test]
        public void ColorAlpha_UndeclaredMode_ReportsItAndLeavesTheAlphaAlone()
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorAlphaConverter.*not a declared AlphaMode"));

            var result = new ColorAlphaConverter(0.25f, (AlphaMode)42).Convert(new Color(0.2f, 0.4f, 0.6f, 0.5f));

            Assert.AreEqual(0.5f, result.a, 1e-5f);
            Assert.AreEqual(0.2f, result.r, 1e-5f);
        }
    }
}
