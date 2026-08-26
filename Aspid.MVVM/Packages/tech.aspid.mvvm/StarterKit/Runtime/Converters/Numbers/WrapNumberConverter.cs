using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Folds a number back into a range instead of clamping it.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a
    /// fractional bound hands back a truncated fold.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Wrap",
        Tooltip = "Folds a number back into a range instead of clamping it")]
    public sealed class WrapNumberConverter :
        IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("How to fold a value that leaves the range.")]
        [SerializeField] private NumberWrapMode _mode;

        [Tooltip("The low end of the range.")]
        [SerializeField] private float _min;

        [Tooltip("The high end of the range. A range equal to the minimum returns that single value.")]
        [SerializeField] private float _max = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public WrapNumberConverter() { }

        /// <param name="mode">How to fold a value that leaves the range.</param>
        /// <param name="min">
        /// The low end of the range. Inverted bounds report an error and are swapped.
        /// </param>
        /// <param name="max">
        /// The high end of the range. A range equal to <paramref name="min"/> returns that single value.
        /// </param>
        public WrapNumberConverter(NumberWrapMode mode, float min, float max)
        {
            _mode = mode;
            _min = min;
            _max = max;
        }

        #region Return int
        int IConverter<int, int>.Convert(int value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<long, int>.Convert(long value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<float, int>.Convert(float value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<double, int>.Convert(double value) =>
            NumericSaturation.ToInt(Apply(value));
        #endregion

        #region Return long
        long IConverter<long, long>.Convert(long value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<int, long>.Convert(int value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<float, long>.Convert(float value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<double, long>.Convert(double value) =>
            NumericSaturation.ToLong(Apply(value));
        #endregion

        #region Return float
        /// <summary>
        /// Folds the specified value into the range.
        /// </summary>
        /// <param name="value">The value to fold.</param>
        /// <returns>
        /// The folded value. An empty range yields its single value; inverted bounds report an error and
        /// fold into the swapped range; an undeclared mode reports an error and returns the value unchanged.
        /// </returns>
        public float Convert(float value) => NumericSaturation.ToFloat(Apply(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Apply(value));
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => Apply(value);

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);
        #endregion

        private double Apply(double value)
        {
            // An empty range is a legitimate way to pin the output — data, not a mistake.
            if (_min == _max) return _min;

            var (min, max) = ((double)_min, (double)_max);
            if (min > max)
            {
                this.LogError($"the minimum {_min} is above the maximum {_max}",
                    "Folding into the swapped bounds.");
                (min, max) = (max, min);
            }

            var span = max - min;
            return _mode switch
            {
                NumberWrapMode.Repeat => min + Repeat(value - min, span),
                NumberWrapMode.PingPong => min + PingPong(value - min, span),
                _ => Undeclared(value)
            };
        }

        // Mathf.Repeat in double. The clamp is Mathf's own: it catches a subtraction landing a hair
        // outside the span, and lets a NaN through.
        private static double Repeat(double value, double length)
        {
            var folded = value - Math.Floor(value / length) * length;
            return Math.Min(Math.Max(folded, 0d), length);
        }

        // Mathf.PingPong in double: the value walks up to the length and back down again.
        private static double PingPong(double value, double length) =>
            length - Math.Abs(Repeat(value, length * 2d) - length);

        private double Undeclared(double value)
        {
            this.LogError($"the mode {_mode.Describe()} is not a declared {nameof(NumberWrapMode)}",
                "Returning the value unchanged.");
            return value;
        }
    }
}
