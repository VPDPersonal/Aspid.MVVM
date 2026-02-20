using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [CustomPropertyDrawer(typeof(SerializableType))]
    [CustomPropertyDrawer(typeof(SerializableType<>))]
    internal sealed class SerializableTypePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = GetTypeFromFieldType();
            SerializableTypeDrawer.DrawIMGUI(position, GetProperty(property), label, type);
        }
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var type = GetTypeFromFieldType();
            return SerializableTypeDrawer.DrawUIToolkit(GetProperty(property), preferredLabel, type);
        }

        private static SerializedProperty GetProperty(SerializedProperty property) =>
            property.FindPropertyRelative("_assemblyQualifiedName");
        
        private Type GetTypeFromFieldType()
        {
            var type = SerializableTypeUtility.GetGenericArgumentFromFieldType(fieldInfo, out var isGeneric);
            if (!isGeneric) type = typeof(object);

            return type;
        }
    }
}