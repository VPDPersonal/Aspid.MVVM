#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a vector to its integer form.
    /// </summary>
    /// <remarks>
    /// <see cref="Vector2Int"/> and <see cref="Vector3Int"/> are what grid and tile games count in,
    /// and the package had no way to reach them.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 To Vector3 Int", Tooltip = "Converts a vector to its integer form")]
    public sealed class Vector3ToVector3IntConverter : ITwoWayConverter<Vector3, Vector3Int>
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        /// <remarks>Default: rounding to nearest.</remarks>
        public Vector3ToVector3IntConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        public Vector3ToVector3IntConverter(RoundMode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Converts the specified vector to its integer form.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The integer vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public Vector3Int Convert(Vector3 value) => _mode switch
        {
            RoundMode.Round => Vector3Int.RoundToInt(value),
            RoundMode.Floor => Vector3Int.FloorToInt(value),
            RoundMode.Ceil => Vector3Int.CeilToInt(value),
            RoundMode.Truncate => new Vector3Int((int)value.x, (int)value.y, (int)value.z),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <summary>
        /// Converts an integer vector back to a floating-point one.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The floating-point vector.</returns>
        public Vector3 ConvertBack(Vector3Int value) => value;
    }
}
