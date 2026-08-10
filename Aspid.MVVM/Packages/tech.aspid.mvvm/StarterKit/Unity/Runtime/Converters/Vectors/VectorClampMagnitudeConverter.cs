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
    /// Keeps a vector inside a length.
    /// </summary>
    /// <remarks>Holding a joystick offset or a drag inside a panel's radius.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector Clamp Magnitude", Tooltip = "Keeps a vector inside a length")]
    public sealed class VectorClampMagnitudeConverter : IConverterVector3
    {
        [Tooltip("The longest the vector is allowed to be.")]
        [SerializeField] private float _maxMagnitude = 1f;

        [Tooltip("The shortest the vector is allowed to be. Zero disables the lower bound.")]
        [SerializeField] private float _minMagnitude;

        /// <remarks>Default: clamping to one.</remarks>
        public VectorClampMagnitudeConverter() { }

        /// <param name="maxMagnitude">The longest the vector is allowed to be.</param>
        /// <param name="minMagnitude">The shortest the vector is allowed to be.</param>
        public VectorClampMagnitudeConverter(float maxMagnitude, float minMagnitude = 0f)
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
        public Vector3 Convert(Vector3 value)
        {
            var magnitude = value.magnitude;
            if (magnitude == 0f) return value;

            return value * ClampScale(magnitude, _minMagnitude, _maxMagnitude);
        }

        // Taken raw, a ceiling below the floor shrinks a long vector past the floor and stretches a
        // short one past the ceiling — one instance breaking both of its own bounds, which reads as
        // "the binding stopped working" rather than as a mistake in the Inspector. A negative
        // ceiling is worse: scaling by it turns the vector around, from a converter whose whole job
        // is to keep a length. Ordering the pair and holding it at zero costs two comparisons and
        // removes both traps, the way VectorClampComponentsConverter.ClampComponent does.
        internal static float ClampScale(float magnitude, float minMagnitude, float maxMagnitude)
        {
            var lower = Mathf.Max(0f, Mathf.Min(minMagnitude, maxMagnitude));
            var upper = Mathf.Max(0f, Mathf.Max(minMagnitude, maxMagnitude));

            if (magnitude > upper) return upper / magnitude;
            if (lower > 0f && magnitude < lower) return lower / magnitude;

            return 1f;
        }
    }
}
