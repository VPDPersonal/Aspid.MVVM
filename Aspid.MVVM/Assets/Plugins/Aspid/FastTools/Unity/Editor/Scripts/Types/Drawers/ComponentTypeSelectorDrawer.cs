using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class ComponentTypeSelectorDrawer
    {
        private const string StyleSheetPath = "Styles/Aspid-FastTools-ComponentTypeSelector";

        internal static void DrawIMGUI(Rect position, SerializedProperty property, Type declaringType)
        {
            var currentType = property.serializedObject.targetObject.GetType();
            var buttonRow = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            if (GUI.Button(buttonRow, new GUIContent(currentType.Name), EditorStyles.popup))
            {
                TypeSelectorWindow.Show(
                    GUIUtility.GUIToScreenRect(buttonRow),
                    types: new[] { declaringType },
                    currentType.AssemblyQualifiedName,
                    onSelected: aqn => OnTypeSelected(property, aqn, currentType));
            }
        }

        internal static VisualElement DrawUIToolkit(SerializedProperty property, Type declaringType)
        {
            var currentType = property.serializedObject.targetObject.GetType();

            var button = new Button()
                .SetText(currentType.Name)
                .AddStyleSheetsFromResource(StyleSheetPath);

            button.AddClicked(() =>
            {
                var window = EditorWindow.focusedWindow;
                var worldBound = button.worldBound;
                var screenRect = new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMin, worldBound.width, worldBound.height);
                var currentT = property.serializedObject.targetObject.GetType();

                TypeSelectorWindow.Show(
                    screenRect: screenRect,
                    types: new[] { declaringType },
                    currentAqn: currentT.AssemblyQualifiedName,
                    onSelected: aqn => OnTypeSelected(property, aqn, currentT));
            });

            return button;
        }

        private static void OnTypeSelected(SerializedProperty property, string aqn, Type currentType)
        {
            var newType = Type.GetType(aqn);
            if (newType is null || newType == currentType) return;

            var script = newType.FindMonoScript();

            if (script is null)
            {
                Debug.LogWarning($"[SubclassDropdown] MonoScript not found for type: {aqn}");
                return;
            }

            EditorApplication.delayCall += () =>
                property.serializedObject.FindProperty("m_Script").SetObjectReferenceAndApply(script);
        }
    }
}
