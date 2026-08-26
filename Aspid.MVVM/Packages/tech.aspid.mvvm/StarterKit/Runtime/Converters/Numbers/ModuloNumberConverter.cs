using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns the remainder of a number divided by an authored divisor.
    /// </summary>
    /// <remarks>
    /// C#'s <c>%</c> takes the sign of the left operand; the euclidean form, the default here, does not.
    /// The int and long overloads use the divisor's whole-number part and stay in integers, so a counter
    /// beyond 2^53 stays exact.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Modulo",
        Tooltip = "Returns the remainder of a number divided by an authored divisor")]
    public sealed class ModuloNumberConverter :
        IConverter<int, int>,
        IConverter<long, long>,
        IConverter<float, float>,
        IConverter<double, double>
    {
        [Tooltip("The number the value is divided by. The int and long overloads use its whole-number " +
            "part, so anything below one is a zero to them.")]
        [SerializeField] private double _divisor = 1d;

        [Tooltip("Always return a non-negative remainder, so -1 modulo 360 is 359. Off gives C#'s %.")]
        [SerializeField] private bool _euclidean = true;

        /// <remarks>Default: dividing by one.</remarks>
        public ModuloNumberConverter() { }

        /// <param name="divisor">
        /// The number the value is divided by. A divisor of zero reports an error and passes the value
        /// through. The int and long overloads use its whole-number part, so anything below one is a
        /// zero to them.
        /// </param>
        /// <param name="euclidean">If <see langword="true"/>, always returns a non-negative remainder.</param>
        public ModuloNumberConverter(double divisor, bool euclidean = true)
        {
            _divisor = divisor;
            _euclidean = euclidean;
        }

        /// <summary>
        /// Divides the specified value by the divisor and returns what is left.
        /// </summary>
        /// <param name="value">The value to divide.</param>
        /// <returns>
        /// The remainder. A divisor of zero reports an error and returns the value unchanged.
        /// </returns>
        public double Convert(double value)
        {
            if (_divisor == 0d) return NoDivisor(value);

            var remainder = value % _divisor;
            return _euclidean && remainder < 0d ? remainder + Math.Abs(_divisor) : remainder;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => NumericSaturation.ToFloat(Convert((double)value));

        /// <inheritdoc cref="Convert(double)"/>
        // Saturated rather than cast: casting the euclidean fold would wrap it to the wrong sign.
        public int Convert(int value) => NumericSaturation.ToInt(Convert((long)value));

        /// <inheritdoc cref="Convert(double)"/>
        public long Convert(long value)
        {
            var divisor = NumericSaturation.ToLong(_divisor);
            if (divisor == 0L) return NoDivisor(value);

            // Every long divides evenly by -1, but long.MinValue % -1L faults on the hardware divide
            // because the quotient does not fit. The remainder is plainly zero, so answer it directly.
            if (divisor == -1L) return 0L;

            var remainder = value % divisor;
            if (!_euclidean || remainder >= 0L) return remainder;

            // Unary minus rather than Math.Abs, which throws on long.MinValue. The minus wraps back
            // to long.MinValue for that divisor, and the wrap is what makes the sum land in range.
            return remainder + (divisor < 0L ? -divisor : divisor);
        }

        private double NoDivisor(double value)
        {
            this.LogError("the divisor is zero", "Returning the value unchanged.");
            return value;
        }

        private long NoDivisor(long value)
        {
            this.LogError(
                _divisor == 0d
                    ? "the divisor is zero"
                    : $"the divisor {_divisor} has no whole-number part for an integer to divide by",
                "Returning the value unchanged.");

            return value;
        }
    }
}
