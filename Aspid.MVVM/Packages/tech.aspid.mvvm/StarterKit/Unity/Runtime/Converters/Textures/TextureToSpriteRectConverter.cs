#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures the pixel rect of a texture.
    /// </summary>
    /// <remarks>
    /// The rect <see cref="Sprite.Create(Texture2D, Rect, Vector2)"/> and the UV maths around an
    /// atlas both want, taken from the texture itself rather than authored beside it and left to go
    /// stale when the texture is replaced.
    /// <para>
    /// Typed on <see cref="Texture"/> rather than <see cref="Texture2D"/> because that is where
    /// <see cref="Texture.width"/> and <see cref="Texture.height"/> are declared: a
    /// <c>RawImage</c>-facing ViewModel holds the base type, and a <c>RenderTexture</c> measures the
    /// same way. A field typed for <see cref="Texture2D"/> still takes it, because
    /// <see cref="IConverter{TFrom, TTo}"/> is contravariant in its input.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Texture", Name = "Texture To Sprite Rect", Tooltip = "Measures the pixel rect of a texture")]
    public sealed class TextureToSpriteRectConverter : IConverter<Texture?, Rect>
    {
        /// <summary>
        /// Measures the specified texture.
        /// </summary>
        /// <param name="value">The texture to measure.</param>
        /// <returns>
        /// A rect covering the whole texture in pixels, or <see cref="Rect.zero"/> when the texture
        /// is missing.
        /// </returns>
        public Rect Convert(Texture? value) =>
            // Unity's overloaded == also catches a destroyed texture, whose width access would throw.
            value == null ? Rect.zero : new Rect(0f, 0f, value.width, value.height);
    }
}
