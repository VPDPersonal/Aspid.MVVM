using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Places only the decorated member in a named Inspector foldout.
    /// </summary>
    /// <remarks>Members with the same title share a foldout, including members in a foldout range.</remarks>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupAttribute : Attribute
    {
        /// <summary>
        /// Gets the foldout title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Creates a foldout group for one member.
        /// </summary>
        /// <param name="title">The foldout title. Blank titles are ignored.</param>
        public HeaderGroupAttribute(string title)
        {
            Title = title;
        }
    }
}
