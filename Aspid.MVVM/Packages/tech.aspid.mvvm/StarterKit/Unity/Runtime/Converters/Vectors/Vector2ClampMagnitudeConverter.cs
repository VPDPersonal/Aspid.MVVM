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
    /// Keeps a 2D vector inside a length.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="VectorClampMagnitudeConverter"/>, and the form a joystick
    /// or a drag offset actually arrives in.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 Clamp Magnitude", Tooltip = "Keeps a 2D vector inside a length")]
    public sealed class Vector2ClampMagnitudeConverter : IConverterVector2
    {
        [Tooltip("The longest the vector is allowed to be.")]
        [SerializeField] private float _maxMagnitude = 1f;

        [Tooltip("The shortest the vector is allowed to be. Zero disables the lower bound.")]
        [SerializeField] private float _minMagnitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ClampMagnitudeConverter"/> class clamping to one.
        /// </summary>
        public Vector2ClampMagnitudeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ClampMagnitudeConverter"/> class.
        /// </summary>
        /// <param name="maxMagnitude">The longest the vector is allowed to be.</param>
        /// <param name="minMagnitude">The shortest the vector is allowed to be.</param>
        public Vector2ClampMagnitudeConverter(float maxMagnitude, float minMagnitude = 0f)
        {
            _maxMagnitude = maxMagnitude;
            _minMagnitude = minMagnitude;
        }

        /// <summary>
        /// Clamps the length of the specified vector.
        /// </summary>
        /// <param name="value">The vector to clamp.</param>
        /// <returns>
        /// The clamped vector. A pair typed the wrong way round is read in the order that holds the
        /// vector inside both bounds, and a negative ceiling reads as zero.
        /// </returns>
        public Vector2 Convert(Vector2 value)
        {
            var magnitude = value.magnitude;

            // A zero vector has no direction to stretch along, so the lower bound cannot be applied
            // to it — the same choice the Vector3 converter makes.
            if (magnitude == 0f) return value;

            // Shared with the Vector3 converter so the bounds of the pair are ordered the same way.
            return value * VectorClampMagnitudeConverter.ClampScale(magnitude, _minMagnitude, _maxMagnitude);
        }
    }
}
