#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number to another numeric type under a chosen overflow policy.
    /// </summary>
    /// <remarks>
    /// A bare <c>(int)</c> cast wraps silently — a <see cref="long"/> score of
    /// <see cref="long.MaxValue"/> arrives negative — so the default is
    /// <see cref="OverflowMode.Saturate"/>, the only mode with no undefined result.
    /// <para>
    /// The class is closed over the four numeric types rather than generic because casting an
    /// unconstrained <c>TFrom</c> would go through <see cref="System.Convert"/>, which boxes on a path
    /// that runs per notification.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Numeric Cast",
        Tooltip = "Converts a number to another numeric type under a chosen overflow policy")]
    public sealed class NumericCastConverter :
        IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("What to do with a value the target type cannot hold. Saturate returns the nearest "
            + "value it can hold, Checked throws, and Unchecked reproduces a plain C# cast.")]
        [SerializeField] private OverflowMode _mode = OverflowMode.Saturate;

        /// <remarks>Default: saturating at the target type's bounds.</remarks>
        public NumericCastConverter() { }

        /// <param name="mode">What to do with a value the target type cannot hold.</param>
        public NumericCastConverter(OverflowMode mode)
        {
            _mode = mode;
        }

        #region Narrowing
        int IConverter<long, int>.Convert(long value) => ToInt(value);

        int IConverter<float, int>.Convert(float value) => ToInt(value);

        int IConverter<double, int>.Convert(double value) => ToInt(value);

        long IConverter<float, long>.Convert(float value) => ToLong(value);

        long IConverter<double, long>.Convert(double value) => ToLong(value);

        float IConverter<double, float>.Convert(double value) => ToFloat(value);
        #endregion

        #region Widening
        // No value of the source type is outside the target's range, so the mode has nothing to
        // decide. long and int still lose precision on the way to a float or double — the result is
        // the nearest representable number, which is a rounding, not an overflow.
        long IConverter<int, long>.Convert(int value) => value;

        float IConverter<int, float>.Convert(int value) => value;

        float IConverter<long, float>.Convert(long value) => value;

        double IConverter<int, double>.Convert(int value) => value;

        double IConverter<long, double>.Convert(long value) => value;

        double IConverter<float, double>.Convert(float value) => value;
        #endregion

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        /// <exception cref="OverflowException">Thrown in <see cref="OverflowMode.Checked"/> when the value does not fit.</exception>
        private int ToInt(long value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((int)value),
            OverflowMode.Checked => checked((int)value),
            OverflowMode.Saturate => NumericSaturation.ToInt(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <inheritdoc cref="ToInt(long)"/>
        private int ToInt(double value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((int)value),
            OverflowMode.Checked => CheckedToInt(value),
            OverflowMode.Saturate => NumericSaturation.ToInt(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        /// <inheritdoc cref="ToInt(long)"/>
        private long ToLong(double value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((long)value),
            OverflowMode.Checked => CheckedToLong(value),
            OverflowMode.Saturate => NumericSaturation.ToLong(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        // The range is tested by hand rather than left to checked(). A checked floating-point to
        // integral cast compiles to conv.ovf, and the runtimes disagree about it: .NET Core throws,
        // Mono — which is what the Editor and a Mono player run — does not for every out-of-range
        // value. Checked is chosen precisely to be told, so it cannot be the mode whose answer
        // depends on which runtime the game shipped with.
        private static int CheckedToInt(double value) =>
            double.IsNaN(value) || value < int.MinValue || value > int.MaxValue
                ? throw new OverflowException($"{value} does not fit in an int.")
                : (int)value;

        // 2^63 itself is the first double above the range: long.MaxValue is 2^63 - 1 and no double
        // holds it, so the bound has to be written as the power of two rather than as long.MaxValue,
        // which would silently round up to it.
        private static long CheckedToLong(double value) =>
            double.IsNaN(value) || value < long.MinValue || value >= 9223372036854775808d
                ? throw new OverflowException($"{value} does not fit in a long.")
                : (long)value;

        /// <inheritdoc cref="ToInt(long)"/>
        private float ToFloat(double value) => _mode switch
        {
            OverflowMode.Unchecked => (float)value,
            OverflowMode.Checked => CheckedToFloat(value),
            OverflowMode.Saturate => NumericSaturation.ToFloat(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
        };

        // checked() covers integral conversions only — the compiler emits nothing for a double to
        // float narrowing — so the range test is written out. A finite double that comes back
        // infinite is exactly the one that did not fit.
        private static float CheckedToFloat(double value)
        {
            var result = (float)value;
            if (!float.IsInfinity(result) || double.IsInfinity(value)) return result;

            throw new OverflowException(
                $"{nameof(NumericCastConverter)}: {value} is outside the range of a float.");
        }
    }
}
