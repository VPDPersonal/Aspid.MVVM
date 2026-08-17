#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a four-component vector as a rectangle.
    /// </summary>
    /// <remarks>
    /// A ViewModel that already holds four packed numbers — a shader property, a saved layout, a
    /// record that arrived as x, y, width, height — meeting a View API typed as
    /// <see cref="Rect"/>: a <see cref="RectTransform"/>, a camera viewport, a sprite border. The
    /// package supported neither direction, so <see cref="Rect"/> could not be bound at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector4 To Rect", Tooltip = "Reads a four-component vector as a rectangle")]
    public sealed class Vector4ToRectConverter : IConverter<Vector4, Rect>
    {
        /// <summary>
        /// Reads the specified vector as a rectangle.
        /// </summary>
        /// <param name="value">The vector to read, as x, y, width, height.</param>
        /// <returns>The rectangle.</returns>
        public Rect Convert(Vector4 value) => new(value.x, value.y, value.z, value.w);
    }
}
