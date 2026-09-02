using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorToColorBlockConverter"/> — the derived states, authored
    /// multipliers, and the multiplier range guard.
    /// </summary>
    [TestFixture]
    public sealed class ColorToColorBlockConverterTests
    {
        [Test]
        public void ColorToColorBlock_DerivesEveryState()
        {
            var block = new ColorToColorBlockConverter().Convert(Color.white);

            Assert.AreEqual(Color.white, block.normalColor);
            Assert.Less(block.pressedColor.r, block.normalColor.r);
            Assert.AreEqual(0.5f, block.disabledColor.a, 1e-5f);
        }

        [Test]
        public void ColorToColorBlock_AuthoredMultipliers_ScaleEachState()
        {
            var block = new ColorToColorBlockConverter(0.8f, pressedMultiplier: 0.4f, disabledAlpha: 0.2f)
                .Convert(Color.white);

            Assert.AreEqual(0.8f, block.highlightedColor.r, 1e-5f);
            Assert.AreEqual(0.4f, block.pressedColor.r, 1e-5f);
            Assert.AreEqual(0.2f, block.disabledColor.a, 1e-5f);
        }

        // UGUI renders a Selectable black at a zero multiplier, and the [Range] screens the field but
        // not the constructor argument. Held to the near end rather than to the default 1, so a value
        // above the range keeps the emphasis it was asking for.
        [TestCase(0f, 1f)]
        [TestCase(9f, 5f)]
        [TestCase(float.NaN, 1f)]
        public void ColorToColorBlock_MultiplierOutsideTheRange_ReportsItAndHoldsItToTheRange(
            float authored,
            float expected)
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorToColorBlockConverter.*color multiplier"));

            var block = new ColorToColorBlockConverter(1.1f, colorMultiplier: authored).Convert(Color.white);

            Assert.AreEqual(expected, block.colorMultiplier, 1e-5f);
        }
    }
}
