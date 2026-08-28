// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="INumberBinder"/> whose implementors bind an <see langword="int"/>: every other numeric type
    /// is converted to <see cref="int"/>, saturating at its bounds rather than wrapping.
    /// </summary>
    public interface IIntBinder : INumberBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<long>.SetValue(long value) =>
            SetValue(NumericSaturation.ToInt(value));

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<float>.SetValue(float value) =>
            SetValue(NumericSaturation.ToInt(value));

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<double>.SetValue(double value) =>
            SetValue(NumericSaturation.ToInt(value));
    }
}
