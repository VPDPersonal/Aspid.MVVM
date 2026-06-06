#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shared sprite lifecycle helper used by the Image sprite binders to create a
    /// <see cref="Sprite"/> from a <see cref="Texture2D"/> and destroy the previously created one.
    /// </summary>
    internal static class SpriteBinderHelper
    {
        /// <summary>
        /// Destroys the previously created <paramref name="oldSprite"/> and creates a new
        /// <see cref="Sprite"/> from <paramref name="texture"/>.
        /// </summary>
        /// <param name="oldSprite">The previously created sprite to destroy, or <see langword="null"/>.</param>
        /// <param name="texture">The <see cref="Texture2D"/> to convert into a sprite, or <see langword="null"/> to create no sprite.</param>
        /// <returns>The newly created <see cref="Sprite"/>, or <see langword="null"/> when <paramref name="texture"/> is <see langword="null"/>.</returns>
        public static Sprite? CreateSprite(Sprite? oldSprite, Texture2D? texture)
        {
            if (oldSprite) Object.Destroy(oldSprite);

            return !texture
                ? null
                : Sprite.Create(texture, new Rect(0, 0, texture!.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}
