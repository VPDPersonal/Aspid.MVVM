using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorBlockTintConverter"/> — tinting every state of a
    /// <see cref="ColorBlock"/>.
    /// </summary>
    [TestFixture]
    public sealed class ColorBlockTintConverterTests
    {
        [Test]
        public void ColorBlockTint_TintsEveryState()
        {
            var block = new ColorToColorBlockConverter().Convert(Color.white);
            var tinted = new ColorBlockTintConverter(Color.red).Convert(block);

            Assert.AreEqual(0f, tinted.normalColor.g, 1e-5f);
            Assert.AreEqual(0f, tinted.pressedColor.g, 1e-5f);
            Assert.AreEqual(0f, tinted.disabledColor.g, 1e-5f);
        }
    }
}
