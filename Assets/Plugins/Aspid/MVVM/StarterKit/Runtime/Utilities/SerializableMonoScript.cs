using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Utilities
{
    [Serializable]
    public class SerializableMonoScript
    { 
        [SerializeField] private string _typeName;
        
        private Type _type;
        
        public Type Type
        {
            get
            {
                if (_type != null) return _type;
                if (string.IsNullOrEmpty(_typeName)) return null;
                return _type = Type.GetType(_typeName);
            }
            set
            {
                _type = value;
                _typeName = _type?.AssemblyQualifiedName ?? "";
            }
        }

        public static implicit operator Type(SerializableMonoScript type) => type.Type;

        public static implicit operator SerializableMonoScript(Type type) => new() { Type = type };
    }
    
    [Serializable]
    public class SerializableMonoScript<T> : SerializableMonoScript { }
}