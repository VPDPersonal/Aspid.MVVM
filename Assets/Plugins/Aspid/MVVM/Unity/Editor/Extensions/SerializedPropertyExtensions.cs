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
            var type = serializedProperty.GetClassInfo().type;
            var member = type.GetMembersInfosIncludingBaseClasses(Flags)
                .FirstOrDefault(member => member.Name == serializedProperty.name);

            return member switch
            {
                FieldInfo field => field.FieldType,
                PropertyInfo property => property.PropertyType,
                _ => null
            };
        }
        
        public static (Type type, object classInstance) GetClassInfo(this SerializedProperty property)
        {
            var path = property.propertyPath;
            var target = property.serializedObject.targetObject;
            var startRemoveIndex = path.Length - property.name.Length - 1;
            
            if (startRemoveIndex < 0)
                return (target.GetType(), target);
            
            path = path.Remove(startRemoveIndex)
                .Replace(".Array.data[", "[");

            (Type type, object classInstance) current = (null, target);

            foreach (var part in path.Split('.'))
            {
                if (part.Contains("["))
                {
                    var startPartIndex = part.IndexOf("[", StringComparison.Ordinal);
                    var length = part.IndexOf("]", StringComparison.Ordinal) - startPartIndex;

                    var index = int.Parse(part.Substring(startPartIndex, length));
                    current = FindType(part[..startPartIndex], index);
                }
                else
                {
                    current = FindType(part);
                }
            }

            return current;

            (Type, object) FindType(string name, int index = -1)
            {
                var field = current.type is null
                    ? FindField(target.GetType(), name)
                    : FindField(current.type, name);

                if (index > -1)
                {
                    if (field.FieldType.IsArray)
                    {
                        var type = field.FieldType.GetElementType();
                        var classInstance = ((object[])field.GetValue(current.classInstance))[index];
                        return (type, classInstance);
                    }

                    if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var type = field.FieldType.GetGenericArguments()[0];
                        var classInstance = ((IReadOnlyList<object>)field.GetValue(current.classInstance))[index];
                        return (type, classInstance);
                    }
                }
                
                return (field.FieldType, field.GetValue(current.classInstance));
            }

            FieldInfo FindField(Type type, string name)
            {
                return type?.GetFieldInfosIncludingBaseClasses(Flags)
                    .FirstOrDefault(field => field.Name == name);
            }
        }
    }
}