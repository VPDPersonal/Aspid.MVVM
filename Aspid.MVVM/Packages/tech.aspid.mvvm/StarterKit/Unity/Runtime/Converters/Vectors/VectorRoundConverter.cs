#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Rounds every axis of a vector.
    /// </summary>
    /// <remarks>Snapping to a grid, for tile-based UI and level tools.</remarks>
    [Serializable]
    public sealed class VectorRoundConverter : IConverterVector3
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("The size of one grid step. Zero rounds to whole numbers.")]
        [SerializeField] private float _step;

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorRoundConverter"/> class rounding to whole numbers.
        /// </summary>
        public VectorRoundConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VectorRoundConverter"/> class.
        /// </summary>
        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="step">The size of one grid step.</param>
        public VectorRoundConverter(RoundMode mode, float step = 0f)
        {
            _mode = mode;
            _step = step;
        }

        /// <summary>
        /// Rounds every axis of the specified vector.
        /// </summary>
        /// <param name="value">The vector to round.</param>
        /// <returns>The rounded vector.</returns>
        public Vector3 Convert(Vector3 value) => new(Apply(value.x), Apply(value.y), Apply(value.z));

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        private float Apply(float value)
        {
            var step = _step == 0f ? 1f : _step;
            var scaled = value / step;

            var rounded = _mode switch
            {
                RoundMode.Round => Mathf.Round(scaled),
                RoundMode.Floor => Mathf.Floor(scaled),
                RoundMode.Ceil => Mathf.Ceil(scaled),
                RoundMode.Truncate => (float)Math.Truncate(scaled),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };

            return rounded * step;
        }
    }
}
