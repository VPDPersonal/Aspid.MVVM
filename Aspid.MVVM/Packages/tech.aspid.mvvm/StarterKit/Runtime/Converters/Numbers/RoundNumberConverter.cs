using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Rounds a number, in a way the caller chooses.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads saturate, and the decimal-place
    /// count is ignored on the way to an integer.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Round",
        Tooltip = "Rounds a number, in a way the caller chooses")]
    public sealed class RoundNumberConverter :
        IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("How many decimal places to keep. Ignored on the way to an int or long.")]
        [SerializeField] [Min(0)] private int _digits;

        [Tooltip("Where a value exactly halfway between two results goes. Only the Round mode consults it.")]
        [SerializeField] private MidpointRounding _midpoint = MidpointRounding.ToEven;

        /// <remarks>Default: rounding to the nearest whole number.</remarks>
        public RoundNumberConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="digits">
        /// How many decimal places to keep. Ignored on the way to an int or long. A negative count
        /// reports an error and keeps none.
        /// </param>
        /// <param name="midpoint">
        /// Where a value exactly halfway between two results goes. Only <see cref="RoundMode.Round"/>
        /// consults it: 2.5 becomes 2 under <see cref="MidpointRounding.ToEven"/> and 3 under
        /// <see cref="MidpointRounding.AwayFromZero"/>.
        /// </param>
        public RoundNumberConverter(
            RoundMode mode,
            int digits = 0,
            MidpointRounding midpoint = MidpointRounding.ToEven)
        {
            _mode = mode;
            _digits = digits;
            _midpoint = midpoint;
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
        /// Rounds the specified value to the configured number of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <returns>
        /// The rounded value. A negative place count reports an error and rounds to a whole number; an
        /// undeclared mode reports an error and returns the value unchanged.
        /// </returns>
        public float Convert(float value) => NumericSaturation.ToFloat(ApplyDigits(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(ApplyDigits(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(ApplyDigits(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(ApplyDigits(value));
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => ApplyDigits(value);

        double IConverter<int, double>.Convert(int value) =>
            ApplyDigits(value);

        double IConverter<long, double>.Convert(long value) =>
            ApplyDigits(value);

        double IConverter<float, double>.Convert(float value) =>
            ApplyDigits(value);
        #endregion

        // The digits scale and unscale around Apply, so only fractional paths come here.
        private double ApplyDigits(double value)
        {
            if (_digits < 0)
            {
                this.LogError($"the decimal-place count {_digits} is negative",
                    "Rounding to a whole number.");
                return Apply(value);
            }

            if (_digits == 0) return Apply(value);

            var scale = Math.Pow(10d, _digits);
            return Apply(value * scale) / scale;
        }

        private double Apply(double value) => _mode switch
        {
            RoundMode.Round => Math.Round(value, _midpoint),
            RoundMode.Floor => Math.Floor(value),
            RoundMode.Ceil => Math.Ceiling(value),
            RoundMode.Truncate => Math.Truncate(value),
            _ => Undeclared(value)
        };

        private double Undeclared(double value)
        {
            this.LogError($"the mode {_mode.Describe()} is not a declared {nameof(RoundMode)}",
                "Returning the value unchanged.");
            return value;
        }
    }
}
