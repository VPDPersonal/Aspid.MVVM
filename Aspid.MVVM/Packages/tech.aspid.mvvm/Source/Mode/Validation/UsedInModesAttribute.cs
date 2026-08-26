using System;
using System.Diagnostics;
using System.Collections.Generic;
using Attribute = UnityEngine.PropertyAttribute;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Marks a serialized field as used only under the specified binding modes, so the Inspector
    /// can disable it while the hosting binder is bound in any other.
    /// This attribute is conditional and only active when the "UNITY_EDITOR" symbol is defined.
    /// </summary>
    /// <remarks>
    /// The field can sit on the binder itself or anywhere inside a serialized object the binder
    /// holds; the nearest binder above it decides. Outside any binder the field stays enabled.
    /// </remarks>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field)]
    public sealed class UsedInModesAttribute : Attribute
    {
        /// <summary>
        /// Gets the binding modes the field is used under.
        /// </summary>
        public IReadOnlyList<BindMode> Modes { get; }

        /// <param name="modes">The binding modes the field is used under.</param>
        public UsedInModesAttribute(params BindMode[] modes)
        {
            Modes = modes;
        }
    }
}
