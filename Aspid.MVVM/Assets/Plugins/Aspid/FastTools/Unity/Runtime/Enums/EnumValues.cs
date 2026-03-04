using System;
using UnityEngine;
using Unity.Profiling;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    // TODO Aspid.FastTools – Write summary
    [Serializable]
    public sealed class EnumValues<TValue> : IEnumerable<KeyValuePair<Enum, TValue>>
    {
        [TypeSelector(typeof(Enum))]
        [SerializeField] private string _enumType;
        [SerializeField] private TValue _defaultValue;
        [SerializeField] private EnumValue<TValue>[] _values;

        private bool _isFlag;
        private bool _isInitialized;

        private void Initialize()
        {
            if (_isInitialized) return;
            
            using (EnumValues.InitializeMarker.Auto())
            {
                var type = Type.GetType(_enumType, throwOnError: true);
            
                foreach (var value in _values)
                    value.Initialize(type);
            
                _isFlag = type.IsDefined(typeof(FlagsAttribute), false);
                _isInitialized = true;
            }
        }
        
        public TValue GetValue(Enum enumValue)
        {
            using (EnumValues.GetValueMarker.Auto())
            {
                Initialize();
            
                foreach (var value in _values)
                {
                    if (Equals(enumValue, value.Key))
                        return value.Value; 
                }

                return _defaultValue;
            }
        }

        public bool Equals(Enum enumValue1, Enum enumValue2)
        {
            using (EnumValues.EqualsMarker.Auto())
            {
                Initialize();
                
                if (_isFlag)
                {
                    using (EnumValues.HasFlagMarker.Auto())
                    {
                        if (!enumValue1.HasFlag(enumValue2)) return false;
                
                        var isEnum1None = Convert.ToInt32(enumValue1) is 0;
                        var isEnum2None = Convert.ToInt32(enumValue2) is 0;
                        return isEnum1None == isEnum2None;
                    }
                }
            
                return enumValue1.Equals(enumValue2);
            }
        }

        public IEnumerator<KeyValuePair<Enum, TValue>> GetEnumerator()
        {
            Initialize();
            
            foreach (var value in _values)
            {
                yield return new KeyValuePair<Enum, TValue>(value.Key, value.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
    
    internal static class EnumValues
    {
        public static readonly ProfilerMarker EqualsMarker = new($"{nameof(EnumValues)}.Equals");
        public static readonly ProfilerMarker HasFlagMarker = new($"{nameof(EnumValues)}.HasFlag");
        public static readonly ProfilerMarker GetValueMarker = new($"{nameof(EnumValues)}.GetValue");
        public static readonly ProfilerMarker InitializeMarker = new($"{nameof(EnumValues)}.Initialize");
    }
}