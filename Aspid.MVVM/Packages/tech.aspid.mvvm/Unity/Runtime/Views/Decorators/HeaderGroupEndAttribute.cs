using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Ends the current Inspector foldout range before the decorated member.
    /// </summary>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupEndAttribute : Attribute { }
}
