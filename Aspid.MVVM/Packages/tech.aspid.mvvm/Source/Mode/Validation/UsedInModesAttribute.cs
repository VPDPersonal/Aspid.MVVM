using System;
using System.Diagnostics;
using System.Collections.Generic;
using Attribute = UnityEngine.PropertyAttribute;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Disables the field in the Inspector while the binder that holds it has a mode outside <see cref="Modes"/>.
    /// </summary>
    /// <remarks>
    /// The field can sit on the binder or anywhere inside an object the binder serializes; the nearest binder above it decides.
    /// With no binder above it or no modes, the field stays enabled.
    /// </remarks>
    /// <example>
    /// <code>
    /// [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
    /// [SerializeField] private string _format;
    /// </code>
    /// </example>
    [Conditional(conditionString: "DEBUG")]
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Field)]
    public sealed class UsedInModesAttribute : Attribute
    {
        /// <summary>
        /// Gets the binding modes the field is used in.
        /// </summary>
        public IReadOnlyList<BindMode> Modes { get; }

        /// <summary>
        /// Creates an attribute for a field used in <paramref name="modes"/>.
        /// </summary>
        /// <param name="modes">The binding modes the field is used in.</param>
        public UsedInModesAttribute(params BindMode[] modes)
        {
            Modes = modes;
        }
    }
}
