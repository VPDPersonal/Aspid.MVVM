using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// Property drawer for <see cref="EnumValues{TValue}"/> and <see cref="EnumValues{TEnum,TValue}"/>.
    /// Renders a header with the enum-type picker (disabled for the typed variant — the enum is
    /// fixed at compile time), the entries list, and the default-value field, and exposes a
    /// context-menu action that fills in any missing enum members from the configured type.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumValues<>))]
    [CustomPropertyDrawer(typeof(EnumValues<,>))]
    internal sealed class EnumValuesPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EnumValuesIMGUIPropertyDrawer.Draw(position, label, property, IsTypedVariant());

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EnumValuesIMGUIPropertyDrawer.GetHeight(property);

        public override VisualElement CreatePropertyGUI(SerializedProperty property) =>
            EnumValuesUIToolkitPropertyDrawer.Draw(property, IsTypedVariant());

        /// <summary>
        /// Whether the drawn field is an <see cref="EnumValues{TEnum,TValue}"/> (directly, or as
        /// an array/list element) — the variant with the enum fixed at compile time — rather than
        /// the untyped <see cref="EnumValues{TValue}"/>.
        /// </summary>
        private bool IsTypedVariant()
        {
            var type = fieldInfo.FieldType;

            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments()[0];
            }

            return type is { IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(EnumValues<,>);
        }
    }
}
