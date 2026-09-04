using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="Image"/> used by the image binders.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Sets <see cref="Image.sprite"/> and, optionally, disables the image while the sprite is
        /// <see langword="null"/>.
        /// </summary>
        /// <param name="image">The image to update.</param>
        /// <param name="sprite">The sprite to show, or <see langword="null"/> to clear it.</param>
        /// <param name="disableWhenNull">
        /// Whether <see cref="Behaviour.enabled"/> follows the presence of a sprite.
        /// </param>
        public static void SetSprite(this Image image, Sprite sprite, bool disableWhenNull)
        {
            image.sprite = sprite;
            if (disableWhenNull) image.enabled = sprite;
        }
    }
}
