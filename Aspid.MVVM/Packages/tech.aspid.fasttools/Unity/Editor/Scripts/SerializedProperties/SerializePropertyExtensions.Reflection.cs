using System;
using UnityEditor;
using System.Reflection;
using System.Collections;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        /// <summary>
        /// Returns the <see cref="Type"/> of the field that backs this <see cref="SerializedProperty"/>.
        /// For an array/list element property the element type is returned.
        /// </summary>
        /// <param name="serializedProperty">The property to inspect.</param>
        /// <returns>
        /// The <see cref="FieldInfo.FieldType"/> of the backing field
        /// (the element type when the property is an array/list element),
        /// or <see langword="null"/> if the field cannot be resolved.
        /// </returns>
        public static Type GetPropertyType(this SerializedProperty serializedProperty)
        {
            var type = serializedProperty.GetFieldInfo()?.FieldType;
            return IsArrayElement(serializedProperty) ? type?.GetCollectionElementTypeOrSelf() : type;
        }

        /// <summary>
        /// Resolves the <see cref="FieldInfo"/> that backs this <see cref="SerializedProperty"/>,
        /// looked up on the runtime type of the property's declaring instance (see <see cref="GetDeclaringInstance"/>).
        /// </summary>
        /// <remarks>
        /// Base classes are searched too. For a list/array element the collection field itself is returned
        /// (matching <c>PropertyDrawer.fieldInfo</c>); a <c>[SerializeReference]</c> segment resolves naturally
        /// through the live managed reference's runtime type.
        /// </remarks>
        /// <param name="property">The property whose backing field should be located.</param>
        /// <returns>The resolved <see cref="FieldInfo"/>, or <see langword="null"/> if it cannot be found.</returns>
        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            var owner = property.GetDeclaringInstance();
            return owner is null ? null : GetFieldIncludingBaseClasses(owner.GetType(), property.GetMemberName());
        }

        /// <summary>
        /// Traverses the <see cref="SerializedProperty.propertyPath"/> to return the runtime object on which the
        /// property's backing field is declared — the direct container, not the root <c>targetObject</c>.
        /// For an array/list element property the instance owning the collection field is returned.
        /// </summary>
        /// <remarks>
        /// When the declaring instance is a struct, the returned object is a boxed <b>copy</b> — mutating it does not
        /// affect the serialized object. Any resolution failure (missing field, a <see langword="null"/> value
        /// or an out-of-range element index along the path) returns <see langword="null"/>.
        /// </remarks>
        /// <param name="property">The property whose declaring instance should be resolved.</param>
        /// <returns>
        /// The instance declaring the property's backing field (the root <c>targetObject</c> for a top-level property),
        /// or <see langword="null"/> if the path cannot be resolved.
        /// </returns>
        /// <example>
        /// For <c>_inventory._slots.Array.data[2]._weapon</c> the returned instance is the slot element
        /// <c>_slots[2]</c> — the object whose class declares the <c>_weapon</c> field:
        /// <code>
        /// var slot = property.GetDeclaringInstance() as InventorySlot;
        /// </code>
        /// </example>
        public static object GetDeclaringInstance(this SerializedProperty property)
        {
            object current = property.serializedObject.targetObject;

            // The simplified path minus its last segment (the property itself).
            var path = property.SimplifyPropertyPath();
            var lastDotIndex = path.LastIndexOf('.');
            if (lastDotIndex < 0) return current;

            foreach (var part in path[..lastDotIndex].Split('.'))
            {
                if (current is null) return null;

                var bracket = part.IndexOf('[');
                var name = bracket < 0 ? part : part[..bracket];

                current = GetFieldIncludingBaseClasses(current.GetType(), name)?.GetValue(current);

                if (bracket >= 0 && current is IList list)
                {
                    // A stale path can point past the end of a shrunk list — a resolution failure, not an exception.
                    var index = int.Parse(part[(bracket + 1)..^1]);
                    current = index < list.Count ? list[index] : null;
                }
            }

            return current;
        }

        private static FieldInfo GetFieldIncludingBaseClasses(Type type, string name)
        {
            const BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            for (var current = type; current is not null; current = current.BaseType)
            {
                var field = current.GetField(name, bindingAttr);
                if (field is not null) return field;
            }

            return null;
        }
    }
}
