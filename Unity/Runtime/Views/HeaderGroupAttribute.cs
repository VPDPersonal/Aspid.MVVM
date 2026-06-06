#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor-only marker that places the decorated binder field into a collapsible foldout with
    /// the supplied title. Fields decorated with the same title merge into a single foldout
    /// regardless of declaration order. Unlike <see cref="HeaderGroupStartAttribute"/>, this
    /// attribute does not open a range — subsequent fields without their own grouping fall back to
    /// the surrounding range (or the root, if no enclosing range is open).
    /// Stripped from builds outside of <c>DEBUG</c> and <c>UNITY_EDITOR</c> configurations.
    /// </summary>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupAttribute : Attribute
    {
        public string Title { get; }

        public HeaderGroupAttribute(string title)
        {
            Title = title;
        }
    }
}
