#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one number into the chosen axes of a 2D vector.
    /// </summary>
    [Serializable]
    public sealed class FloatToVector2Converter : IConverter<float, Vector2>
    {
        [Tooltip("Which axes the number is written into.")]
        [SerializeField] private AxisMask _axes = AxisMask.X | AxisMask.Y;

        [Tooltip("The value used for the axes the number does not write.")]
        [SerializeField] private Vector2 _base = Vector2.one;

        /// <remarks>Default: writing both axes.</remarks>
        public FloatToVector2Converter() { }

        /// <param name="axes">Which axes the number is written into.</param>
        /// <param name="base">The value used for the axes the number does not write.</param>
        public FloatToVector2Converter(AxisMask axes, Vector2 @base = default)
        {
            _axes = axes;
            _base = @base;
        }

        /// <summary>
        /// Writes the specified number into the chosen axes.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The vector.</returns>
        public Vector2 Convert(float value) => new(
            _axes.HasFlag(AxisMask.X) ? value : _base.x,
            _axes.HasFlag(AxisMask.Y) ? value : _base.y);
    }
}
