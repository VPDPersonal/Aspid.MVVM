using System;
using UnityEditor;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Editors
{
    public static partial class SerializePropertyExtensions
    {
        private const BindingFlags BindingFlags =
            System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.NonPublic;
        
        public static Type GetPropertyType(this SerializedProperty serializedProperty) => GetMemberInfo(serializedProperty) switch
        {
            FieldInfo field => field.FieldType,
            PropertyInfo property => property.PropertyType,
            _ => null
        };

        public static MemberInfo GetMemberInfo(this SerializedProperty serializedProperty)
        {
            var type = serializedProperty.GetClassInstance().GetType();
            
            return type
                .GetMembersInfosIncludingBaseClasses(BindingFlags)
                .FirstOrDefault(member => member.Name == serializedProperty.name);
        }
        
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
                var currentType = current.GetType();
                var field = FindField(currentType, name);

                if (index > -1)
                {
                    if (field.FieldType.IsArray)
                    {
                        var type = field.FieldType.GetElementType();
                        var classInstance = ((object[])field.GetValue(current))[index];
                        return (type, classInstance);
                    }

                    if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var type = field.FieldType.GetGenericArguments()[0];
                        var classInstance = ((IReadOnlyList<object>)field.GetValue(current))[index];
                        return (type, classInstance);
                    }
                }
                
                return field?.GetValue(current);
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