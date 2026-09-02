using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using Aspid.MVVM.StarterKit;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="Texture2DToSpriteConverter"/> — the sprite cache, the null path, and
    /// the pixels-per-unit guard.
    /// </summary>
    [TestFixture]
    public sealed class Texture2DToSpriteConverterTests : SceneFixture
    {
        // Sprite.Create allocates every call and a binder pushes on every notification, so without
        // the cache a bound avatar leaks a sprite per frame.
        [Test]
        public void Texture2DToSprite_ReusesTheSpriteWhileTheTextureIsUnchanged()
        {
            var texture = Track(new Texture2D(4, 4));
            var converter = new Texture2DToSpriteConverter();

            var first = converter.Convert(texture);
            var second = converter.Convert(texture);

            Assert.IsNotNull(first);
            Assert.AreSame(first, second);
        }

        [Test]
        public void Texture2DToSprite_NullClearsTheCache() =>
            Assert.IsNull(new Texture2DToSpriteConverter().Convert(null));

        // Sprite.Create divides the pixel rect by this, so zero would hand back a sprite of infinite
        // world size — a bound image that simply stops drawing, with nothing in the log about it.
        [Test]
        public void Texture2DToSprite_PixelsPerUnitNotAboveZero_IsReportedAndBuildsAt100()
        {
            LogAssert.Expect(LogType.Error, new Regex("Texture2DToSpriteConverter.*not a scale"));

            var texture = Track(new Texture2D(4, 4));
            var converter = new Texture2DToSpriteConverter(Vector2.one * 0.5f, 0f);

            var sprite = converter.Convert(texture);

            Assert.IsNotNull(sprite);
            Assert.AreEqual(100f, sprite.pixelsPerUnit, 1e-5f);
        }
    }
}
