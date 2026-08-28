// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="INumberBinder"/> whose implementors bind a <see langword="long"/>: every other numeric type
    /// is converted to <see cref="long"/>, saturating at its bounds rather than wrapping.
    /// </summary>
    public interface ILongBinder : INumberBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<int>.SetValue(int value) =>
            SetValue((long)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<float>.SetValue(float value) =>
            SetValue(NumericSaturation.ToLong(value));

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<double>.SetValue(double value) =>
            SetValue(NumericSaturation.ToLong(value));
    }
}
