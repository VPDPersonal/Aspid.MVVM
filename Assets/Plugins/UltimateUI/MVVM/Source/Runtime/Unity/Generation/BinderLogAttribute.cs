using System;
using System.Diagnostics;

namespace UltimateUI.MVVM.Unity.Generation
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}