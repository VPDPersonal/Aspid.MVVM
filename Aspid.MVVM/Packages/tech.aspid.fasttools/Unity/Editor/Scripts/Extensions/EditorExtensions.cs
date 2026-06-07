using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Editor-side extension methods for <see cref="Object"/> and its subclass <see cref="Component"/>
    /// that resolve human-readable script names, respecting the <see cref="AddComponentMenu"/> attribute.
    /// </summary>
    public static class EditorExtensions
    {
        /// <summary>
        /// Returns a human-readable display name for the given Unity object.
        /// If the object's type (or any of its base types) is decorated with <see cref="AddComponentMenu"/>,
        /// the name is taken from <see cref="ObjectNames.GetInspectorTitle(Object)"/>, which honours the menu name;
        /// otherwise it falls back to <see cref="ObjectNames.NicifyVariableName"/> applied to the type name.
        /// </summary>
        /// <param name="obj">The object whose display name should be resolved.</param>
        /// <returns>
        /// The display name string, or <see cref="string.Empty"/> if <paramref name="obj"/> is <see langword="null"/>
        /// or has been destroyed.
        /// </returns>
        public static string GetScriptName(this Object obj)
        {
            if (!obj) return string.Empty;

            var targetType = obj.GetType();
            return Attribute.IsDefined(targetType, typeof(AddComponentMenu), inherit: true)
                ? ObjectNames.GetInspectorTitle(obj)
                : ObjectNames.NicifyVariableName(targetType.Name);
        }

        /// <summary>
        /// Returns the display name of a component with a 1-based numeric suffix appended when multiple
        /// components of the exact same type exist on the same <see cref="GameObject"/>. The index reflects
        /// the order returned by <see cref="Component.GetComponents(Type)"/>.
        /// For example, the second <c>AudioSource</c> on the object is returned as <c>"Audio Source (2)"</c>.
        /// </summary>
        /// <param name="targetComponent">The component whose indexed display name should be resolved.</param>
        /// <returns>
        /// The display name with an index suffix if duplicates exist on the same object,
        /// the plain display name if there is only one such component,
        /// or <see langword="null"/> if <paramref name="targetComponent"/> is <see langword="null"/>
        /// or has been destroyed.
        /// </returns>
        public static string GetScriptNameWithIndex(this Component targetComponent)
        {
            if (!targetComponent) return null;

            var type = targetComponent.GetType();
            var components = targetComponent.GetComponents(type)
                .Where(component => component.GetType() == type)
                .ToArray();

            if (components.Length <= 1) return targetComponent.GetScriptName();

            for (var i = 0; i < components.Length; i++)
            {
                if (components[i] == targetComponent)
                    return $"{targetComponent.GetScriptName()} ({i + 1})";
            }

            return targetComponent.GetScriptName();
        }
    }
}
