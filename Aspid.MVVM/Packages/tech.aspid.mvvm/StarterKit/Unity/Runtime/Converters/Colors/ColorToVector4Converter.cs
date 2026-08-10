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
    /// A shader property authored as a vector rather than a colour — tint weights, a packed mask, a
    /// <c>_MainTex_ST</c>-style parameter. The ViewModel keeps the colour it already has, and the
    /// vector binder on the other side gets the four floats it wants, instead of the two properties
    /// that were needed for one value.
    /// <para>
    /// The channels are copied as they are, with no colour-space conversion: a colour handed to a
    /// shader as a vector arrives exactly as the ViewModel wrote it, which is what makes the round
    /// trip through <see cref="Vector4ToColorConverter"/> exact.
    /// </para>
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
