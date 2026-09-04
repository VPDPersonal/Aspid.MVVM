#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Creates sprites from textures for the sprite binders and destroys the previous one.
    /// </summary>
    internal static class SpriteBinderHelper
    {
        /// <summary>
        /// Destroys <paramref name="oldSprite"/> and creates a full-texture sprite from <paramref name="texture"/>.
        /// </summary>
        /// <param name="oldSprite">The previously created sprite, or <see langword="null"/>.</param>
        /// <param name="texture">The texture to wrap, or <see langword="null"/> to create nothing.</param>
        /// <returns>
        /// The new sprite, or <see langword="null"/> when <paramref name="texture"/> is <see langword="null"/>.
        /// </returns>
        public static Sprite? CreateSprite(Sprite? oldSprite, Texture2D? texture)
        {
            if (oldSprite) Object.Destroy(oldSprite);

            return !texture
                ? null
                : Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
