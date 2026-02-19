#nullable enable
using System;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

// TODO Aspid.UnityFastTools – Write summary
// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [Conditional(conditionString: "UNITY_EDITOR")]
    public sealed class TypeSelectorAttribute : PropertyAttribute
    {
        public readonly string[] AssemblyQualifiedNames;

        public TypeSelectorAttribute()
            : this(typeof(object)) { }
        
        public TypeSelectorAttribute(Type type)
            : this(types: type) { }

        public TypeSelectorAttribute(params Type[] types)
        {
            AssemblyQualifiedNames = types
                .Select(type => type.AssemblyQualifiedName)
                .ToArray();
        }
        
        public TypeSelectorAttribute(string assemblyQualifiedName)
            : this(assemblyQualifiedNames: assemblyQualifiedName) { }
        
        public TypeSelectorAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames;
        }
    }
}