using System;
using System.Diagnostics;

namespace Aspid.UI.MVVM.Mono.Views
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinderAttribute : Attribute
    {
        public Type Type { get; }
        
        public RequireBinderAttribute(Type type)
        {
            Type = type;
        }
    }
}