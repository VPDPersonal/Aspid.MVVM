#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="NumberConverter"/> that also converts back within the same numeric type.
    /// </summary>
    [Serializable]
    public abstract class TwoWayNumberConverter : NumberConverter,
        ITwoWayConverter<int, int>,
        ITwoWayConverter<long, long>,
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        /// <summary>
        /// Converts the specified number back.
        /// </summary>
        /// <param name="value">The number to convert back.</param>
        /// <returns>The number the forward pass was given, saturated to the return type.</returns>
        public int ConvertBack(int value) =>
            NumericSaturation.ToInt(Undo(value));

        /// <inheritdoc cref="ConvertBack(int)"/>
        public long ConvertBack(long value) =>
            NumericSaturation.ToLong(Undo(value));

        /// <inheritdoc cref="ConvertBack(int)"/>
        public float ConvertBack(float value) =>
            NumericSaturation.ToFloat(Undo(value));

        /// <inheritdoc cref="ConvertBack(int)"/>
        public double ConvertBack(double value) =>
            Undo(value);

        /// <summary>
        /// Reverses <see cref="NumberConverter.Apply"/>.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>The number the forward pass was given, in <see cref="double"/>.</returns>
        protected abstract double Undo(double value);
    }
}
