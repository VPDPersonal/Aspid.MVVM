using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    [CustomPropertyDrawer(typeof(SerializableType))]
    [CustomPropertyDrawer(typeof(SerializableType<>))]
    internal sealed class SerializableTypePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = GetTypeFromFieldType();
            
            SerializableTypeDrawer.DrawIMGUI(
                position: position,
                property: GetProperty(property),
                label: label,
                types: new[] { type },
                allow: TypeAllow.All);
        }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var type = GetTypeFromFieldType();
            
            return SerializableTypeDrawer.DrawUIToolkit(
                property: GetProperty(property), 
                label: preferredLabel, 
                types: new[] { type },
                allow: TypeAllow.All);
        }

        private static SerializedProperty GetProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_assemblyQualifiedName");
        
        private Type GetTypeFromFieldType()
        {
            var type = GetGenericArgumentFromFieldType(out var isGeneric);
            return !isGeneric ? typeof(object) : type;
        }
        
        private Type GetGenericArgumentFromFieldType(out bool isGeneric)
        {
            var type = fieldInfo.FieldType;
            
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments()[0];
            }
            else if (type.IsArray)
            {
                type = type.GetElementType();
            }

            if (type is not { IsGenericType: true })
            {
                isGeneric = false;
                return null;
            }

            isGeneric = true;
            var types = type.GetGenericArguments();
            return types is { Length: 1 } ? types[0] : null;
        }
    }
}
