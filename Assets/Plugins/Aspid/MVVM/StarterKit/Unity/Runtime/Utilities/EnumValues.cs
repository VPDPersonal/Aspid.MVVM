#nullable enable
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class EnumValues<T> : IEnumValues<Enum, T>, IEnumerable<EnumValue<T>>
    {
        [Header("Parameters")]
        [SerializeField] private T? _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        
        [SerializeField] private EnumValue<T>[] _values;
        
        private bool _isFlag;
        private bool _isInitialized;
        
        public EnumValues(params EnumValue<T>[] values)
            : this(values, default) { }

        public EnumValues(EnumValue<T>[] values, T? defaultValue, bool allowDefaultValueWhenNoValue = true)
        {
            _values = values;
            _defaultValue = defaultValue;
            _allowDefaultValueWhenNoValue = allowDefaultValueWhenNoValue;
        }
        
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
            
            if (!_allowDefaultValueWhenNoValue)
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
    
    [Serializable]
    public sealed class EnumValues<TEnum, T> : IEnumValues<TEnum, T>, IEnumerable<EnumValue<TEnum, T>>
        where TEnum : Enum
    {
        [Header("Parameters")]
        [SerializeField] private T? _defaultValue;
        [SerializeField] private bool _allowDefaultValueWhenNoValue;
        
        [SerializeField] private EnumValue<TEnum, T>[] _values;
        
        private bool _isFlag;
        private bool _isInitialized;
        
        public EnumValues(params EnumValue<TEnum, T>[] values)
            : this(values, default) { }

        public EnumValues(EnumValue<TEnum, T>[] values, T? defaultValue, bool allowDefaultValueWhenNoValue = true)
        {
            _values = values;
            _defaultValue = defaultValue;
            _allowDefaultValueWhenNoValue = allowDefaultValueWhenNoValue;
        }
        
        public void Initialize(Enum enumValue)
        {
            if (_isInitialized) return;
            
            _isFlag = enumValue.GetType().IsDefined(typeof(FlagsAttribute), false);
            _isInitialized = true;
        }
        
        public void Deinitialize() =>
            _isInitialized = false;
        
        public T? GetValue(TEnum enumValue)
        {
            Initialize(enumValue);
            
            foreach (var value in _values)
            {
                if (Equals(enumValue, value.Key!))
                    return value.Value; 
            }

            if (!_allowDefaultValueWhenNoValue)
                throw new ArgumentOutOfRangeException();
            
            return _defaultValue;
        }
        
        public bool Equals(TEnum enumValue1, TEnum enumValue2)
        {
            return _isFlag 
                ? enumValue1.HasFlag(enumValue2) 
                : enumValue1.Equals(enumValue2);
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