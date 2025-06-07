using System;

namespace Aspid.MVVM
{
    public sealed class TwoWayEnumEvent<T> : TwoWayStructEvent<T, Enum>
        where T : struct, Enum
    {
        public TwoWayEnumEvent(T value, Action<T> setValue) 
            : base(value, setValue) { }
    }
}