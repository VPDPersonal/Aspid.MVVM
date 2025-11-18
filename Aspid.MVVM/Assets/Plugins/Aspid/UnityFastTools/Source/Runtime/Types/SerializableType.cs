#nullable enable
using System;
using UnityEngine;

// TODO Aspid.UnityFastTools – Write Summary
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [Serializable]
    public sealed class SerializableType
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        private Type? _type;
        
        public Type BaseType => typeof(object);
        
        public Type? Type
        {
            get
            {
                using (this.Marker())
                    return _type ??= Type.GetType(_assemblyQualifiedName, throwOnError: false);
            }
        }

        public static implicit operator Type?(SerializableType type) => type.Type;
    }

    [Serializable]
    public sealed class SerializableType<T>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        [SerializeField] private string _assemblyQualifiedName;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        
        private Type? _type;
        
        public Type BaseType => typeof(T);
        
        public Type? Type
        {
            get
            {
                using (this.Marker())
                    return _type ??= Type.GetType(_assemblyQualifiedName, throwOnError: false);
            }
        }

        public static implicit operator Type?(SerializableType<T> type) => type.Type;
    }
}