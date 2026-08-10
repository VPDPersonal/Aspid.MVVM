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
        /// <returns>The clamped vector.</returns>
        public Vector3 Convert(Vector3 value)
        {
            var magnitude = value.magnitude;
            if (magnitude == 0f) return value;

            if (magnitude > _maxMagnitude) return value * (_maxMagnitude / magnitude);
            if (_minMagnitude > 0f && magnitude < _minMagnitude) return value * (_minMagnitude / magnitude);

            return value;
        }
    }
}
