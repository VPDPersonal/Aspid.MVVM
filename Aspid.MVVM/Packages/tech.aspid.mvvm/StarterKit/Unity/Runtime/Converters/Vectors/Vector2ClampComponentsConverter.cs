#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps both axes of a 2D vector between two bounds.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="VectorClampComponentsConverter"/>: holding a dragged icon
    /// inside its panel, or an anchored position inside the safe area — a limit that is a rectangle
    /// rather than the radius <see cref="Vector2ClampMagnitudeConverter"/> applies.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Clamp Components", Tooltip = "Keeps both axes of a 2D vector between two bounds")]
    public sealed class Vector2ClampComponentsConverter : IConverterVector2
    {
        [Tooltip("The lowest each axis is allowed to be.")]
        [SerializeField] private Vector2 _min = new(-1f, -1f);

        [Tooltip("The highest each axis is allowed to be.")]
        [SerializeField] private Vector2 _max = Vector2.one;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ClampComponentsConverter"/> class clamping to ±1.
        /// </summary>
        public Vector2ClampComponentsConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ClampComponentsConverter"/> class.
        /// </summary>
        /// <param name="min">The lowest each axis is allowed to be.</param>
        /// <param name="max">The highest each axis is allowed to be.</param>
        public Vector2ClampComponentsConverter(Vector2 min, Vector2 max)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Clamps both axes of the specified vector.
        /// </summary>
        /// <param name="value">The vector to clamp.</param>
        /// <returns>The clamped vector.</returns>
        public Vector2 Convert(Vector2 value) => new(
            VectorClampComponentsConverter.ClampComponent(value.x, _min.x, _max.x),
            VectorClampComponentsConverter.ClampComponent(value.y, _min.y, _max.y));
    }
}
