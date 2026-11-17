using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Starts a foldout range at the decorated member.
    /// </summary>
    /// <remarks>
    /// The range ends before <see cref="HeaderGroupEndAttribute"/> or another <see cref="HeaderGroupStartAttribute"/>.
    /// <see cref="HeaderGroupAttribute"/> affects only its own member.
    /// Attribute applications are omitted unless <c>DEBUG</c> or <c>UNITY_EDITOR</c> is defined.
    /// </remarks>
    /// <example>
    /// <code>
    /// [HeaderGroupStart("Stats")]
    /// [Bind] private int _health;
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class HeaderGroupStartAttribute : Attribute
    {
        /// <summary>
        /// Gets the foldout title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Creates a grouping attribute with the supplied title.
        /// </summary>
        /// <param name="title">The foldout title; blank titles are ignored.</param>
        public HeaderGroupStartAttribute(string title)
        {
            Title = title;
        }
    }
}
