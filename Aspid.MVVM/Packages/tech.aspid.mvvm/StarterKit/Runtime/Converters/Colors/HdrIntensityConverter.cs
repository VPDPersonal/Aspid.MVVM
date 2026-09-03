#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pushes a color above white by an exposure value.
    /// </summary>
    /// <remarks>
    /// The result leaves the 0..1 range on purpose, bind it to a material color or a light. A UGUI
    /// <see cref="UnityEngine.UI.Graphic"/> clamps it and shows no difference above white.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "HDR Intensity",
        Tooltip = "Pushes a color above white by an exposure value")]
    public sealed class HdrIntensityConverter : IConverter<Color, Color>
    {
        [Tooltip("The exposure applied to the color, in stops. Each whole step doubles the brightness.")]
        [SerializeField] private float _intensity;

        /// <remarks>Default: no exposure, which changes nothing.</remarks>
        public HdrIntensityConverter() { }

        /// <param name="intensity">
        /// The exposure applied to the color, in stops. Each whole step doubles its brightness; zero
        /// changes nothing.
        /// </param>
        public HdrIntensityConverter(float intensity)
        {
            _intensity = intensity;
        }

        /// <summary>
        /// Applies the exposure to the specified color.
        /// </summary>
        /// <param name="value">The color to brighten.</param>
        /// <returns>
        /// The color scaled by two to the power of the intensity, with its alpha untouched. The
        /// channels are not clamped, an HDR color above one is the point.
        /// </returns>
        public Color Convert(Color value)
        {
            var factor = Mathf.Pow(2f, _intensity);
            return new Color(value.r * factor, value.g * factor, value.b * factor, value.a);
        }
    }
}
