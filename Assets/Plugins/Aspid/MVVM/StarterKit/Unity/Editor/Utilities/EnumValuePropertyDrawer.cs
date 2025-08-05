using System;
using UnityEditor;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [CustomPropertyDrawer(typeof(EnumValue<>))]
    public sealed class EnumValuePropertyDrawer : PropertyDrawer
    {
        private static readonly float _newLine = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var keyProperty = property.FindPropertyRelative("_key");
            var valueProperty = property.FindPropertyRelative("_value");
            var enumTypeProperty = property.FindPropertyRelative("_enumType");
            
            EditorGUI.BeginProperty(position, label, property);
            {
                var enumType = Type.GetType(enumTypeProperty.stringValue);
                var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                if (enumType is { IsEnum: true })
                {
                    keyProperty.stringValue = KeyPopup(rect, enumType, keyProperty.stringValue);
                }
                else
                {
                    EditorGUI.PropertyField(rect, keyProperty);
                }
                
                rect.y += _newLine;
                EditorGUI.PropertyField(rect, valueProperty);
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            _newLine * 2;

        private static string KeyPopup(Rect rect, Type enumType, string value)
        {
            var enumNames = Enum.GetNames(enumType);
            var selectedValueIndex = Mathf.Max(0, Array.IndexOf(enumNames, value));
                    
            selectedValueIndex = EditorGUI.Popup(rect, "Key", selectedValueIndex, enumNames);
            return enumNames[selectedValueIndex];
        }
    }
}