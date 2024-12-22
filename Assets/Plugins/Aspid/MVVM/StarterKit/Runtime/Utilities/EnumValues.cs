#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Utilities
{
    [Serializable]
    public sealed class EnumValues<T> : IEnumValues<Enum, T>, IEnumerable<EnumValue<T>>
    {
        [Header("Parameters")]
        [SerializeField] private T? _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        
        [SerializeField] private EnumValue<T>[] _values;
        
        private bool _isEnumTypeSet;

        public EnumValues(params EnumValue<T>[] values)
            : this(values, default) { }

        public EnumValues(EnumValue<T>[] values, T? defaultValue, bool allowDefaultValueWhenNoValue = true)
        {
            _values = values;
            _defaultValue = defaultValue;
            _allowDefaultValueWhenNoValue = allowDefaultValueWhenNoValue;
        }
        
        public T? GetValue(Enum enumValue)
        {
            Initialize(enumValue);

            foreach (var value in _values)
            {
                if (!value.Key!.Equals(enumValue)) continue;
                return value.Value;
            }

            if (!_allowDefaultValueWhenNoValue)
                throw new ArgumentOutOfRangeException();
            
            return _defaultValue;
        }

        public void Deinitialize() =>
            _isEnumTypeSet = false;

        public IEnumerator<EnumValue<T>> GetEnumerator()
        {
            foreach (var value in _values)
                yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        
        private void Initialize(Enum enumValue)
        {
            if (_isEnumTypeSet) return;
            
            foreach (var value in _values)
                value.Initialize(enumValue.GetType());
                
            _isEnumTypeSet = true;
        }
    }
    
    [Serializable]
    public sealed class EnumValues<TEnum, T> : IEnumValues<TEnum, T>, IEnumerable<EnumValue<TEnum, T>>
        where TEnum : Enum
    {
        [Header("Parameters")]
        [SerializeField] private T? _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        
        [SerializeField] private EnumValue<TEnum, T>[] _values;
        
        public EnumValues(params EnumValue<TEnum, T>[] values)
            : this(values, default) { }

        public EnumValues(EnumValue<TEnum, T>[] values, T? defaultValue, bool allowDefaultValueWhenNoValue = true)
        {
            _values = values;
            _defaultValue = defaultValue;
            _allowDefaultValueWhenNoValue = allowDefaultValueWhenNoValue;
        }
        
        public T? GetValue(TEnum enumValue)
        {
            foreach (var value in _values)
            {
                if (!value.Key!.Equals(enumValue)) continue;
                return value.Value;
            }

            if (!_allowDefaultValueWhenNoValue)
                throw new ArgumentOutOfRangeException();
            
            return _defaultValue;
        }
        
        public IEnumerator<EnumValue<TEnum, T>> GetEnumerator()
        {
            foreach (var value in _values)
                yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}