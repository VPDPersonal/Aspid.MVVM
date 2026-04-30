using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Aspid.MVVM
{
    /// <summary>
    /// Attribute used to override allowed binding modes for a class.
    /// This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.
    /// </summary>
    [Conditional("DEBUG")]
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class BindModeOverrideAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether all binding modes (except <see cref="BindMode.None"/>) are allowed.
        /// If <c>true</c>, all modes except <see cref="BindMode.None"/> are enabled.
        /// </summary>
        public bool IsAll { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether only <see cref="BindMode.OneWay"/> and <see cref="BindMode.OneTime"/> are allowed.
        /// If <c>true</c>, only these two modes are enabled.
        /// </summary>
        public bool IsOne { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether only <see cref="BindMode.TwoWay"/> and <see cref="BindMode.OneWayToSource"/> are allowed.
        /// If <c>true</c>, only these two modes are enabled.
        /// </summary>
        public bool IsTwo { get; set; }
        
        /// <summary>
        /// Gets the array of allowed binding modes for the property.
        /// </summary>
        public BindMode[] Modes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindModeOverrideAttribute"/> class with the specified binding modes.
        /// If no modes are provided and neither <see cref="IsOne"/> nor <see cref="IsTwo"/> is set, the behavior is equivalent to <see cref="IsAll"/>.
        /// If <see cref="IsOne"/> and <see cref="IsTwo"/> are both <c>true</c>, the behavior is equivalent to <see cref="IsAll"/>.
        /// If <see cref="IsOne"/> or <see cref="IsTwo"/> is <c>true</c> and modes are provided, the allowed modes are a combination of the specified modes and the modes defined by the properties.
        /// </summary>
        /// <param name="modes">The binding modes that are allowed for the property.</param>
        public BindModeOverrideAttribute(params BindMode[] modes)
        {
            Modes = modes;
        }
    }
}