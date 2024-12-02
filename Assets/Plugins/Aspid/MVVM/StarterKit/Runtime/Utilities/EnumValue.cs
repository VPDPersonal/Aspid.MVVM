#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Utilities
{
    [Serializable]
    public sealed class EnumValue<T>
    {
#pragma warning disable CS8618
        [SerializeField] private string _key;
        [SerializeField] private T _value;
#pragma warning restore CS8618

        public T Value => _value;
        
        public Enum? Key { get; private set;  }
        
        public Type? EnumType { get; private set; }
        
        public void SetType(Type enumType)
        {
            if (!Enum.IsDefined(enumType, _key))
            {
                throw new Exception($"[{nameof(EnumValue<T>)}] [{nameof(SetType)}]" +
                    $"Couldn't parse key '{_key}' to Enum '{nameof(enumType)}'");
            }
                
            Key = Enum.Parse(enumType, _key) as Enum;
        }
    }
}