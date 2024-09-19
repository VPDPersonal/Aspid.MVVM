using System;
using System.Diagnostics;

namespace AspidUI.MVVM.Unity.Views
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