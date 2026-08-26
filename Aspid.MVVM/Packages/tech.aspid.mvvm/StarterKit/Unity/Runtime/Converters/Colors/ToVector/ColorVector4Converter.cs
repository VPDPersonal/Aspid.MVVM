#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between a color and a <see cref="Vector4"/>, in either direction.
    /// </summary>
    /// <remarks>
    /// The channels are copied as they are, with no color-space conversion or clamping, which is
    /// what makes the round trip exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color/To Vector",
        Name = "To Vector4",
        Tooltip = "Converts between a color and a Vector4, in either direction")]
    public sealed class ColorVector4Converter :
        ITwoWayConverter<Color, Vector4>,
        ITwoWayConverter<Vector4, Color>
    {
        /// <summary>
        /// Reads the specified color as a vector.
        /// </summary>
        /// <param name="value">The color to read.</param>
        /// <returns>Its red, green, blue and alpha as x, y, z and w.</returns>
        public Vector4 Convert(Color value) =>
            new(value.r, value.g, value.b, value.a);

        /// <summary>
        /// Reads the specified vector as a color.
        /// </summary>
        /// <param name="value">The vector to read.</param>
        /// <returns>Its x, y, z and w as red, green, blue and alpha, unclamped.</returns>
        public Color Convert(Vector4 value) =>
            new(value.x, value.y, value.z, value.w);

        /// <summary>
        /// Reads a vector back as a color.
        /// </summary>
        /// <param name="value">The vector to read.</param>
        /// <returns>Its x, y, z and w as red, green, blue and alpha, unclamped.</returns>
        public Color ConvertBack(Vector4 value) => Convert(value);

        /// <summary>
        /// Reads a color back as a vector.
        /// </summary>
        /// <param name="value">The color to read.</param>
        /// <returns>Its red, green, blue and alpha as x, y, z and w.</returns>
        public Vector4 ConvertBack(Color value) => Convert(value);
    }
}
