#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class EnumValues<T> : IEnumerable<EnumValue<T>>
    {
        [SerializeField] private T? _defaultValue;
        [SerializeField] private bool _isDefaultValue;
        [SerializeField] private EnumValue<T>[] _values;
        
#if UNITY_EDITOR
        [HideInInspector]
        [SerializeField] private string _enumType;
#endif
        
        private bool _isFlag;
        private bool _isInitialized;
        
        public EnumValues(params EnumValue<T>[] values)
            : this(values, default) { }

#pragma warning disable CS8618
        public EnumValues(EnumValue<T>[] values, T? defaultValue, bool isDefaultValue = true)
        {
            _values = values;
            _defaultValue = defaultValue;
            _isDefaultValue = isDefaultValue;
        }
#pragma warning restore
        
        public void Initialize(Enum enumValue)
        {
            if (_isInitialized) return;
            
            foreach (var value in _values)
                value.Initialize(enumValue.GetType());
            
            _isFlag = enumValue.GetType().IsDefined(typeof(FlagsAttribute), false);
            _isInitialized = true;
        }

        public void Deinitialize() =>
            _isInitialized = false;
        
        public T? GetValue(Enum enumValue)
        {
            Initialize(enumValue);
            
            foreach (var value in _values)
            {
                if (Equals(enumValue, value.Key!))
                    return value.Value; 
            }
            
            if (!_isDefaultValue)
                throw new ArgumentOutOfRangeException();
            
            return _defaultValue;
        }

        public bool Equals(Enum enumValue1, Enum enumValue2)
        {
            return _isFlag 
                ? enumValue1.HasFlag(enumValue2) 
                : enumValue1.Equals(enumValue2);
        }

        public IEnumerator<EnumValue<T>> GetEnumerator()
        {
            foreach (var value in _values)
                yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}