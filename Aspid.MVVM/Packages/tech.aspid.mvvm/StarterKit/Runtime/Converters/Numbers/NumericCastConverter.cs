#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number to another numeric type under a chosen overflow policy.
    /// </summary>
    /// <remarks>Widening conversions never overflow, so the mode applies to narrowing ones only.</remarks>
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

        /// <param name="mode">What to do with a value the target type cannot hold. <see cref="OverflowMode.Checked"/> throws.</param>
        public NumericCastConverter(OverflowMode mode)
        {
            _mode = mode;
        }

        int IConverter<long, int>.Convert(long value) =>
            ToInt(value);

        int IConverter<float, int>.Convert(float value) =>
            ToInt(value);

        int IConverter<double, int>.Convert(double value) =>
            ToInt(value);

        long IConverter<float, long>.Convert(float value) =>
            ToLong(value);

        long IConverter<double, long>.Convert(double value) =>
            ToLong(value);

        float IConverter<double, float>.Convert(double value) =>
            ToFloat(value);

        long IConverter<int, long>.Convert(int value) =>
            value;

        float IConverter<int, float>.Convert(int value) =>
            value;

        float IConverter<long, float>.Convert(long value) =>
            value;

        double IConverter<int, double>.Convert(int value) =>
            value;

        double IConverter<long, double>.Convert(long value) =>
            value;

        double IConverter<float, double>.Convert(float value) =>
            value;

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

        private static int CheckedToInt(long value) =>
            value < int.MinValue || value > int.MaxValue
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of an int.")
                : (int)value;

        // Tested by hand: conv.ovf on a double behaves differently on Mono and .NET Core.
        private static int CheckedToInt(double value) =>
            double.IsNaN(value) || value < int.MinValue || value > int.MaxValue
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of an int.")
                : (int)value;

        // 2^63 is the first double above the range; long.MaxValue itself rounds up to it.
        private static long CheckedToLong(double value) =>
            double.IsNaN(value) || value < long.MinValue || value >= 9223372036854775808d
                ? throw new OverflowException(
                    $"{nameof(NumericCastConverter)}: {value} is outside the range of a long.")
                : (long)value;

        private static float CheckedToFloat(double value)
        {
            var result = (float)value;
            if (!float.IsInfinity(result) || double.IsInfinity(value)) return result;

            throw new OverflowException(
                $"{nameof(NumericCastConverter)}: {value} is outside the range of a float.");
        }

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
            problem: $"the mode {_mode.Describe()} is not a declared {nameof(OverflowMode)}",
            consequence: "Saturating at the target type's bounds.");
    }
}
