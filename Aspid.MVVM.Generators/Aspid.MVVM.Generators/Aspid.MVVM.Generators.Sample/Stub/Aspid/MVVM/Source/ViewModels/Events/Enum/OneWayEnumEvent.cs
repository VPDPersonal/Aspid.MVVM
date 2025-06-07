using System;

namespace Aspid.MVVM
{
    public sealed class OneWayEnumEvent<T> : OneWayStructEvent<T, Enum>
        where T : struct, Enum
    {
        public OneWayEnumEvent(T value) 
            : base(value) { }
    }
}