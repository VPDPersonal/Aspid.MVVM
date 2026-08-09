#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the alpha of every colour in a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>Dimming a whole interactive element without touching its hues.</remarks>
    [Serializable]
    public sealed class ColorBlockAlphaConverter : IConverterColorBlock
    {
        [Tooltip("The alpha applied to every state.")]
        [SerializeField, Range(0f, 1f)] private float _alpha = 1f;

        [Tooltip("How the alpha is applied.")]
        [SerializeField] private AlphaMode _mode = AlphaMode.Multiply;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBlockAlphaConverter"/> class at full opacity.
        /// </summary>
        public ColorBlockAlphaConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBlockAlphaConverter"/> class.
        /// </summary>
        /// <param name="alpha">The alpha applied to every state.</param>
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
        /// <returns>The adjusted block.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            var alpha = new ColorAlphaConverter(_alpha, _mode);

            value.normalColor = alpha.Convert(value.normalColor);
            value.highlightedColor = alpha.Convert(value.highlightedColor);
            value.pressedColor = alpha.Convert(value.pressedColor);
            value.selectedColor = alpha.Convert(value.selectedColor);
            value.disabledColor = alpha.Convert(value.disabledColor);

            return value;
        }
    }
}
