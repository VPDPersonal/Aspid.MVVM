using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    internal sealed class TypeSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = GetTypeFromAttribute();
            SerializableTypeDrawer.DrawIMGUI(position, property, label, type);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var type = GetTypeFromAttribute();
            return SerializableTypeDrawer.DrawUIToolkit(property, preferredLabel, type);
        }

        private Type GetTypeFromAttribute() =>
            ((TypeSelectorAttribute)attribute).Type;
    }
}