using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    [CustomPropertyDrawer(typeof(ComponentTypeSelector))]
    internal sealed class ComponentTypeSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var currentType = property.serializedObject.targetObject.GetType();
            var rowHeight = EditorGUIUtility.singleLineHeight;
            var openButtonWidth = rowHeight;

            var dropdownRect = new Rect(position.x, position.y, position.width - openButtonWidth - 2f, rowHeight);
            var openButtonRect = new Rect(dropdownRect.xMax + 2f, position.y, openButtonWidth, rowHeight);

            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(currentType.Name), FocusType.Passive))
            {
                TypeSelectorWindow.Show(
                    GUIUtility.GUIToScreenRect(dropdownRect),
                    types: new[] { fieldInfo.DeclaringType },
                    currentType.AssemblyQualifiedName,
                    onSelected: aqn => ReplaceComponentScript(property, currentType, Type.GetType(aqn)));
            }

            TypeIMGUIPropertyDrawer.DrawOpenScriptButton(openButtonRect, currentType);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var currentType = property.serializedObject.targetObject.GetType();

            var field = new InspectorTypeField(label: null, defaultValue: currentType)
            {
                Types = new[] { fieldInfo.DeclaringType },
            };

            field.RegisterValueChangedCallback(evt =>
                ReplaceComponentScript(property, currentType, evt.newValue));

            return field;
        }

        private static void ReplaceComponentScript(SerializedProperty property, Type oldType, Type newType)
        {
            if (newType is null || newType == oldType) return;

            var script = newType.FindMonoScript();

            if (script is null)
            {
                Debug.LogWarning($"[ComponentTypeSelector] MonoScript not found for type: {newType.AssemblyQualifiedName}");
                return;
            }

            EditorApplication.delayCall += () =>
                property.serializedObject.FindProperty("m_Script").SetObjectReferenceAndApply(script);
        }
    }
}
