#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between a <see cref="Color"/> and a <see cref="Color32"/>, in either direction.
    /// </summary>
    /// <remarks>
    /// Narrowing clamps each channel to 0..1 and quantizes it to a byte, so an HDR color loses
    /// everything above white and the round trip through the byte color is not exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "To Color32",
        Tooltip = "Converts between a Color and a Color32, in either direction")]
    public sealed class ColorColor32Converter :
        ITwoWayConverter<Color, Color32>,
        ITwoWayConverter<Color32, Color>
    {
        /// <summary>
        /// Narrows the specified color.
        /// </summary>
        /// <param name="value">The color to narrow.</param>
        /// <returns>The same color with each channel as a byte.</returns>
        public Color32 Convert(Color value) =>
            value;

        /// <summary>
        /// Widens the specified byte color.
        /// </summary>
        /// <param name="value">The byte color to widen.</param>
        /// <returns>The same color with each channel as a 0..1 float.</returns>
        public Color Convert(Color32 value) =>
            value;

        /// <summary>
        /// Widens a byte color back.
        /// </summary>
        /// <param name="value">The byte color to widen.</param>
        /// <returns>The same color with each channel as a 0..1 float.</returns>
        public Color ConvertBack(Color32 value) =>
            value;

        /// <summary>
        /// Narrows a color back.
        /// </summary>
        /// <param name="value">The color to narrow.</param>
        /// <returns>The same color with each channel as a byte.</returns>
        public Color32 ConvertBack(Color value) =>
            value;
    }
}
