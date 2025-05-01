#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Unity
{
    public interface IEnumValues<in TEnum, out T>
        where TEnum : Enum
    {
        public T? GetValue(TEnum enumValue);
    }
}