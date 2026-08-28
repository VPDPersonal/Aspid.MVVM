// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="INumberBinder"/> whose implementors bind a <see langword="float"/>: every other numeric type
    /// is converted to <see cref="float"/>, saturating a <see cref="double"/> at its bounds rather than
    /// turning an in-range value into an infinity.
    /// </summary>
    public interface IFloatBinder : INumberBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<int>.SetValue(int value) =>
            SetValue((float)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<long>.SetValue(long value) =>
            SetValue((float)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<double>.SetValue(double value) =>
            SetValue(NumericSaturation.ToFloat(value));
    }
}
