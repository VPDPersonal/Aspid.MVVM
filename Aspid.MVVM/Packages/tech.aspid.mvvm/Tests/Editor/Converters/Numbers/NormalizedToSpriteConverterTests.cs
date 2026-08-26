using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit.Tests
{
    /// <summary>
    /// Coverage for <see cref="NormalizedToSpriteConverter"/> — the frame picked at each amount and
    /// the no-frames fallback.
    /// </summary>
    /// <remarks>
    /// The parameterless constructor was made private; a converter is always built with its frames
    /// named explicitly.
    /// </remarks>
    [TestFixture]
    internal sealed class NormalizedToSpriteConverterTests
    {
        private static Sprite NewSprite() =>
            Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);

        [Test]
        public void Convert_PicksTheFrameForTheAmount()
        {
            var first = NewSprite();
            var last = NewSprite();
            var converter = new NormalizedToSpriteConverter(new[] { first, last });

            Assert.AreSame(first, converter.Convert(0f));
            Assert.AreSame(last, converter.Convert(1f));
        }

        [Test]
        public void Convert_ClampsOutOfRangeAmounts()
        {
            var first = NewSprite();
            var last = NewSprite();
            var converter = new NormalizedToSpriteConverter(new[] { first, last });

            Assert.AreSame(first, converter.Convert(-1f));
            Assert.AreSame(last, converter.Convert(2f));
        }

        [Test]
        public void Convert_NoFrames_ReportsAndReturnsNull()
        {
            LogAssert.Expect(LogType.Error, new Regex("no frames are assigned"));

            Assert.IsNull(new NormalizedToSpriteConverter(null).Convert(0.5f));
        }
    }
}
