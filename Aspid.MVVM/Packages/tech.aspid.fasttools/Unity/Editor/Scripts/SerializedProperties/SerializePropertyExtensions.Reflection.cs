using System;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Aspid.FastTools.Reflection;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;

        /// <summary>
        /// Returns the <see cref="Type"/> of the field or property that backs this <see cref="SerializedProperty"/>.
        /// </summary>
        /// <param name="serializedProperty">The property to inspect.</param>
        /// <returns>
        /// The <see cref="FieldInfo.FieldType"/> or <see cref="PropertyInfo.PropertyType"/> of the backing member,
        /// or <see langword="null"/> if the member cannot be resolved.
        /// </returns>
        public static Type GetPropertyType(this SerializedProperty serializedProperty) => GetMemberInfo(serializedProperty) switch
        {
            FieldInfo field => field.FieldType,
            PropertyInfo property => property.PropertyType,
            _ => null
        };

        /// <summary>
        /// Uses reflection to find the <see cref="MemberInfo"/> (field or property) on the owning class
        /// that corresponds to this <see cref="SerializedProperty"/>.
        /// </summary>
        /// <param name="serializedProperty">The property whose backing member should be located.</param>
        /// <returns>
        /// The <see cref="MemberInfo"/> whose name matches <see cref="SerializedProperty.name"/>,
        /// or <see langword="null"/> if it cannot be found.
        /// </returns>
        public static MemberInfo GetMemberInfo(this SerializedProperty serializedProperty)
        {
            var instance = serializedProperty.GetClassInstance();
            if (instance is null) return null;

            return instance.GetType()
                .GetMembersInfosIncludingBaseClasses(BindingFlags)
                .FirstOrDefault(member => member.Name == serializedProperty.name);
        }

        /// <summary>
        /// Traverses the <see cref="SerializedProperty.propertyPath"/> to return the runtime object instance
        /// that directly owns this property (i.e., the containing class instance, not the root target).
        /// Supports nested objects, arrays, and generic <see cref="List{T}"/> fields.
        /// </summary>
        /// <param name="property">The property whose owning instance should be resolved.</param>
        /// <returns>The runtime object that contains the field represented by <paramref name="property"/>.</returns>
        public static object GetClassInstance(this SerializedProperty property)
        {
            var path = property.propertyPath;
            var target = property.serializedObject.targetObject;
            var startRemoveIndex = path.Length - property.name.Length - 1;

            if (startRemoveIndex < 0) return target;
            
            path = path
                .Remove(startRemoveIndex)
                .Replace(".Array.data[", "[");

            object current = target;

            foreach (var part in path.Split('.'))
            {
                if (part.Contains("["))
                {
                    var startPartIndex = part.IndexOf("[", StringComparison.Ordinal) + 1;
                    var length = part.IndexOf("]", StringComparison.Ordinal) - startPartIndex;

                    var index = int.Parse(part.Substring(startPartIndex, length));
                    current = FindInstance(part[..(startPartIndex - 1)], index);
                }
                else
                {
                    current = FindInstance(part);
                }
            }

            return current;

            object FindInstance(string name, int index = -1)
            {
                if (current is null) return null;

                var field = FindField(current.GetType(), name);
                if (field is null) return null;

                var value = field.GetValue(current);
                return index > -1 && value is IList list
                    ? list[index]
                    : value;
            }

            FieldInfo FindField(Type type, string name)
            {
                return type?.GetMembersInfosIncludingBaseClasses(BindingFlags)
                    .OfType<FieldInfo>()
                    .FirstOrDefault(field => field.Name == name);
            }
        }
    }
}
