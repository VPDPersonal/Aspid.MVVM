using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Starts an Inspector foldout range at the decorated member.
    /// </summary>
    /// <remarks>
    /// The range ends before the next <see cref="HeaderGroupEndAttribute"/> or <see cref="HeaderGroupStartAttribute"/> with a title.
    /// </remarks>
    /// <example>
    /// <code>
    /// [HeaderGroupStart("Stats")]
    /// [Bind] private int _health;
    /// [Bind] private int _stamina;
    /// [HeaderGroupEnd]
    /// [Bind] private string _playerName;
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
        /// Creates a foldout range with the given title.
        /// </summary>
        /// <param name="title">The foldout title. Blank titles are ignored.</param>
        public HeaderGroupStartAttribute(string title)
        {
            Title = title;
        }
    }
}
