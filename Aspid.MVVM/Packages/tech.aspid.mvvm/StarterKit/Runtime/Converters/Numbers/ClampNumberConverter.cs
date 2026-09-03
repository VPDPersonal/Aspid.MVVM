using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a number inside a range.
    /// </summary>
    /// <remarks>An in-range int or long is returned untouched, so it never round-trips through a floating-point number.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Clamp",
        Tooltip = "Keeps a number inside a range")]
    public sealed class ClampNumberConverter :
        IConverter<int, int>,
        IConverter<long, long>,
        IConverter<float, float>,
        IConverter<double, double>
    {
        [Tooltip("The lowest value allowed through.")]
        [SerializeField] private double _min;

        [Tooltip("The highest value allowed through.")]
        [SerializeField] private double _max = 1d;

        [Tooltip("Which bound to apply.")]
        [SerializeField] private ClampMode _mode = ClampMode.Both;

        /// <remarks>Default: clamping to 0..1.</remarks>
        public ClampNumberConverter() { }

        /// <param name="min">The lowest value allowed through. Inverted bounds report an error and are swapped.</param>
        /// <param name="max">The highest value allowed through. Inverted bounds report an error and are swapped.</param>
        /// <param name="mode">Which bound to apply.</param>
        public ClampNumberConverter(
            double min,
            double max,
            ClampMode mode = ClampMode.Both)
        {
            _min = min;
            _max = max;
            _mode = mode;
        }

        /// <summary>
        /// Clamps the specified value.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <returns>
        /// The value held inside the bounds; a NaN passes through. Inverted bounds are swapped, an undeclared mode
        /// leaves the value unclamped, both with an error.
        /// </returns>
        public double Convert(double value)
        {
            var (low, high, min, max) = Bounds();

            if (low && value < min) return min;
            if (high && value > max) return max;

            return value;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) =>
            NumericSaturation.ToFloat(Convert((double)value));

        /// <inheritdoc cref="Convert(double)"/>
        public int Convert(int value)
        {
            var (low, high, min, max) = Bounds();

            if (low && value < min) return NumericSaturation.ToInt(Math.Ceiling(min));
            if (high && value > max) return NumericSaturation.ToInt(Math.Floor(max));

            return value;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public long Convert(long value)
        {
            var (low, high, min, max) = Bounds();

            if (low && value < min) return NumericSaturation.ToLong(Math.Ceiling(min));
            if (high && value > max) return NumericSaturation.ToLong(Math.Floor(max));

            return value;
        }

        private (bool Low, bool High, double Min, double Max) Bounds() => _mode switch
        {
            ClampMode.Both => NormalizedBounds(),
            ClampMode.Min => (true, false, _min, _max),
            ClampMode.Max => (false, true, _min, _max),
            _ => Undeclared()
        };

        private (bool Low, bool High, double Min, double Max) NormalizedBounds()
        {
            if (_min <= _max) return (true, true, _min, _max);

            this.LogError(
                problem: $"the minimum {_min} is above the maximum {_max}",
                consequence: "Clamping to the swapped bounds.");

            return (true, true, _max, _min);
        }

        private (bool Low, bool High, double Min, double Max) Undeclared()
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(ClampMode)}",
                consequence: "Letting the value through unclamped.");

            return (false, false, _min, _max);
        }
    }
}
