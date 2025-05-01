#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class EnumValue<T> : IEnumValue<Enum, T>
    {
        [SerializeField] private string _key;
        [SerializeField] private T? _value;

        public T? Value => _value;
        
        public Enum? Key { get; private set;  }
        
        public Type? Type { get; private set; }

        public EnumValue()
            : this(string.Empty, default) { }

        public EnumValue(string key, T? value)
        {
            _key = key;
            _value = value;
        }

        public void Initialize(Type type)
        {
            if (!Enum.IsDefined(type, _key))
            {
                throw new Exception($"[{nameof(EnumValue<T>)}] [{nameof(Initialize)}]" +
                    $"Couldn't parse key '{_key}' to Enum '{nameof(type)}'");
            }

            Type = type;
            Key = Enum.Parse(type, _key) as Enum;
        }
    }
    
    [Serializable]
    public sealed class EnumValue<TEnum, T>  : IEnumValue<TEnum, T>
        where TEnum : Enum
    {
        [SerializeField] private TEnum? _key;
        [SerializeField] private T? _value;

        public T? Value => _value;
        
        public TEnum? Key => _key;

        public Type Type => typeof(TEnum);

        public EnumValue()
            : this(default, default) { }

        public EnumValue(TEnum? key, T? value)
        {
            _key = key;
            _value = value;
        }
    }
}