#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Rounds a number, in a way the caller chooses.
    /// </summary>
    /// <remarks>The decimal-place count is ignored on the way to an int or long.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Round",
        Tooltip = "Rounds a number, in a way the caller chooses")]
    public sealed class RoundNumberConverter : NumberConverter
    {
        [Tooltip("Which way to drop the fraction.")]
        [SerializeField] private RoundMode _mode;

        [Tooltip("How many decimal places to keep. Ignored on the way to an int or long.")]
        [SerializeField] [Min(0)] private int _digits;

        [Tooltip("Where an exact half goes. Only the Round mode consults it.")]
        [SerializeField] private MidpointRounding _midpoint = MidpointRounding.ToEven;

        /// <remarks>Default: rounding to the nearest whole number.</remarks>
        public RoundNumberConverter() { }

        /// <param name="mode">Which way to drop the fraction.</param>
        /// <param name="digits">How many decimal places to keep. Ignored on the way to an int or long.</param>
        /// <param name="midpoint">
        /// Where an exact half goes. Only <see cref="RoundMode.Round"/> consults it: 2.5 becomes 2 under
        /// <see cref="MidpointRounding.ToEven"/> and 3 under <see cref="MidpointRounding.AwayFromZero"/>.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="digits"/> is negative.</exception>
        public RoundNumberConverter(
            RoundMode mode,
            int digits = 0,
            MidpointRounding midpoint = MidpointRounding.ToEven)
        {
            _mode = mode;
            _midpoint = midpoint;
            _digits = digits >= 0 ? digits : throw new ArgumentOutOfRangeException(nameof(digits));
        }

        /// <summary>
        /// Rounds the number to the configured number of decimal places.
        /// </summary>
        /// <param name="value">The number to round.</param>
        /// <returns>The rounded number. An undeclared mode reports an error and returns the value unchanged.</returns>
        protected override double Apply(double value)
        {
            if (_digits == 0) return Round(value);

            var scale = Math.Pow(10d, _digits);
            return Round(value * scale) / scale;
        }

        private double Round(double value) => _mode switch
        {
            RoundMode.Round => Math.Round(value, _midpoint),
            RoundMode.Floor => Math.Floor(value),
            RoundMode.Ceil => Math.Ceiling(value),
            RoundMode.Truncate => Math.Truncate(value),
            _ => Undeclared(value)
        };

        private double Undeclared(double value)
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(RoundMode)}",
                consequence: "Returning the value unchanged.");

            return value;
        }
    }
}
