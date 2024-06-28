using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public sealed class RequireBinder : Attribute
    {
        public Type Type { get; }
        
        public RequireBinder(Type type)
        {
            Type = type;
        }
    }
}