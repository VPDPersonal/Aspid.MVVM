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
    /// Keeps every axis of a vector between two bounds.
    /// </summary>
    /// <remarks>
    /// Holding a dragged marker inside a panel, or a camera target inside the level — a limit that
    /// is a box rather than the radius <see cref="VectorClampMagnitudeConverter"/> applies.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector Clamp Components", Tooltip = "Keeps every axis of a vector between two bounds")]
    public sealed class VectorClampComponentsConverter : IConverterVector3
    {
        [Tooltip("The lowest each axis is allowed to be.")]
        [SerializeField] private Vector3 _min = new(-1f, -1f, -1f);

        [Tooltip("The highest each axis is allowed to be.")]
        [SerializeField] private Vector3 _max = Vector3.one;

        /// <remarks>Default: clamping to ±1.</remarks>
        public VectorClampComponentsConverter() { }

        /// <param name="min">The lowest each axis is allowed to be.</param>
        /// <param name="max">The highest each axis is allowed to be.</param>
        public VectorClampComponentsConverter(Vector3 min, Vector3 max)
        {
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Clamps every axis of the specified vector.
        /// </summary>
        /// <param name="value">The vector to clamp.</param>
        /// <returns>The clamped vector.</returns>
        public Vector3 Convert(Vector3 value) => new(
            ClampComponent(value.x, _min.x, _max.x),
            ClampComponent(value.y, _min.y, _max.y),
            ClampComponent(value.z, _min.z, _max.z));

        // Mathf.Clamp with the bounds the wrong way round returns the minimum for every input, so a
        // pair typed in the wrong order reads as "the binding stopped working" rather than as a
        // mistake in the Inspector. Ordering them costs a comparison and removes the trap.
        internal static float ClampComponent(float value, float min, float max) =>
            min <= max ? Mathf.Clamp(value, min, max) : Mathf.Clamp(value, max, min);
    }
}
