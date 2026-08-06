using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        private Type GetTypeFromFieldType() =>
            SerializableTypeUtility.TryGetBaseType(fieldInfo.FieldType, out var baseType) ? baseType : typeof(object);
    }
}
