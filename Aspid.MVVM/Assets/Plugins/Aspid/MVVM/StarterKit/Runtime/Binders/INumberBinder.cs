// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A composite binder interface that accepts values of all common numeric primitive types
    /// (<see cref="int"/>, <see cref="uint"/>, <see cref="long"/>, <see cref="ulong"/>,
    /// <see cref="byte"/>, <see cref="sbyte"/>, <see cref="short"/>, <see cref="ushort"/>,
    /// <see cref="float"/>, <see cref="double"/>) and routes them to a unified integer or
    /// floating-point setter.
    /// </summary>
    /// <remarks>
    /// Default interface method implementations handle the widening/narrowing conversions so that
    /// implementors only need to provide <see cref="SetValue(int)"/>, <see cref="SetValue(long)"/>,
    /// <see cref="SetValue(float)"/>, and <see cref="SetValue(double)"/>. Unsigned and shorter integer types
    /// are automatically cast to the nearest appropriate signed type before dispatch:
    /// <list type="bullet">
    ///   <item><see cref="uint"/> and <see cref="ulong"/> are cast to <see cref="long"/>.</item>
    ///   <item><see cref="byte"/> and <see cref="sbyte"/> are cast to <see cref="short"/>.</item>
    ///   <item><see cref="short"/> and <see cref="ushort"/> are cast to <see cref="int"/>.</item>
    /// </list>
    /// This interface is primarily intended for binders that target numeric UI elements (e.g., sliders,
    /// progress bars) that need to accept bound values regardless of the exact numeric type on the ViewModel.
    /// </remarks>
    public interface INumberBinder :
        IBinder<int>, IBinder<uint>,
        IBinder<long>, IBinder<ulong>,
        IBinder<byte>, IBinder<sbyte>,
        IBinder<short>, IBinder<ushort>,
        IBinder<float>, IBinder<double>
    {
        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<uint>.SetValue(uint value) =>
            SetValue((long)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<ulong>.SetValue(ulong value) =>
            SetValue((long)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<sbyte>.SetValue(sbyte value) =>
            SetValue((short)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<byte>.SetValue(byte value) =>
            SetValue((short)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<short>.SetValue(short value) =>
            SetValue((int)value);

        /// <inheritdoc cref="IBinder{T}.SetValue"/>
        void IBinder<ushort>.SetValue(ushort value) =>
            SetValue((int)value);
    }
}