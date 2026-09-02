using UnityEngine;
using NUnit.Framework;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="ColorBlockAlphaConverter"/> — dimming every state's alpha.
    /// </summary>
    [TestFixture]
    public sealed class ColorBlockAlphaConverterTests
    {
        [Test]
        public void ColorBlockAlpha_DimsEveryState()
        {
            var block = ColorBlock.defaultColorBlock;
            var dimmed = new ColorBlockAlphaConverter(0.5f).Convert(block);

            Assert.AreEqual(block.normalColor.a * 0.5f, dimmed.normalColor.a, 1e-5f);
            Assert.AreEqual(block.highlightedColor.a * 0.5f, dimmed.highlightedColor.a, 1e-5f);
        }
    }
}
