#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a colour as a <see cref="Vector4"/>.
    /// </summary>
    /// <remarks>
    /// The channels are copied as they are, with no colour-space conversion, which is what makes the
    /// round trip through <see cref="Vector4ToColorConverter"/> exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color To Vector4", Tooltip = "Reads a colour as a Vector4")]
    public sealed class ColorToVector4Converter : IConverter<Color, Vector4>
    {
        /// <summary>
        /// Reads the specified colour as a vector.
        /// </summary>
        /// <param name="value">The colour to read.</param>
        /// <returns>Its red, green, blue and alpha as x, y, z and w.</returns>
        public Vector4 Convert(Color value) => new(value.r, value.g, value.b, value.a);
    }
}
