using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Editor-only attribute that marks a <see cref="MonoBinder"/> class for inclusion in the
    /// "Add Binder" context menu for a specific target component type.
    /// The editor uses this attribute to automatically populate serialized properties when the binder is added.
    /// </summary>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class AddBinderContextMenuAttribute : Attribute
    {
        /// <summary>
        /// The component type this binder is associated with.
        /// Used to determine which context menus display this binder entry.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Optional override for the root menu path displayed in the context menu.
        /// When <c>null</c> or empty, a default path is generated from the binder type name.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Optional sub-path appended under <see cref="Path"/> in the context menu hierarchy.
        /// </summary>
        public string SubPath { get; set; }

        /// <summary>
        /// Names of serialized properties that should be automatically populated
        /// when this binder is added via the context menu.
        /// </summary>
        public string[] SerializePropertyNames { get; }

        /// <summary>
        /// Initializes the attribute for the given component type.
        /// </summary>
        /// <param name="type">The component type this binder targets.</param>
        /// <param name="serializePropertyNames">
        /// Names of serialized properties to auto-populate when the binder is added via the context menu.
        /// </param>
        public AddBinderContextMenuAttribute(Type type, params string[] serializePropertyNames)
        {
            Type = type;
            SerializePropertyNames = serializePropertyNames;
        }
    }
}