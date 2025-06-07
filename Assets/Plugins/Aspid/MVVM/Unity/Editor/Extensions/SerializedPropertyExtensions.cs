using System;
using System.Linq;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace Aspid.MVVM.Unity
{
    public static class SerializedPropertyExtensions
    {
        private const BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        public static Type GetPropertyType(this SerializedProperty serializedProperty)
        {
            var type = serializedProperty.GetClassType();
            var member= type.GetMembersInfosIncludingBaseClasses(Flags)
                .FirstOrDefault(member => member.Name == serializedProperty.name);

            return member switch
            {
                FieldInfo field => field.FieldType,
                PropertyInfo property => property.PropertyType,
                _ => null
            };
        }
        
        public static Type GetClassType(this SerializedProperty property)
        {
            var path = property.propertyPath;
            var startRemoveIndex = path.Length - property.name.Length - 1;
            
            if (startRemoveIndex < 0)
                return property.serializedObject.targetObject.GetType();
            
            path = path.Remove(startRemoveIndex)
                .Replace(".Array.data[", "[");

            Type currentType = null;

            foreach (var part in path.Split('.'))
            {
                currentType = part.Contains("[")
                    ? FindType(part[..part.IndexOf("[", StringComparison.Ordinal)], true)
                    : FindType(part);
            }

            return currentType;

            Type FindType(string name, bool isArray = false)
            {
                var field = currentType is null
                    ? FindField(property.serializedObject.targetObject.GetType(), name)
                    : FindField(currentType, name);

                if (isArray)
                {
                    if (field.FieldType.IsArray) 
                        return field.FieldType.GetElementType();

                    if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                        return field.FieldType.GetGenericArguments()[0];
                }

                return field.FieldType;
            }

            FieldInfo FindField(Type type, string name)
            {
                return type?.GetFieldInfosIncludingBaseClasses(Flags)
                    .FirstOrDefault(field => field.Name == name);
            }
        }
    }
}