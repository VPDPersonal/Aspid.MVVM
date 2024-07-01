using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Source.Binders
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public class BinderLogAttribute : Attribute
    {
        public BinderLogAttribute(string condition = "") { }
    }
}