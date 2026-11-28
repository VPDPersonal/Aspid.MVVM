using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Overrides the <see cref="BindModeAttribute"/> restrictions for the class and its subclasses.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Combine <see cref="Modes"/>, <see cref="IsOne"/> and <see cref="IsTwo"/> to select modes.
    /// With no selection or with <see cref="IsAll"/>, all valid modes are available.
    /// </para>
    /// <para>The Inspector replaces a stored mode outside the allowed set with the first allowed one.</para>
    /// <para>The serializable binder generator applies the same restrictions to the generated constructor.</para>
    /// <para>The nearest override in the class hierarchy takes precedence.</para>
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
    [SuppressMessage(category: "ReSharper", checkId: "UnusedAutoPropertyAccessor.Global")]
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
        /// <exception cref="ArgumentNullException"><paramref name="modes"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An element of <paramref name="modes"/> is <see cref="BindMode.None"/> or undefined.</exception>
        public BindModeOverrideAttribute(params BindMode[] modes)
        {
            BindModeExtensions.ValidateModes(modes);
            Modes = modes;
        }
    }
}
