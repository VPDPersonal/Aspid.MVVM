#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base for a converter that transforms a number and accepts every numeric type.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>: the int and long results truncate and saturate, the float result saturates.
    /// </remarks>
    [Serializable]
    public abstract class NumberConverter :
        IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        /// <summary>
        /// Converts the specified number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>The result, saturated to the return type.</returns>
        public int Convert(int value) =>
            NumericSaturation.ToInt(Apply(value));

        /// <inheritdoc cref="Convert(int)"/>
        public long Convert(long value) =>
            NumericSaturation.ToLong(Apply(value));

        /// <inheritdoc cref="Convert(int)"/>
        public float Convert(float value) =>
            NumericSaturation.ToFloat(Apply(value));

        /// <inheritdoc cref="Convert(int)"/>
        public double Convert(double value) =>
            Apply(value);

        int IConverter<long, int>.Convert(long value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<float, int>.Convert(float value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<double, int>.Convert(double value) =>
            NumericSaturation.ToInt(Apply(value));

        long IConverter<int, long>.Convert(int value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<float, long>.Convert(float value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<double, long>.Convert(double value) =>
            NumericSaturation.ToLong(Apply(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Apply(value));

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);

        /// <summary>
        /// Transforms the specified number.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>The result, in <see cref="double"/>.</returns>
        protected abstract double Apply(double value);
    }
}
