#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the texture a <see cref="Sprite"/> is drawn from.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Texture",
        Name = "Sprite To Texture",
        Tooltip = "Takes the texture a Sprite is drawn from")]
    public sealed class SpriteToTextureConverter : IConverter<Sprite?, Texture?>
    {
        /// <summary>
        /// Takes the texture of the specified sprite.
        /// </summary>
        /// <param name="value">The sprite to read.</param>
        /// <returns>
        /// Its texture, or <see langword="null"/> when the sprite is missing or destroyed.
        /// </returns>
        public Texture? Convert(Sprite? value) => value == null 
            ? null
            : value.texture;
    }
}
