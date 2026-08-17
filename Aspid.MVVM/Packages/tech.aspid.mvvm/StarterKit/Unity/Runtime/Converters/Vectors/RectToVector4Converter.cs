#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a rectangle as a four-component vector.
    /// </summary>
    /// <remarks>
    /// The way back from <see cref="Vector4ToRectConverter"/>, and the form a shader property or a
    /// saved layout takes.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Rect To Vector4", Tooltip = "Reads a rectangle as a four-component vector")]
    public sealed class RectToVector4Converter : IConverter<Rect, Vector4>
    {
        /// <summary>
        /// Reads the specified rectangle as a vector.
        /// </summary>
        /// <param name="value">The rectangle to read.</param>
        /// <returns>The vector, as x, y, width, height.</returns>
        public Vector4 Convert(Rect value) => new(value.x, value.y, value.width, value.height);
    }
}
