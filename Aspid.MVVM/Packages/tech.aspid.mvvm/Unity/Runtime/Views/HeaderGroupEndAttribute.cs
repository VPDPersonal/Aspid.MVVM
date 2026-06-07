#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor-only marker that closes the currently open foldout group before the decorated
    /// binder field is processed. The decorated field itself is rendered outside of the closed group.
    /// Stripped from builds outside of <c>DEBUG</c> and <c>UNITY_EDITOR</c> configurations.
    /// </summary>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupEndAttribute : Attribute { }
}
