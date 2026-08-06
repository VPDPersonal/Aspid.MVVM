using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// Property drawer for <see cref="EnumValue{TValue}"/>. Picks an EnumField/EnumFlagsField
    /// for the row's key based on the enum type configured on the parent
    /// <see cref="EnumValues{TValue}"/> / <see cref="EnumValues{TEnum,TValue}"/>, and falls back
    /// to a raw string field when the type can't be resolved.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumValue<>))]
    internal sealed class EnumValuePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EnumValueIMGUIPropertyDrawer.Draw(position, property);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EnumValueIMGUIPropertyDrawer.GetHeight(property);

        public override VisualElement CreatePropertyGUI(SerializedProperty property) =>
            EnumValueUIToolkitPropertyDrawer.Draw(property);
    }
}
