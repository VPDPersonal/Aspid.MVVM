#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 2D vector to its integer form.
    /// </summary>
    [Serializable]
    public sealed class Vector2ToVector2IntConverter : ITwoWayConverter<Vector2, Vector2Int>
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        /// <remarks>Default: rounding to nearest.</remarks>
        public Vector2ToVector2IntConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        public Vector2ToVector2IntConverter(RoundMode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Converts the specified vector to its integer form.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The integer vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public Vector2Int Convert(Vector2 value) => _mode switch
        {
            RoundMode.Round => Vector2Int.RoundToInt(value),
            RoundMode.Floor => Vector2Int.FloorToInt(value),
            RoundMode.Ceil => Vector2Int.CeilToInt(value),
            RoundMode.Truncate => new Vector2Int((int)value.x, (int)value.y),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <summary>
        /// Converts an integer vector back to a floating-point one.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The floating-point vector.</returns>
        public Vector2 ConvertBack(Vector2Int value) => value;
    }
}
