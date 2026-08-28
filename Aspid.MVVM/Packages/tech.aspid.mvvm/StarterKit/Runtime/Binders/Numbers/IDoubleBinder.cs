// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="INumberBinder"/> whose implementors bind a <see langword="double"/>: every other numeric type
    /// widens to <see cref="double"/> without loss.
    /// </summary>
    public interface IDoubleBinder : INumberBinder
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<int>.SetValue(int value) =>
            SetValue((double)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<long>.SetValue(long value) =>
            SetValue((double)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<float>.SetValue(float value) =>
            SetValue((double)value);
    }
}
