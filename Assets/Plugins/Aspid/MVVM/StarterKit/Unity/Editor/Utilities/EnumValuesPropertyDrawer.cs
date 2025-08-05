using UnityEditor;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [CustomPropertyDrawer(typeof(EnumValues<>))]
    public sealed class EnumValuesPropertyDrawer : PropertyDrawer
    {
        private static readonly float _newLine = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
       
        private EnumTypeDrawer _enumTypeDrawer;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valuesProperty = property.FindPropertyRelative("_values");
            var enumTypeProperty = property.FindPropertyRelative("_enumType");
            var defaultValueProperty = property.FindPropertyRelative("_defaultValue");
            var isDefaultValueProperty = property.FindPropertyRelative("_isDefaultValue");
            
            EditorGUI.BeginProperty(position, label, property);
            {
                var rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect, isDefaultValueProperty);

                if (isDefaultValueProperty.boolValue)
                {
                    rect.y += _newLine;
                    EditorGUI.PropertyField(rect, defaultValueProperty);
                }

                rect.y += _newLine;
                (_enumTypeDrawer ??= new EnumTypeDrawer()).Draw(rect, enumTypeProperty);
                
                rect.y += _newLine;
                EditorGUI.PropertyField(rect, valuesProperty, true);
                
                for (var i = 0; i < valuesProperty.arraySize; i++)
                {
                    var element = valuesProperty.GetArrayElementAtIndex(i);
                    var elementEnumTypeProperty = element.FindPropertyRelative("_enumType");
                    elementEnumTypeProperty.stringValue = enumTypeProperty.stringValue;
                }
            }
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var values = property.FindPropertyRelative("_values");
            var isDefaultValue = property.FindPropertyRelative("_isDefaultValue");
            
            var propertyCount = isDefaultValue.boolValue ? 3 : 2;
            return _newLine * propertyCount + EditorGUI.GetPropertyHeight(values, true);
        }
    }
}