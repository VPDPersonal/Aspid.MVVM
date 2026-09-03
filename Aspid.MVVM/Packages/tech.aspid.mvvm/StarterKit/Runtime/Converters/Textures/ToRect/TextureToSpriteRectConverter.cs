#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures the pixel rect of a texture.
    /// </summary>
    /// <remarks>
    /// Typed on <see cref="Texture"/> so a <see cref="RenderTexture"/> measures the same way; a
    /// <see cref="Texture2D"/> field still accepts it, the input is contravariant.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Texture/To Rect",
        Name = "Sprite Rect",
        Tooltip = "Measures the pixel rect of a texture")]
    public sealed class TextureToSpriteRectConverter : IConverter<Texture?, Rect>
    {
        /// <summary>
        /// Measures the specified texture.
        /// </summary>
        /// <param name="value">The texture to measure.</param>
        /// <returns>
        /// A rect covering the whole texture in pixels, or <see cref="Rect.zero"/> when the texture
        /// is missing or destroyed.
        /// </returns>
        public Rect Convert(Texture? value) => value == null
            ? Rect.zero
            : new Rect(0f, 0f, value.width, value.height);
    }
}
