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
    /// Coverage for <see cref="ColorBlockFadeDurationConverter"/> — setting the duration in isolation
    /// and the negative-duration guard.
    /// </summary>
    [TestFixture]
    public sealed class ColorBlockFadeDurationConverterTests
    {
        [Test]
        public void ColorBlockFadeDuration_SetsOnlyTheDuration()
        {
            var block = ColorBlock.defaultColorBlock;
            var slowed = new ColorBlockFadeDurationConverter(0.5f).Convert(block);

            Assert.AreEqual(0.5f, slowed.fadeDuration, 1e-5f);
            Assert.AreEqual(block.normalColor, slowed.normalColor);
        }

        // A Selectable tween over a negative duration never finishes, so the state change is stuck
        // rather than instant — and nothing about the block says which duration it was handed.
        [Test]
        public void ColorBlockFadeDuration_NegativeDuration_IsReportedAndFadesInstantly()
        {
            LogAssert.Expect(LogType.Error, new Regex("ColorBlockFadeDurationConverter.*not a length of time"));

            var slowed = new ColorBlockFadeDurationConverter(-1f).Convert(ColorBlock.defaultColorBlock);

            Assert.AreEqual(0f, slowed.fadeDuration, 1e-5f);
        }
    }
}
