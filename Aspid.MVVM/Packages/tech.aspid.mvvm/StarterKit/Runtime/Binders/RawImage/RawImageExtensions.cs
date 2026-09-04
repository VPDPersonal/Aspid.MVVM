using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="RawImage"/> used by the raw image binders.
    /// </summary>
    public static class RawImageExtensions
    {
        /// <summary>
        /// Sets <see cref="RawImage.texture"/> and, optionally, disables the image while the texture is
        /// <see langword="null"/>.
        /// </summary>
        /// <param name="image">The image to update.</param>
        /// <param name="texture">The texture to show, or <see langword="null"/> to clear it.</param>
        /// <param name="disableWhenNull">
        /// Whether <see cref="Behaviour.enabled"/> follows the presence of a texture.
        /// </param>
        public static void SetTexture(this RawImage image, Texture texture, bool disableWhenNull)
        {
            image.texture = texture;
            if (disableWhenNull) image.enabled = texture;
        }
    }
}
