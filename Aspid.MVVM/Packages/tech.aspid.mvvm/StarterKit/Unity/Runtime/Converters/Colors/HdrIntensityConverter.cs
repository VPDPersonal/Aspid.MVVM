#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pushes a colour above white by an exposure value.
    /// </summary>
    /// <remarks>
    /// The exposure is in stops, matching Unity's own HDR colour field: the picked colour times two to the
    /// power of the intensity. One stop is twice as bright.
    /// <para>
    /// The result leaves the 0..1 range on purpose, so it belongs on something that keeps it — a material
    /// colour or a light. A UGUI <see cref="UnityEngine.UI.Graphic"/> clamps it and shows no difference
    /// above white.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Hdr Intensity", Tooltip = "Pushes a colour above white by an exposure value")]
    public sealed class HdrIntensityConverter : IConverter<Color, Color>
    {
        [Tooltip("The exposure applied to the colour, in stops. Each whole step doubles its brightness; zero changes nothing.")]
        [SerializeField] private float _intensity;

        /// <summary>
        /// Initializes a new instance of the <see cref="HdrIntensityConverter"/> class that changes nothing.
        /// </summary>
        public HdrIntensityConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HdrIntensityConverter"/> class.
        /// </summary>
        /// <param name="intensity">The exposure applied to the colour, in stops.</param>
        public HdrIntensityConverter(float intensity)
        {
            _intensity = intensity;
        }

        /// <summary>
        /// Applies the exposure to the specified colour.
        /// </summary>
        /// <param name="value">The colour to brighten.</param>
        /// <returns>
        /// The colour scaled by two to the power of the intensity, with its alpha untouched. The
        /// channels are not clamped — an HDR colour above one is the point.
        /// </returns>
        public Color Convert(Color value)
        {
            var factor = Mathf.Pow(2f, _intensity);

            return new Color(value.r * factor, value.g * factor, value.b * factor, value.a);
        }
    }
}
