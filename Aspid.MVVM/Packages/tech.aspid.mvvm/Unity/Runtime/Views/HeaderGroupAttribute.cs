using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Groups the decorated member under the supplied foldout title.
    /// </summary>
    /// <remarks>
    /// Affects only this member; subsequent members keep the current range. Equal titles share a foldout.
    /// Attribute applications are omitted unless <c>DEBUG</c> or <c>UNITY_EDITOR</c> is defined.
    /// </remarks>
    /// <example>
    /// <code>
    /// [HeaderGroup("Stats")]
    /// [Bind] private int _health;
    /// </code>
    /// </example>
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
        /// Creates a grouping attribute with the supplied title.
        /// </summary>
        /// <param name="title">The foldout title; blank titles are ignored.</param>
        public HeaderGroupAttribute(string title)
        {
            Title = title;
        }
    }
}
