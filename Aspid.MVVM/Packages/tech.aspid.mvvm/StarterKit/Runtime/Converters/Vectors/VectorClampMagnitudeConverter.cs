#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a vector inside a length.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Clamp Magnitude",
        Tooltip = "Keeps a vector inside a length")]
    public sealed class VectorClampMagnitudeConverter :
        IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>
    {
        [Tooltip("The longest the vector is allowed to be.")]
        [SerializeField] [Min(0f)] private float _maxMagnitude = 1f;

        [Tooltip("The shortest the vector is allowed to be. Zero disables the lower bound.")]
        [SerializeField] [Min(0f)] private float _minMagnitude;

        /// <remarks>Default: clamping to one.</remarks>
        public VectorClampMagnitudeConverter() { }

        /// <param name="maxMagnitude">
        /// The longest the vector is allowed to be. Bounds typed the wrong way round are reported and
        /// swapped, and a negative bound reads as zero.
        /// </param>
        /// <param name="minMagnitude">
        /// The shortest the vector is allowed to be. Zero disables the lower bound; bounds typed the
        /// wrong way round are reported and swapped.
        /// </param>
        public VectorClampMagnitudeConverter(
            float maxMagnitude,
            float minMagnitude = 0f)
        {
            _maxMagnitude = maxMagnitude;
            _minMagnitude = minMagnitude;
        }

        /// <summary>
        /// Clamps the length of the specified vector.
        /// </summary>
        /// <param name="value">The vector to clamp.</param>
        /// <returns>
        /// The clamped vector, with a zero vector left as it is. A pair typed the wrong way round,
        /// or with a negative length in it, reports an error and is read in the order that holds the
        /// vector inside both bounds, with a negative bound reading as zero.
        /// </returns>
        public Vector3 Convert(Vector3 value) =>
            value * Scale(value.magnitude);

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value) =>
            value * Scale(value.magnitude);

        Vector4 IConverter<Vector4, Vector4>.Convert(Vector4 value) =>
            value * Scale(value.magnitude);

        private float Scale(float magnitude)
        {
            ReportInvalidBounds();

            // A zero vector has no direction to stretch along.
            if (magnitude is 0f) return 1f;

            return ClampScale(magnitude, _minMagnitude, _maxMagnitude);
        }

        private void ReportInvalidBounds()
        {
            if (_minMagnitude >= 0f && _maxMagnitude >= _minMagnitude) return;

            this.LogError(
                problem: $"the length bounds {_minMagnitude}..{_maxMagnitude} are not two ordered non-negative lengths",
                consequence: "Clamping to the ordered pair, with a negative bound held at zero.");
        }

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
