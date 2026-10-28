using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Aspid.MVVM
{
    /// <summary>
    /// Replaces the <see cref="BindModeAttribute"/> limits of every <see cref="BindMode"/> field of the class and its subclasses in the Inspector.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The allowed modes are <see cref="Modes"/> plus the groups that <see cref="IsOne"/> and <see cref="IsTwo"/> add; with none of
    /// them, or with <see cref="IsAll"/>, every mode except <see cref="BindMode.None"/> is allowed.
    /// </para>
    /// <para>The Inspector replaces a stored mode outside the allowed set with the first allowed one.</para>
    /// <para>A subclass with its own <see cref="BindModeOverrideAttribute"/> uses that one instead.</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// [BindModeOverride(BindMode.OneWay, BindMode.OneTime)]
    /// public class FillAmountBinder : MonoBinder { }
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class, Inherited = false)]
    public sealed class BindModeOverrideAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether every mode except <see cref="BindMode.None"/> is allowed.
        /// </summary>
        public bool IsAll { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="BindMode.OneWay"/> and <see cref="BindMode.OneTime"/> are allowed in
        /// addition to <see cref="Modes"/>.
        /// </summary>
        public bool IsOne { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/> are
        /// allowed in addition to <see cref="Modes"/>.
        /// </summary>
        public bool IsTwo { get; set; }
        
        /// <summary>
        /// Gets the explicitly allowed modes.
        /// </summary>
        public BindMode[] Modes { get; }

        /// <summary>
        /// Creates an attribute that allows <paramref name="modes"/>.
        /// </summary>
        /// <param name="modes">The allowed modes; none to allow every mode unless <see cref="IsOne"/> or <see cref="IsTwo"/> is set.</param>
        public BindModeOverrideAttribute(params BindMode[] modes)
        {
            Modes = modes;
        }
    }
}
