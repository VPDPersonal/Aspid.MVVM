#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the texture a <see cref="Sprite"/> is drawn from.
    /// </summary>
    /// <remarks>
    /// A ViewModel holding a <see cref="Sprite"/> could not feed a <c>RawImage</c>, which wants a
    /// <see cref="Texture"/> — two properties for one picture.
    /// </remarks>
    [Serializable]
    public sealed class SpriteToTextureConverter : IConverter<Sprite?, Texture?>
    {
        /// <summary>
        /// Takes the texture of the specified sprite.
        /// </summary>
        /// <param name="value">The sprite to read.</param>
        /// <returns>Its texture, or <see langword="null"/> when the sprite is missing.</returns>
        public Texture? Convert(Sprite? value) => value == null ? null : value.texture;
    }
}
