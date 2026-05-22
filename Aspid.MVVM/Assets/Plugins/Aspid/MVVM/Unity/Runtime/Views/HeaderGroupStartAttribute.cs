#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Editor-only marker that opens a collapsible foldout starting at the decorated binder field.
    /// The foldout continues until either <see cref="HeaderGroupEndAttribute"/>, another
    /// <see cref="HeaderGroupAttribute"/> / <see cref="HeaderGroupStartAttribute"/>,
    /// or the end of the inspector list is reached.
    /// Stripped from builds outside of <c>DEBUG</c> and <c>UNITY_EDITOR</c> configurations.
    /// </summary>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupStartAttribute : Attribute
    {
        public string Title { get; }

        public HeaderGroupStartAttribute(string title)
        {
            Title = title;
        }
    }
}
