#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Narrows a <see cref="Color"/> into a <see cref="Color32"/>.
    /// </summary>
    /// <remarks>
    /// Each channel is clamped to 0..255 and rounded, so an HDR colour loses everything above white and
    /// the round trip back through <see cref="Color32ToColorConverter"/> is not exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color To Color32", Tooltip = "Narrows a Color into a Color32")]
    public sealed class ColorToColor32Converter : IConverter<Color, Color32>
    {
        /// <summary>
        /// Narrows the specified colour.
        /// </summary>
        /// <param name="value">The colour to narrow.</param>
        /// <returns>The same colour with each channel as a byte.</returns>
        public Color32 Convert(Color value) => value;
    }
}
