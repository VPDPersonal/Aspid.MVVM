using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number to another numeric type under a chosen overflow policy.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="OverflowMode.Saturate"/>, the only mode with no undefined result. The
    /// class is closed over the four numeric types rather than generic because casting an unconstrained
    /// <c>TFrom</c> would go through <see cref="System.Convert"/>, which boxes on a path that runs per
    /// notification.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Cast",
        Tooltip = "Converts a number to another numeric type under a chosen overflow policy")]
    public sealed class NumericCastConverter :
        IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("What to do with a value the target type cannot hold. Checked throws.")]
        [SerializeField] private OverflowMode _mode = OverflowMode.Saturate;

        /// <remarks>Default: saturating at the target type's bounds.</remarks>
        public NumericCastConverter() { }

        /// <param name="mode">
        /// What to do with a value the target type cannot hold. <see cref="OverflowMode.Checked"/>
        /// throws instead of converting.
        /// </param>
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
        // decide. A long or int still rounds on the way to a float — precision, not overflow.
        long IConverter<int, long>.Convert(int value) => value;

        float IConverter<int, float>.Convert(int value) => value;

        float IConverter<long, float>.Convert(long value) => value;

        double IConverter<int, double>.Convert(int value) => value;

        double IConverter<long, double>.Convert(long value) => value;

        double IConverter<float, double>.Convert(float value) => value;
        #endregion

        private int ToInt(long value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((int)value),
            OverflowMode.Checked => CheckedToInt(value),
            OverflowMode.Saturate => NumericSaturation.ToInt(value),
            _ => UndeclaredToInt(value)
        };

        private int ToInt(double value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((int)value),
            OverflowMode.Checked => CheckedToInt(value),
            OverflowMode.Saturate => NumericSaturation.ToInt(value),
            _ => UndeclaredToInt(value)
        };

        private long ToLong(double value) => _mode switch
        {
            OverflowMode.Unchecked => unchecked((long)value),
            OverflowMode.Checked => CheckedToLong(value),
            OverflowMode.Saturate => NumericSaturation.ToLong(value),
            _ => UndeclaredToLong(value)
        };

        private float ToFloat(double value) => _mode switch
        {
            OverflowMode.Unchecked => (float)value,
            OverflowMode.Checked => CheckedToFloat(value),
            OverflowMode.Saturate => NumericSaturation.ToFloat(value),
            _ => UndeclaredToFloat(value)
        };

        // Written out rather than left to checked((int)value) so the message names the converter;
        // the framework's own reads only "Arithmetic operation resulted in an overflow".
        private static int CheckedToInt(long value) =>
            value < int.MinValue || value > int.MaxValue
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of an int.")
                : (int)value;

        // The range is tested by hand rather than left to checked(): conv.ovf behaves differently on
        // Mono — what the Editor and a Mono player run — than on .NET Core, and Checked is chosen
        // precisely to be told, not to depend on the runtime the game shipped with.
        private static int CheckedToInt(double value) =>
            double.IsNaN(value) || value < int.MinValue || value > int.MaxValue
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of an int.")
                : (int)value;

        // 2^63 is the first double above the range: long.MaxValue is 2^63 - 1 and no double holds it,
        // so writing the bound as long.MaxValue would silently round it up.
        private static long CheckedToLong(double value) =>
            double.IsNaN(value) || value < long.MinValue || value >= 9223372036854775808d
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of a long.")
                : (long)value;

        // checked() covers integral conversions only, so the range test is written out: a finite
        // double that comes back infinite is exactly the one that did not fit.
        private static float CheckedToFloat(double value)
        {
            var result = (float)value;
            if (!float.IsInfinity(result) || double.IsInfinity(value)) return result;

            throw new OverflowException(
                $"{nameof(NumericCastConverter)}: {value} is outside the range of a float.");
        }

        // The answer is the default mode's: saturation is the only policy with no undefined result.
        private int UndeclaredToInt(long value)
        {
            LogUndeclaredMode();
            return NumericSaturation.ToInt(value);
        }

        private int UndeclaredToInt(double value)
        {
            LogUndeclaredMode();
            return NumericSaturation.ToInt(value);
        }

        private long UndeclaredToLong(double value)
        {
            LogUndeclaredMode();
            return NumericSaturation.ToLong(value);
        }

        private float UndeclaredToFloat(double value)
        {
            LogUndeclaredMode();
            return NumericSaturation.ToFloat(value);
        }

        private void LogUndeclaredMode() => this.LogError(
            $"the mode {_mode.Describe()} is not a declared {nameof(OverflowMode)}",
            "Saturating at the target type's bounds.");
    }
}
