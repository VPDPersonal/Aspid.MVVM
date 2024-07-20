using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BinderLogAttribute : Attribute
    {
#if !ULTIMATE_UI_MVVM_BINDER_LOG_DISABLED
        public const bool Enabled = true;
#else
        public const bool Disabled = false;
#endif
    }
}