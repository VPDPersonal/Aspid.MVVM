using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    internal sealed class TypeSelectorPropertyDrawer : PropertyDrawer
    {
        private const BindingFlags BindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = GetTypesFromAttribute(property);
            SerializableTypeDrawer.DrawIMGUI(position, property, label, type);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var type = GetTypesFromAttribute(property);
            return SerializableTypeDrawer.DrawUIToolkit(property, preferredLabel, type);
        }

        private Type[] GetTypesFromAttribute(SerializedProperty property)
        {
            var typeSelectorAttribute = (TypeSelectorAttribute)attribute;
            
            var assemblyQualifiedNames = typeSelectorAttribute.AssemblyQualifiedNames
                .Where(assemblyQualifiedName => !string.IsNullOrWhiteSpace(assemblyQualifiedName))
                .ToArray();
            
            if (assemblyQualifiedNames.Length is 0)
                return Array.Empty<Type>();
            
            var targetObject = property.serializedObject.targetObject;
            var targetType = targetObject.GetType();
            var types = new List<Type>();

            foreach (var name in assemblyQualifiedNames)
            {
                var member = GetMemberFromHierarchy(targetType, name);
                
                if (member is not null)
                {
                    AddTypesFromMember(types, member, targetObject);
                }
                else
                {
                    var type = Type.GetType(name, throwOnError: false);
                    
                    if (type is not null)
                        types.Add(type);
                }
            }

            return types.ToArray();
        }

        private static MemberInfo GetMemberFromHierarchy(Type type, string memberName)
        {
            var currentType = type;
            while (currentType is not null)
            {
                var members = currentType.GetMember(memberName, BindingAttr);
                if (members.Length > 0)
                    return members[0];
                
                currentType = currentType.BaseType;
            }
            return null;
        }

        private static void AddTypesFromMember(List<Type> types, MemberInfo member, object targetObject)
        {
            var value = member switch
            {
                FieldInfo fieldInfo => fieldInfo.GetValue(targetObject),
                PropertyInfo propertyInfo => propertyInfo.GetValue(targetObject),
                _ => null
            };

            switch (value)
            {
                case null: return;
                
                case Type type:
                    types.Add(type);
                    return;
                
                case Type[] typeArray:
                    types.AddRange(typeArray);
                    return;
                
                case string assemblyQualifiedName:
                {
                    var type = Type.GetType(assemblyQualifiedName, throwOnError: false);
                    if (type is not null)
                        types.Add(type);
                    return;
                }
                
                case string[] assemblyQualifiedNames:
                {
                    foreach (var assemblyQualifiedName in assemblyQualifiedNames)
                    {
                        if (string.IsNullOrWhiteSpace(assemblyQualifiedName)) continue;
                        
                        var type = Type.GetType(assemblyQualifiedName, throwOnError: false);
                        if (type is not null)
                            types.Add(type);
                    }
                    return;
                }
            }
        }
    }
}