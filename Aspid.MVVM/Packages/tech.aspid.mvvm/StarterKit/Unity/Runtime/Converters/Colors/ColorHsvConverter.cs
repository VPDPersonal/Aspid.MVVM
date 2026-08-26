#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shifts a color in HSV space.
    /// </summary>
    /// <remarks>
    /// Saturation and brightness are held inside 0..1, so an HDR color comes back at white level.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "HSV",
        Tooltip = "Shifts a color in HSV space")]
    public sealed class ColorHsvConverter : IConverter<Color, Color>
    {
        [Tooltip("How far to rotate the hue, in turns. 0.5 is the opposite color.")]
        [SerializeField] private float _hueShift;

        [Tooltip("Scales the saturation. The result is held to 0..1.")]
        [SerializeField] private float _saturationMultiplier = 1f;

        [Tooltip("Scales the brightness. The result is held to 0..1, so an HDR color comes back at " +
            "white level.")]
        [SerializeField] private float _valueMultiplier = 1f;

        /// <remarks>Default: no shift and no scaling, which changes nothing.</remarks>
        public ColorHsvConverter() { }

        /// <param name="hueShift">
        /// How far to rotate the hue, in turns. 0.5 is the opposite color.
        /// </param>
        /// <param name="saturationMultiplier">Scales the saturation. The result is held to 0..1.</param>
        /// <param name="valueMultiplier">
        /// Scales the brightness. The result is held to 0..1, so an HDR color comes back at white level.
        /// </param>
        public ColorHsvConverter(float hueShift, float saturationMultiplier = 1f, float valueMultiplier = 1f)
        {
            _hueShift = hueShift;
            _saturationMultiplier = saturationMultiplier;
            _valueMultiplier = valueMultiplier;
        }

        /// <summary>
        /// Shifts the specified color.
        /// </summary>
        /// <param name="value">The color to shift.</param>
        /// <returns>The shifted color, with its alpha untouched.</returns>
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
