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
    /// Keeps a number inside a range.
    /// </summary>
    /// <remarks>
    /// The bounds are authored as <see cref="double"/> so the same converter can guard an
    /// <see cref="int"/> or <see cref="long"/> field without the bound being rounded on the way in — a
    /// <see cref="float"/> cannot name every <see cref="int"/>. The int and long overloads return the
    /// incoming value untouched when it is already in range, so no value round-trips through a
    /// floating-point number.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Clamp Number", Tooltip = "Keeps a number inside a range")]
    public sealed class ClampNumberConverter :
        IConverterFloat,
        IConverter<int, int>,
        IConverter<long, long>,
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

        /// <param name="min">The lowest value allowed through.</param>
        /// <param name="max">The highest value allowed through.</param>
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
        /// <returns>The value, held inside the configured bounds. A NaN passes through unchanged.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public double Convert(double value)
        {
            var (low, high) = Bounds();

            // Written as two comparisons rather than Math.Clamp so that a NaN — which fails every
            // comparison — passes through instead of collapsing onto a bound.
            if (low && value < _min) return _min;
            if (high && value > _max) return _max;

            return value;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => (float)Convert((double)value);

        /// <inheritdoc cref="Convert(double)"/>
        public int Convert(int value)
        {
            var (low, high) = Bounds();

            // The bounds are authored as doubles, so a fractional one has to round INTO the range:
            // truncating 0.5 toward zero would return 0 for a minimum of 0.5 and leave the value
            // still below the bound the converter promised to hold it above.
            if (low && value < _min) return NumericSaturation.ToInt(Math.Ceiling(_min));
            if (high && value > _max) return NumericSaturation.ToInt(Math.Floor(_max));

            return value;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public long Convert(long value)
        {
            var (low, high) = Bounds();

            if (low && value < _min) return NumericSaturation.ToLong(Math.Ceiling(_min));
            if (high && value > _max) return NumericSaturation.ToLong(Math.Floor(_max));

            return value;
        }

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        private (bool Low, bool High) Bounds() => _mode switch
        {
            ClampMode.Both => (true, true),
            ClampMode.Min => (true, false),
            ClampMode.Max => (false, true),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };
    }
}
