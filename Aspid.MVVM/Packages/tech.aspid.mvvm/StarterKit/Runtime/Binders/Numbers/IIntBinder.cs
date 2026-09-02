// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="INumberBinder"/> whose implementors bind an <see langword="int"/>; wider types saturate at the bounds instead of wrapping.
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
