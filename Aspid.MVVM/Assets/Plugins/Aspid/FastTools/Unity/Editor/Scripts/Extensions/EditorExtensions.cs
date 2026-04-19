using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.FastTools.Editors
{
    /// <summary>
    /// Editor-side extension methods for <see cref="UnityEngine.Object"/> and <see cref="Component"/>
    /// that resolve human-readable script names, respecting the <see cref="AddComponentMenu"/> attribute.
    /// </summary>
    public static class EditorExtensions
    {
        /// <summary>
        /// Returns a human-readable display name for the given Unity object.
        /// If the object's type is decorated with <see cref="AddComponentMenu"/>, the name is taken from
        /// <see cref="ObjectNames.GetInspectorTitle"/>; otherwise it falls back to
        /// <see cref="ObjectNames.NicifyVariableName"/> applied to the type name.
        /// </summary>
        /// <param name="obj">The object whose display name should be resolved.</param>
        /// <returns>
        /// The display name string, or <see cref="string.Empty"/> if <paramref name="obj"/> is <see langword="null"/>.
        /// </returns>
        public static string GetScriptName(this Object obj)
        {
            if (!obj) return string.Empty;

            var targetType = obj.GetType();
            var attributes = targetType.GetCustomAttributes(false);

            return attributes.Any(attribute => attribute is AddComponentMenu)
                ? ObjectNames.GetInspectorTitle(obj)
                : ObjectNames.NicifyVariableName(targetType.Name);
        }

        /// <summary>
        /// Returns the display name of a component with a numeric suffix appended when multiple components
        /// of the same type exist on the same <see cref="GameObject"/>.
        /// For example, the second <c>AudioSource</c> would be returned as <c>"Audio Source (2)"</c>.
        /// </summary>
        /// <param name="targetComponent">The component whose indexed display name should be resolved.</param>
        /// <returns>
        /// The display name with an index suffix if duplicates exist on the same object,
        /// the plain display name if there is only one such component,
        /// or <see langword="null"/> if <paramref name="targetComponent"/> is <see langword="null"/>.
        /// </returns>
        public static string GetScriptNameWithIndex(this Component targetComponent)
        {
            if (targetComponent is null) return null;

            var type = targetComponent.GetType();
            var components = targetComponent
                .GetComponents(type)
                .Where(component => component.GetType() == targetComponent.GetType())
                .ToArray();

            switch (components.Length)
            {
                case 0:
                case 1: return targetComponent.GetScriptName();
                default:
                    {
                        var index = 0;

                        foreach (var component in components)
                        {
                            if (component.GetType() != type) continue;

                            index++;
                            if (component == targetComponent)
                                return $"{targetComponent.GetScriptName()} ({index})";
                        }

                        return targetComponent.GetScriptName();
                    }
            }
        }
    }
}
