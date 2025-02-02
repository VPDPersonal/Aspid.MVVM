using UnityEngine;
using System.Diagnostics;

namespace Aspid.MVVM.Mono
{
    /// <summary>
    /// Attribute used to specify allowed binding modes for a property in the Unity Editor.
    /// This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    public sealed class BindModeAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the array of allowed binding modes for the property.
        /// </summary>
        public BindMode[] Modes { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindModeAttribute"/> class with the specified binding modes.
        /// </summary>
        /// <param name="modes">The binding modes that are allowed for the property.</param>
        public BindModeAttribute(params BindMode[] modes)
        {
            Modes = modes;
        }
    }
}