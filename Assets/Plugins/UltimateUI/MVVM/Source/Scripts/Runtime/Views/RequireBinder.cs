using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class RequireBinder : Attribute
    {
        public Type Type { get; }
        
        public RequireBinder(Type type)
        {
            Type = type;
        }
    }
}