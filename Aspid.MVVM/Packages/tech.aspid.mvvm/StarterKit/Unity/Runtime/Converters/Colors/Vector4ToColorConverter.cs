#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a <see cref="Vector4"/> as a colour.
    /// </summary>
    /// <remarks>
    /// The other direction of <see cref="ColorToVector4Converter"/>: a ViewModel that already stores
    /// a vector — one row of a shader parameter table, a value read back off a material — driving a
    /// colour binder without growing a second property to hold the same four numbers.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Vector4 To Color", Tooltip = "Reads a Vector4 as a colour")]
    public sealed class Vector4ToColorConverter : IConverter<Vector4, Color>
    {
        /// <summary>
        /// Reads the specified vector as a colour.
        /// </summary>
        /// <param name="value">The vector to read.</param>
        /// <returns>Its x, y, z and w as red, green, blue and alpha, unclamped.</returns>
        public Color Convert(Vector4 value) => new(value.x, value.y, value.z, value.w);
    }
}
