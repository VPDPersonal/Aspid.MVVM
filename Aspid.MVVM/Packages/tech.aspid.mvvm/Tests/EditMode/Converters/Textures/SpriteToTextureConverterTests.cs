using UnityEngine;
using NUnit.Framework;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Coverage for <see cref="SpriteToTextureConverter"/> — reading the backing texture and the
    /// null path.
    /// </summary>
    [TestFixture]
    public sealed class SpriteToTextureConverterTests : SceneFixture
    {
        [Test]
        public void SpriteToTexture_ReadsTheTexture()
        {
            var texture = Track(new Texture2D(4, 4));
            var sprite = Track(Sprite.Create(texture, new Rect(0, 0, 4, 4), Vector2.one * 0.5f));

            Assert.AreSame(texture, new SpriteToTextureConverter().Convert(sprite));
            Assert.IsNull(new SpriteToTextureConverter().Convert(null));
        }
    }
}
