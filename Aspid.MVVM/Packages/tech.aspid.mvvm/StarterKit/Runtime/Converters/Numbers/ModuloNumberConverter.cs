#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns the remainder of a number divided by an authored divisor.
    /// </summary>
    /// <remarks>The int and long overloads use the divisor's whole-number part and stay in integers.</remarks>
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
        [Tooltip("The number the value is divided by. Integers use its whole-number part.")]
        [SerializeField] private double _divisor = 1d;

        [Tooltip("Return a non-negative remainder: -1 modulo 360 is 359. Off gives C#'s %.")]
        [SerializeField] private bool _euclidean = true;

        /// <remarks>Default: dividing by one.</remarks>
        public ModuloNumberConverter() { }

        /// <param name="divisor">The number the value is divided by. Zero passes the value through. Integers use its whole-number part.</param>
        /// <param name="euclidean">If <see langword="true"/>, returns a non-negative remainder.</param>
        public ModuloNumberConverter(
            double divisor,
            bool euclidean = true)
        {
            _divisor = divisor;
            _euclidean = euclidean;
        }

        /// <summary>
        /// Divides the specified value by the divisor and returns what is left.
        /// </summary>
        /// <param name="value">The value to divide.</param>
        /// <returns>The remainder. A divisor of zero reports an error and returns the value unchanged.</returns>
        public double Convert(double value)
        {
            if (_divisor is 0d) return NoDivisor(value);

            var remainder = value % _divisor;
            return _euclidean && remainder < 0d ? remainder + Math.Abs(_divisor) : remainder;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) =>
            NumericSaturation.ToFloat(Convert((double)value));

        /// <inheritdoc cref="Convert(double)"/>
        public int Convert(int value) =>
            NumericSaturation.ToInt(Convert((long)value));

        /// <inheritdoc cref="Convert(double)"/>
        public long Convert(long value)
        {
            var divisor = NumericSaturation.ToLong(_divisor);
            if (divisor == 0L) return NoDivisor(value);

            // long.MinValue % -1L faults on the hardware divide; the remainder is zero anyway.
            if (divisor == -1L) return 0L;

            var remainder = value % divisor;
            if (!_euclidean || remainder >= 0L) return remainder;

            // Unary minus rather than Math.Abs, which throws on long.MinValue.
            return remainder + (divisor < 0L ? -divisor : divisor);
        }

        private double NoDivisor(double value)
        {
            this.LogError(
                problem: "the divisor is zero",
                consequence: "Returning the value unchanged.");

            return value;
        }

        private long NoDivisor(long value)
        {
            this.LogError(
                problem: _divisor is 0d
                    ? "the divisor is zero"
                    : $"the divisor {_divisor} has no whole-number part for an integer to divide by",
                consequence: "Returning the value unchanged.");

            return value;
        }
    }
}
