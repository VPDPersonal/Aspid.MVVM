using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Returns the remainder of a number divided by an authored divisor.
    /// </summary>
    /// <remarks>
    /// C#'s <c>%</c> takes the sign of the left operand, so <c>-1 % 360</c> is <c>-1</c> — never what a
    /// wrapped angle or a cycling index wants. The euclidean form gives 359 and is the default here.
    /// <para>
    /// The int and long overloads take the whole-number part of the divisor and stay in integers, so a
    /// counter beyond 2^53 stays exact.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Modulo Number", Tooltip = "Returns the remainder of a number divided by an authored divisor")]
    public sealed class ModuloNumberConverter :
        IConverter<int, int>,
        IConverter<long, long>,
        IConverter<float, float>,
        IConverter<double, double>
    {
        [Tooltip("The number the value is divided by. A divisor of zero passes the value through. "
            + "The int and long overloads use its whole-number part.")]
        [SerializeField] private double _divisor = 1d;

        [Tooltip("Always return a non-negative remainder, so -1 modulo 360 is 359 rather than -1. "
            + "Turn it off for C#'s own % semantics, where the result takes the value's sign.")]
        [SerializeField] private bool _euclidean = true;

        /// <remarks>Default: dividing by one.</remarks>
        public ModuloNumberConverter() { }

        /// <param name="divisor">The number the value is divided by.</param>
        /// <param name="euclidean">
        /// If <see langword="true"/>, always returns a non-negative remainder.
        /// </param>
        public ModuloNumberConverter(double divisor, bool euclidean = true)
        {
            _divisor = divisor;
            _euclidean = euclidean;
        }

        /// <summary>
        /// Divides the specified value by the divisor and returns what is left.
        /// </summary>
        /// <param name="value">The value to divide.</param>
        /// <returns>The remainder, or the value unchanged when the divisor is zero.</returns>
        public double Convert(double value)
        {
            if (_divisor == 0d) return value;

            var remainder = value % _divisor;
            return _euclidean && remainder < 0d ? remainder + Math.Abs(_divisor) : remainder;
        }

        /// <inheritdoc cref="Convert(double)"/>
        public float Convert(float value) => (float)Convert((double)value);

        // Saturated rather than cast: a divisor larger than an int is a misconfiguration, and casting
        // the euclidean fold would wrap it round to the wrong sign — the exact fault this converter
        // exists to fix.
        /// <inheritdoc cref="Convert(double)"/>
        public int Convert(int value) => NumericSaturation.ToInt(Convert((long)value));

        /// <inheritdoc cref="Convert(double)"/>
        public long Convert(long value)
        {
            var divisor = NumericSaturation.ToLong(_divisor);
            if (divisor == 0L) return value;

            var remainder = value % divisor;
            if (!_euclidean || remainder >= 0L) return remainder;

            // Unary minus rather than Math.Abs: Math.Abs(long.MinValue) throws, and a divisor that
            // large is a misconfiguration, not a reason to take down the binder dispatch.
            return remainder + (divisor < 0L ? -divisor : divisor);
        }
    }
}
