using System;
using System.Diagnostics;

namespace AspidUI.MVVM.Unity.Generation
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}