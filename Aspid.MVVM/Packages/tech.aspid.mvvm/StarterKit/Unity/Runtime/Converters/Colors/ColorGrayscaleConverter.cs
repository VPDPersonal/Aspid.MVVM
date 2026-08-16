#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Desaturates a colour.
    /// </summary>
    /// <remarks>
    /// Greying out a locked item without a second sprite. The weights are the usual luminance
    /// coefficients, so the result matches what the eye reads as brightness rather than a flat
    /// channel average.
    /// </remarks>
    [Serializable]
    public sealed class ColorGrayscaleConverter : IConverterColor
    {
        [Tooltip("How much colour to keep. Zero is fully grey, one leaves the colour untouched.")]
        [SerializeField, Range(0f, 1f)] private float _saturation;

        /// <remarks>Default: fully desaturating.</remarks>
        public ColorGrayscaleConverter() { }

        /// <param name="saturation">How much colour to keep.</param>
        public ColorGrayscaleConverter(float saturation)
        {
            _saturation = saturation;
        }

        /// <summary>
        /// Desaturates the specified colour.
        /// </summary>
        /// <param name="value">The colour to desaturate.</param>
        /// <returns>The desaturated colour, with its alpha untouched.</returns>
        public Color Convert(Color value)
        {
            var luminance = value.r * 0.299f + value.g * 0.587f + value.b * 0.114f;
            var grey = new Color(luminance, luminance, luminance, value.a);

            return Color.Lerp(grey, value, Mathf.Clamp01(_saturation));
        }
    }
}
