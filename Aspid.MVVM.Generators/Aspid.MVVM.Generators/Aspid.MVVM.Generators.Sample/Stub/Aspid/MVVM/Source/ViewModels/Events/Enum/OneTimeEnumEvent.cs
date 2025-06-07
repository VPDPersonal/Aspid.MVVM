using System;

namespace Aspid.MVVM
{
    public sealed class OneTimeEnumEvent<T> : OneTimeStructEvent<T, Enum>
        where T : struct, Enum
    {
        public OneTimeEnumEvent(T value) 
            : base(value) { }
    }
}