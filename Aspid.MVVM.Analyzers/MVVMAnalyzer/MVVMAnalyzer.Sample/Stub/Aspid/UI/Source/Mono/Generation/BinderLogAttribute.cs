using System;
using System.Diagnostics;

namespace Aspid.UI.MVVM.Mono.Generation
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute { }
}