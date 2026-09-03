#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the alpha of every color in a <see cref="ColorBlock"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Color Block Alpha",
        Tooltip = "Changes the alpha of every color in a ColorBlock")]
    public sealed class ColorBlockAlphaConverter : IConverter<ColorBlock, ColorBlock>
    {
        [Tooltip("The alpha applied to every state. The result is held to 0..1 whichever mode is used.")]
        [SerializeField] [Range(0f, 1f)] private float _alpha = 1f;

        [Tooltip("How the alpha is applied.")]
        [SerializeField] private AlphaMode _mode = AlphaMode.Multiply;

        /// <remarks>Default: scaling every state's alpha by one, which changes nothing.</remarks>
        public ColorBlockAlphaConverter() { }

        /// <param name="alpha">The alpha applied to every state. The result is held to 0..1 whichever mode is used.</param>
        /// <param name="mode">How the alpha is applied.</param>
        public ColorBlockAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Multiply)
        {
            _alpha = alpha;
            _mode = mode;
        }

        /// <summary>
        /// Changes the alpha of every state of the specified block.
        /// </summary>
        /// <param name="value">The block to adjust.</param>
        /// <returns>
        /// The adjusted block, every alpha held to 0..1. A mode that is not a declared
        /// <see cref="AlphaMode"/> value reports an error and every alpha is left as it arrived.
        /// </returns>
        public ColorBlock Convert(ColorBlock value)
        {
            value.normalColor = Fade(value.normalColor);
            value.highlightedColor = Fade(value.highlightedColor);
            value.pressedColor = Fade(value.pressedColor);
            value.selectedColor = Fade(value.selectedColor);
            value.disabledColor = Fade(value.disabledColor);

            return value;
        }

        private Color Fade(Color color) =>
            ColorAlphaConverter.Apply(this, color, _alpha, _mode);
    }
}
