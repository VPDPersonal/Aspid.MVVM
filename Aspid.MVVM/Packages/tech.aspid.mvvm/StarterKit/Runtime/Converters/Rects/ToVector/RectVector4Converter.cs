#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between a rectangle and a four-component vector, in either direction.
    /// </summary>
    /// <remarks>
    /// The four numbers are a corner plus a size (x, y, width, height), and neither direction
    /// normalizes, so the round trip is exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Rect/To Vector",
        Name = "To Vector4",
        Tooltip = "Converts between a rectangle and a four-component vector, in either direction")]
    public sealed class RectVector4Converter :
        ITwoWayConverter<Rect, Vector4>,
        ITwoWayConverter<Vector4, Rect>
    {
        /// <summary>
        /// Reads the specified rectangle as a vector.
        /// </summary>
        /// <param name="value">The rectangle to read.</param>
        /// <returns>The vector, as x, y, width, height.</returns>
        public Vector4 Convert(Rect value) =>
            new(value.x, value.y, value.width, value.height);

        /// <summary>
        /// Reads the specified vector as a rectangle.
        /// </summary>
        /// <param name="value">The vector to read, as x, y, width, height.</param>
        /// <returns>The rectangle.</returns>
        public Rect Convert(Vector4 value) =>
            new(value.x, value.y, value.z, value.w);

        /// <summary>
        /// Reads a vector back as a rectangle.
        /// </summary>
        /// <param name="value">The vector to read, as x, y, width, height.</param>
        /// <returns>The rectangle.</returns>
        public Rect ConvertBack(Vector4 value) =>
            Convert(value);

        /// <summary>
        /// Reads a rectangle back as a vector.
        /// </summary>
        /// <param name="value">The rectangle to read.</param>
        /// <returns>The vector, as x, y, width, height.</returns>
        public Vector4 ConvertBack(Rect value) =>
            Convert(value);
    }
}
