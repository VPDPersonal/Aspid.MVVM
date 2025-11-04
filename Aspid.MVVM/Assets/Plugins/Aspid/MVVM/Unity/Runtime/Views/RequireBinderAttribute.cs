#nullable enable
using System;
using System.Linq;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    [Conditional("DEBUG")]
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        public string? Id { get; set; }
        
        public string[]? AssemblyQualifiedNames { get; }
        
        public RequireBinderAttribute() { }
        
        public RequireBinderAttribute(Type type)
            : this(new[] { type }) { }
        
        public RequireBinderAttribute(params Type[] types)
        {
            AssemblyQualifiedNames = 
                types.Select(type => type.AssemblyQualifiedName).ToArray();
        }
        
        public RequireBinderAttribute(string assemblyQualifiedName)
            : this(new[] { assemblyQualifiedName }) { }

        public RequireBinderAttribute(params string[] assemblyQualifiedNames)
        {
            AssemblyQualifiedNames = assemblyQualifiedNames;
        }
    }
}