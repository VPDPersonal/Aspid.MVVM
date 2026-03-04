using System;
using UnityEngine;
using Unity.Profiling;
using Unity.Collections.LowLevel.Unsafe;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools
{
    [Serializable]
    public sealed class EnumValue<TValue>
    {
        [SerializeField] private string _key;
        [SerializeField] private TValue _value;
        
#if UNITY_EDITOR
        [SerializeField] private string _enumType;
#endif

        internal TValue Value => _value;
        
        internal Enum Key { get; private set; }

        internal Type Type { get; private set; }
        
        internal void Initialize(Type type)
        {
            // TODO Aspid.FastTools – Add Define for Marker
            using (EnumValue.InitializeMarker.Auto())
            {
                if (Enum.TryParse(type, _key, out var parsedEnum))
                {
                    Type = type;
                    Key = UnsafeUtility.As<object, Enum>(ref parsedEnum);
                }
                else
                {
                    // Not Exception. Because this is a visual error.
                    Debug.LogError($"[{nameof(EnumValue<TValue>)}] [{nameof(Initialize)}]" +
                        $"Couldn't parse key '{_key}' to Enum '{nameof(type)}'");
                }   
            }
        }
    }
    
    internal static class EnumValue
    {
        public static readonly ProfilerMarker InitializeMarker = new($"{nameof(EnumValue)}.Initialize");
    }
}