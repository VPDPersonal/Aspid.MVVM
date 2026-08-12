using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
// ReSharper disable UnusedParameter.Local
namespace Aspid.MVVM
{
    // TODO Move To UnityFastTools
    /// <summary>
    /// Editor-only attribute that offers a <see cref="MonoBinder"/> in the "Add Binder" context menu of a
    /// component, and of the specific serialized properties it names.
    /// </summary>
    /// <remarks>
    /// Choosing the entry adds the binder component to the same GameObject. It does not fill any of the binder's
    /// own fields — the property names decide <em>where the entry appears</em>, not what happens afterwards.
    /// </remarks>
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
        /// Intended override for the root menu path. Nothing reads it yet.
        /// </summary>
        /// <remarks>
        /// Set on a number of binders, but no code consults it, so the entry appears under the path derived from
        /// the binder type name regardless. Kept because the binders that set it describe a hierarchy worth
        /// having; documented as unimplemented rather than removed silently.
        /// </remarks>
        public string Path { get; set; }

        /// <inheritdoc cref="Path"/>
        public string SubPath { get; set; }

        /// <summary>
        /// Names of serialized properties on the target component whose context menu should offer this binder.
        /// </summary>
        /// <remarks>
        /// Matched against the leaf name of the right-clicked property, so a nested one such as
        /// <c>m_OnClick.m_PersistentCalls.m_Calls</c> is matched as <c>m_Calls</c>. A name the component does not
        /// have simply never matches, and the entry never appears — which is why a contract test checks them.
        /// </remarks>
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