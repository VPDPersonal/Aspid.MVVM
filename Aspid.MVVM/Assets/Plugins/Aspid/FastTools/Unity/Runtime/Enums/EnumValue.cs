using System;
using UnityEngine;
using Unity.Collections.LowLevel.Unsafe;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// A single serializable entry in an <see cref="EnumValues{TValue}"/> collection,
    /// associating one enum member with a value of type <typeparamref name="TValue"/>.
    /// </summary>
    /// <typeparam name="TValue">The type of the value mapped to the enum member.</typeparam>
    /// <remarks>
    /// The enum key is stored as a string and resolved to an <see cref="Enum"/> instance
    /// lazily via <see cref="Initialize"/>. This class is managed entirely by
    /// <see cref="EnumValues{TValue}"/> and is not intended for direct instantiation.
    /// </remarks>
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
#if !ASPID_FAST_TOOLS_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
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
}
