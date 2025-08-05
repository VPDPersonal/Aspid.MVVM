#nullable enable
using System;
using UnityEngine;
using System.ComponentModel;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class EnumValue<T>
    {
        [SerializeField] private string _key;
        [SerializeField] private T? _value;
        
#if UNITY_EDITOR
        [HideInInspector]
        [SerializeField] private string _enumType;
#endif

        public T? Value => _value;
        
        public Enum? Key { get; private set;  }
        
        public Type? Type { get; private set; }

        public EnumValue()
            : this(string.Empty, default) { }

#pragma warning disable CS8618
        public EnumValue(string key, T? value)
        {
            _key = key;
            _value = value;
        }
#pragma warning restore

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
}