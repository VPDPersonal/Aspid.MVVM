#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Utilities
{
    public interface IEnumValue<out TEnum, out T>
        where TEnum : Enum
    {
        public T? Value { get; }
        
        public TEnum? Key { get; }
        
        public Type? Type => typeof(TEnum);
    }
}