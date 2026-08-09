// The Unity-independent core (Source) and StarterKit runtime are compiled both inside Unity and as a
// plain .NET library (see Aspid.MVVM.Generators.Sample). Inside Unity these attributes come from
// UnityEngine.CoreModule; outside it they do not exist, which would otherwise force every annotated
// field to be wrapped in `#if UNITY_2022_1_OR_NEWER`. These no-op stubs stand in for them instead,
// so the attributes can be written unconditionally at every usage site.
#if !UNITY_2022_1_OR_NEWER
using System;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedMember.Global
namespace UnityEngine
{
    /// <summary>
    /// Stand-in for Unity's base class for attributes that customise how a field is drawn in the Inspector.
    /// Compiled only outside Unity, where <c>UnityEngine.CoreModule</c> is unavailable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class PropertyAttribute : Attribute { }

    /// <summary>
    /// Stand-in for Unity's attribute that forces a private field to be serialised.
    /// Compiled only outside Unity, where <c>UnityEngine.CoreModule</c> is unavailable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SerializeField : Attribute { }

    /// <summary>
    /// Stand-in for Unity's attribute that serialises a field by reference, preserving its concrete type.
    /// Compiled only outside Unity, where <c>UnityEngine.CoreModule</c> is unavailable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class SerializeReference : Attribute { }

    /// <summary>
    /// Stand-in for Unity's attribute that shows a hint above a field in the Inspector.
    /// Compiled only outside Unity, where <c>UnityEngine.CoreModule</c> is unavailable.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class TooltipAttribute : PropertyAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooltipAttribute"/> class.
        /// </summary>
        /// <param name="tooltip">The hint shown above the field in the Inspector. Ignored outside Unity.</param>
        public TooltipAttribute(string tooltip) => this.tooltip = tooltip;

        /// <summary>
        /// Gets the hint shown above the field in the Inspector.
        /// </summary>
        // ReSharper disable once InconsistentNaming — mirrors the public field name of Unity's TooltipAttribute.
        public string tooltip { get; }
    }
}
#endif
