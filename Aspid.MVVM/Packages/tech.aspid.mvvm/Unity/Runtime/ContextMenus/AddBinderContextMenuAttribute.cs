#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Adds an Inspector context menu entry to the named serialized properties of a component.
    /// </summary>
    /// <remarks>Selecting the entry adds the decorated binder to the inspected component's GameObject.</remarks>
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
        /// Gets or sets the full menu path. A blank path uses the generated path.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the submenu under <c>Add Binder</c>. A nonblank <see cref="Path"/> takes precedence.
        /// </summary>
        public string? SubPath { get; set; }

        /// <summary>
        /// Gets the leaf names of serialized properties whose context menus offer the entry.
        /// </summary>
        public string[] SerializePropertyNames { get; }

        /// <summary>
        /// Creates an entry matched by component type and property name.
        /// </summary>
        /// <param name="type">The target component type.</param>
        /// <param name="serializePropertyNames">The serialized property leaf names. An empty array matches no properties.</param>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> or <paramref name="serializePropertyNames"/> is <see langword="null"/>.</exception>
        public AddBinderContextMenuAttribute(
            Type type,
            params string[] serializePropertyNames)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            SerializePropertyNames = serializePropertyNames ?? throw new ArgumentNullException(nameof(serializePropertyNames));
        }
    }
}
