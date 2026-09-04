using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="PropertyDrawer"/> that draws a <see cref="ConverterFallback{T}"/> as the two fields
    /// it replaced.
    /// </summary>
    /// <remarks>
    /// A struct renders as a foldout by default, which would bury a converter's failure handling one
    /// click deeper than it sat before. The field's own label and tooltip stay on the value, since that
    /// is what the converter names it.
    /// </remarks>
    [CustomPropertyDrawer(typeof(ConverterFallback<>), useForChildren: true)]
    public sealed class ConverterFallbackDrawer : PropertyDrawer
    {
        private const string ModeLabel = "On Failure";

        // Both members are auto-properties, so Unity serializes their compiler-generated backing field.
        private const string ModePath = "<Mode>k__BackingField";
        private const string ValuePath = "<FallbackValue>k__BackingField";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = property.FindPropertyRelative(ValuePath);
            var mode = property.FindPropertyRelative(ModePath);

            EditorGUI.BeginProperty(position, label, property);
            {
                position.height = EditorGUI.GetPropertyHeight(value, includeChildren: true);
                EditorGUI.PropertyField(position, value, label, includeChildren: true);

                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
                position.height = EditorGUI.GetPropertyHeight(mode, includeChildren: false);
                EditorGUI.PropertyField(position, mode, new GUIContent(ModeLabel, mode.tooltip));
            }
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative(ValuePath), includeChildren: true) +
            EditorGUIUtility.standardVerticalSpacing +
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative(ModePath), includeChildren: false);
    }
}
