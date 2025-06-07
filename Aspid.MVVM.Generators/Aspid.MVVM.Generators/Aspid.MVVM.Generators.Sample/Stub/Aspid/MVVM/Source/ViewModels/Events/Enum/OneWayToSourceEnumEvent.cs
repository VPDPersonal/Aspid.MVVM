using System;

namespace Aspid.MVVM
{
    public sealed class OneWayToSourceEnumEvent<T> : OneWayToSourceStructEvent<T, Enum>
        where T : struct, Enum
    {
        public OneWayToSourceEnumEvent(Action<T> setValue) 
            : base(setValue) { }
    }
}