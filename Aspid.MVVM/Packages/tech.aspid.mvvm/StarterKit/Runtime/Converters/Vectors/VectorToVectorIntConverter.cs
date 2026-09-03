#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a vector to its integer form.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "To Vector Int",
        Tooltip = "Converts a vector to its integer form")]
    public sealed class VectorToVectorIntConverter :
        ITwoWayConverter<Vector2, Vector2Int>,
        ITwoWayConverter<Vector3, Vector3Int>
    {
        [Tooltip("Which way to drop the fraction.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private RoundMode _mode;

        /// <remarks>Default: rounding to nearest.</remarks>
        public VectorToVectorIntConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        public VectorToVectorIntConverter(RoundMode mode)
        {
            _mode = mode;
        }

        /// <summary>
        /// Converts the specified vector to its integer form.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>
        /// The integer vector. An undeclared mode reports an error and rounds to nearest.
        /// </returns>
        public Vector2Int Convert(Vector2 value) => _mode switch
        {
            RoundMode.Round => Vector2Int.RoundToInt(value),
            RoundMode.Floor => Vector2Int.FloorToInt(value),
            RoundMode.Ceil => Vector2Int.CeilToInt(value),
            RoundMode.Truncate => new Vector2Int((int)value.x, (int)value.y),
            _ => Vector2Int.RoundToInt(Undeclared(value))
        };

        Vector3Int IConverter<Vector3, Vector3Int>.Convert(Vector3 value) => _mode switch
        {
            RoundMode.Round => Vector3Int.RoundToInt(value),
            RoundMode.Floor => Vector3Int.FloorToInt(value),
            RoundMode.Ceil => Vector3Int.CeilToInt(value),
            RoundMode.Truncate => new Vector3Int((int)value.x, (int)value.y, (int)value.z),
            _ => Vector3Int.RoundToInt(Undeclared(value))
        };

        /// <summary>
        /// Converts an integer vector back to a floating-point one.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>
        /// The floating-point vector. The fraction dropped by <see cref="Convert"/> is not restored,
        /// so a TwoWay binding quantizes the source.
        /// </returns>
        public Vector2 ConvertBack(Vector2Int value) =>
            value;

        Vector3 ITwoWayConverter<Vector3, Vector3Int>.ConvertBack(Vector3Int value) =>
            value;

        private T Undeclared<T>(T value)
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Rounding to nearest.");

            return value;
        }
    }
}
