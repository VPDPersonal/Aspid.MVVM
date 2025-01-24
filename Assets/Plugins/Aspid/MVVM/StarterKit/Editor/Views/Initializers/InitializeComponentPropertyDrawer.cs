using UnityEditor;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Views
{
    [CustomPropertyDrawer(typeof(InitializeComponent<>), true)]
    public sealed class InitializeComponentPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                var properties = GetProperties(property);

                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, properties.resolve);

                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                EditorGUI.PropertyField(position, properties.component);
            }
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        {
            var properties = GetProperties(property);
            var height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            
            if (properties.component is not null)
                height += EditorGUI.GetPropertyHeight(properties.component, true);

            return height;
        }
        
        private static (SerializedProperty resolve, SerializedProperty component) GetProperties(SerializedProperty property)
        {
            (SerializedProperty resolve, SerializedProperty component) properties = default;
            
            properties.resolve = property.FindPropertyRelative("Resolve");
            
            switch ((InitializeComponent.Resolve)properties.resolve.enumValueIndex)
            {
                case InitializeComponent.Resolve.References:
                    properties.component = property.FindPropertyRelative("References");
                    break;

                case InitializeComponent.Resolve.ScriptableObject:
                    properties.component = property.FindPropertyRelative("Scriptable");
                    break;

#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    properties.component = property.FindPropertyRelative("Type");
                    break;
#endif
                
                case InitializeComponent.Resolve.Mono:
                default: 
                    properties.component = property.FindPropertyRelative("Mono");
                    break;
            }

            return properties;
        }
    }
}