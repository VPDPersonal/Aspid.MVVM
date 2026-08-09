#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shifts a colour in HSV space.
    /// </summary>
    /// <remarks>A palette of variations from one authored base colour.</remarks>
    [Serializable]
    public sealed class ColorHsvConverter : IConverterColor
    {
        [Tooltip("How far to rotate the hue, in turns. 0.5 is the opposite colour.")]
        [SerializeField] private float _hueShift;

        [Tooltip("Scales the saturation.")]
        [SerializeField] private float _saturationMultiplier = 1f;

        [Tooltip("Scales the brightness.")]
        [SerializeField] private float _valueMultiplier = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorHsvConverter"/> class that changes nothing.
        /// </summary>
        public ColorHsvConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorHsvConverter"/> class.
        /// </summary>
        /// <param name="hueShift">How far to rotate the hue, in turns.</param>
        /// <param name="saturationMultiplier">Scales the saturation.</param>
        /// <param name="valueMultiplier">Scales the brightness.</param>
        public ColorHsvConverter(float hueShift, float saturationMultiplier = 1f, float valueMultiplier = 1f)
        {
            _hueShift = hueShift;
            _saturationMultiplier = saturationMultiplier;
            _valueMultiplier = valueMultiplier;
        }

        /// <summary>
        /// Shifts the specified colour.
        /// </summary>
        /// <param name="value">The colour to shift.</param>
        /// <returns>The shifted colour, with its alpha untouched.</returns>
        public Color Convert(Color value)
        {
            Color.RGBToHSV(value, out var h, out var s, out var v);

            h = Mathf.Repeat(h + _hueShift, 1f);
            s = Mathf.Clamp01(s * _saturationMultiplier);
            v = Mathf.Clamp01(v * _valueMultiplier);

            var shifted = Color.HSVToRGB(h, s, v);
            shifted.a = value.a;

            return shifted;
        }
    }
}
