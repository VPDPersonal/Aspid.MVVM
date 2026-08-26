using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a number inside a range.
    /// </summary>
    /// <remarks>
    /// The bounds are authored as <see cref="double"/> so an int or long bound is not rounded on the
    /// way in — a <see cref="float"/> cannot name every <see cref="int"/>. The int and long overloads
    /// return an in-range value untouched, so no value round-trips through a floating-point number.
    /// </remarks>
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

        /// <param name="min">
        /// The lowest value allowed through. Inverted bounds report an error and are swapped when the
        /// mode applies both.
        /// </param>
        /// <param name="max">
        /// The highest value allowed through. Inverted bounds report an error and are swapped when the
        /// mode applies both.
        /// </param>
        /// <param name="mode">Which bound to apply.</param>
        public ClampNumberConverter(double min, double max, ClampMode mode = ClampMode.Both)
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
        /// The value, held inside the bounds; a NaN passes through. Inverted bounds report an error and
        /// clamp to the swapped range when the mode applies both; an undeclared mode reports an error
        /// and leaves the value unclamped.
        /// </returns>
        public double Convert(double value)
        {
            var (low, high, min, max) = Bounds();

            // Two comparisons rather than Math.Clamp: a NaN fails both and passes through.
            if (low && value < min) return min;
            if (high && value > max) return max;

            return value;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => NumericSaturation.ToFloat(Convert((double)value));

        /// <inheritdoc cref="Convert(double)"/>
        public int Convert(int value)
        {
            var (low, high, min, max) = Bounds();

            // A fractional bound has to round INTO the range: truncating a minimum of 0.5 toward
            // zero would leave the value below the bound the converter promised to hold it above.
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

        // The swap belongs to Both alone: a single-bound mode never reads the other bound, so a
        // minimum above an untouched default maximum is authoring rather than a contradiction.
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

            this.LogError($"the minimum {_min} is above the maximum {_max}",
                "Clamping to the swapped bounds.");
            return (true, true, _max, _min);
        }

        private (bool Low, bool High, double Min, double Max) Undeclared()
        {
            this.LogError($"the mode {_mode.Describe()} is not a declared {nameof(ClampMode)}",
                "Letting the value through unclamped.");
            return (false, false, _min, _max);
        }
    }
}
