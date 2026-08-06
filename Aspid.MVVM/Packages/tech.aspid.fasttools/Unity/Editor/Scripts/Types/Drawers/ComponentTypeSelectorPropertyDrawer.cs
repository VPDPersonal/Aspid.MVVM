using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
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

            var dropdownRect = new Rect(position.x, position.y, position.width - rowHeight - 2f, rowHeight);
            var openButtonRect = new Rect(dropdownRect.xMax + 2f, position.y, rowHeight, rowHeight);

            if (EditorGUI.DropdownButton(dropdownRect,
                    new GUIContent(TypeSelectorHelpers.GetTypeSelectorTitle(currentType)), FocusType.Passive))
            {
                var persistent = property.Persistent();
                var filter = new TypeSelectorFilter
                {
                    Types = new[] { fieldInfo.DeclaringType },
                };

                TypeSelectorWindow.Show(
                    GUIUtility.GUIToScreenRect(dropdownRect),
                    filter,
                    currentType.AssemblyQualifiedName,
                    onSelected: aqn => ReplaceComponentScript(
                        persistent,
                        currentType,
                        string.IsNullOrEmpty(aqn) ? null : Type.GetType(aqn, throwOnError: false)));
            }

            TypeIMGUIPropertyDrawer.DrawOpenScriptButton(openButtonRect, currentType);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUIUtility.singleLineHeight;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var currentType = property.serializedObject.targetObject.GetType();
            var persistent = property.Persistent();

            var field = new InspectorTypeField(label: null, defaultValue: currentType)
            {
                Types = new[] { fieldInfo.DeclaringType },
            };

            field.RegisterValueChangedCallback(evt =>
                ReplaceComponentScript(persistent, currentType, evt.newValue));
            field.RegisterCallback<AttachToPanelEvent>(_ => HideScriptField(field));

            return field;
        }

        private static void HideScriptField(VisualElement field)
        {
            var inspector = field.GetFirstAncestorOfType<InspectorElement>();
            if (inspector is null) return;

            inspector.Query<PropertyField>()
                .Where(propertyField => propertyField.bindingPath == "m_Script")
                .ForEach(propertyField => propertyField.style.display = DisplayStyle.None);
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
