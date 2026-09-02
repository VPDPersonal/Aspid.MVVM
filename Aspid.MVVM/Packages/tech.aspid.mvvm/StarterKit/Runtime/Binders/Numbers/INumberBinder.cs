// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Composite <see cref="IBinder{T}"/> that accepts every common numeric primitive.
    /// Implementors provide only the <see cref="int"/>, <see cref="long"/>, <see cref="float"/> and <see cref="double"/>
    /// overloads; the rest are routed to them here.
    /// </summary>
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
            SetValue(NumericSaturation.ToLong(value));

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
