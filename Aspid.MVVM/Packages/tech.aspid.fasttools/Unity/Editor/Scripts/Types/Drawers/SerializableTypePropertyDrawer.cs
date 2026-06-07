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
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) => TypeIMGUIPropertyDrawer.Draw(
            position: position,
            label: label,
            property: GetProperty(property),
            allow: TypeAllow.All,
            types: GetTypeFromFieldType());
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property) => TypeUIToolkitPropertyDrawer.Draw(
            label: preferredLabel, 
            property: GetProperty(property), 
            allow: TypeAllow.All,
            types: GetTypeFromFieldType());

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
