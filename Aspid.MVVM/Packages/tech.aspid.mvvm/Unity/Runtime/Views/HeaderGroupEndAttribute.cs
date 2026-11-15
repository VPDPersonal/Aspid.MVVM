using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Ends the current foldout range before the decorated member.
    /// </summary>
    /// <remarks>
    /// The decorated member is displayed outside the closed range.
    /// Attribute applications are omitted unless <c>DEBUG</c> or <c>UNITY_EDITOR</c> is defined.
    /// </remarks>
    /// <example>
    /// <code>
    /// [HeaderGroupEnd]
    /// [Bind] private int _health;
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupEndAttribute : Attribute { }
}
