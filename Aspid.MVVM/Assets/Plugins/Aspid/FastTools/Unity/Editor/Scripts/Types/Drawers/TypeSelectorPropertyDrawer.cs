using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    internal sealed class TypeSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ThrowExceptionIfInvalidProperty(property);
            var allow = GetTypeAllow();
            var types = GetTypesFromAttribute(property);

            SerializableTypeDrawer.DrawIMGUI(
                position: position,
                property: property,
                label: label,
                types: types,
                allow: allow);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            ThrowExceptionIfInvalidProperty(property);
            var allow = GetTypeAllow();
            var types = GetTypesFromAttribute(property);

            return SerializableTypeDrawer.DrawUIToolkit(
                property: property,
                label: preferredLabel,
                types: types,
                allow: allow);
        }

        private TypeAllow GetTypeAllow()
        {
            var allow = TypeAllow.None;
            var typeSelectorAttribute = (TypeSelectorAttribute)attribute;
            
            if (typeSelectorAttribute.AllowAbstractTypes) 
                allow |= TypeAllow.Abstract;
            
            if (typeSelectorAttribute.AllowInterfaces)
                allow |= TypeAllow.Interface;
            
            return allow;
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
            const BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
            var currentType = type;
            while (currentType is not null)
            {
                var members = currentType.GetMember(memberName, bindingAttr);
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
        
        private static void ThrowExceptionIfInvalidProperty(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.String)
                throw new ArgumentException("Property must be of type String", nameof(property));
        }
    }
}
