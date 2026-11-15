#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Offers the decorated component in a target component menu and in the menus of its named serialized properties.
    /// </summary>
    /// <remarks>Adds the component to the same GameObject without populating its fields. Applications are Editor-only.</remarks>
    /// <example>
    /// <code>
    /// [AddBinderContextMenu(typeof(UnityEngine.Renderer), "m_Enabled", SubPath = "Enabled")]
    /// public sealed class EnabledBinder : MonoBinder { }
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class AddBinderContextMenuAttribute : Attribute
    {
        /// <summary>
        /// Gets the component type whose serialized properties offer this entry.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets or sets the complete menu entry path; <see langword="null"/> uses the generated path.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the group below the generated menu root; ignored when <see cref="Path"/> is specified.
        /// </summary>
        public string? SubPath { get; set; }

        /// <summary>
        /// Gets the leaf names of serialized properties whose context menus offer the entry.
        /// </summary>
        public string[] SerializePropertyNames { get; }

        /// <summary>
        /// Creates an entry restricted to the supplied component type and property names.
        /// </summary>
        /// <param name="type">The target component type.</param>
        /// <param name="serializePropertyNames">The serialized property leaf names; an empty array offers only the component menu entry.</param>
        /// <exception cref="ArgumentNullException">An argument is <see langword="null"/>.</exception>
        public AddBinderContextMenuAttribute(
            Type type,
            params string[] serializePropertyNames)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            SerializePropertyNames = serializePropertyNames ?? throw new ArgumentNullException(nameof(serializePropertyNames));
        }
    }
}
