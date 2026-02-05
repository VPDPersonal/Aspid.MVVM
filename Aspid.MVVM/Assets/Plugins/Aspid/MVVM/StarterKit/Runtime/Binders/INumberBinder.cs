// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface INumberBinder :
        IBinder<int>, IBinder<uint>,
        IBinder<long>, IBinder<ulong>,
        IBinder<byte>, IBinder<sbyte>,
        IBinder<short>, IBinder<ushort>,
        IBinder<float>, IBinder<double>
    {
        void IBinder<uint>.SetValue(uint value) =>
            SetValue((long)value);
        
        void IBinder<ulong>.SetValue(ulong value) =>
            SetValue((long)value);
        
        void IBinder<sbyte>.SetValue(sbyte value) =>
            SetValue((short)value);
        
        void IBinder<byte>.SetValue(byte value) =>
            SetValue((short)value);
        
        void IBinder<short>.SetValue(short value) =>
            SetValue((int)value);
        
        void IBinder<ushort>.SetValue(ushort value) =>
            SetValue((int)value);
    }
}