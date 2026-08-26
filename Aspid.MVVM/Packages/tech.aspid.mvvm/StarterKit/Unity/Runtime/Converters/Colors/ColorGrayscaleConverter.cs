#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Desaturates a color.
    /// </summary>
    /// <remarks>
    /// Gray is computed with the luminance weights, not a flat channel average.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Grayscale",
        Tooltip = "Desaturates a color")]
    public sealed class ColorGrayscaleConverter : IConverter<Color, Color>
    {
        [Tooltip("How much color to keep. Zero is fully gray, one leaves the color untouched.")]
        [SerializeField] [Range(0f, 1f)] private float _saturation;

        /// <remarks>Default: fully gray.</remarks>
        public ColorGrayscaleConverter() { }

        /// <param name="saturation">
        /// How much color to keep. Zero is fully gray, one leaves the color untouched; a value
        /// outside that range is held to it.
        /// </param>
        public ColorGrayscaleConverter(float saturation)
        {
            _saturation = saturation;
        }

        /// <summary>
        /// Desaturates the specified color.
        /// </summary>
        /// <param name="value">The color to desaturate.</param>
        /// <returns>The desaturated color, with its alpha untouched.</returns>
        public Color Convert(Color value)
        {
            var luminance = value.r * 0.299f + value.g * 0.587f + value.b * 0.114f;
            var gray = new Color(luminance, luminance, luminance, value.a);

            return Color.Lerp(gray, value, Mathf.Clamp01(_saturation));
        }
    }
}
