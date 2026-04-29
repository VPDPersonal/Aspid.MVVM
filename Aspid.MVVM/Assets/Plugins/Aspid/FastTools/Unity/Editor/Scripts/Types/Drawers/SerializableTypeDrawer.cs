using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class SerializableTypeDrawer
    {
        private const string OpenButtonIconPath = "Icons/open_button_icon";
        private const string StyleSheetPath = "Styles/Aspid-FastTools-SerializableType";
        private const string ButtonsClass = "aspid-fasttools-serializable-type-buttons";
        private const string OpenButtonClass = "aspid-fasttools-serializable-type-open-button";
        private const string OnlyButtonsClass = "aspid-fasttools-serializable-type-only-buttons";
        
        internal static void DrawIMGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label,
            Type[] types,
            TypeAllow allow)
        {
            var openButtonWidth = position.height;
            
            if (!string.IsNullOrWhiteSpace(label.text))
            {
                EditorGUI.LabelField(position, label);
                position.x += EditorGUIUtility.labelWidth;
                position.width -= EditorGUIUtility.labelWidth;
            }

            var dropdownRect = position;
            var currentType = GetType(property.stringValue);
            var hasValidType = currentType is not null;
            
            if (hasValidType)
                dropdownRect.width -= openButtonWidth + 2f;
                
            var caption = GetCaption(property.stringValue);
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(caption), FocusType.Passive))
            {
                var current = property.stringValue ?? string.Empty;
                var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(dropdownRect.x, dropdownRect.y));
                var screenRect = new Rect(screenPosition.x, screenPosition.y, dropdownRect.width, dropdownRect.height);
                
                TypeSelectorWindow.Show(
                    screenRect: screenRect,
                    types: types,
                    currentAqn: current,
                    allow: allow,
                    onSelected: assemblyQualifiedName =>
                    {
                        property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    });
            }

            if (!hasValidType) return;
            
            var openButtonRect = new Rect(dropdownRect.xMax + 2f, position.y, openButtonWidth, position.height);
            if (GUI.Button(openButtonRect, new GUIContent(Resources.Load<Texture2D>(OpenButtonIconPath))))
                OpenScript(currentType);
        }
        
        internal static VisualElement DrawUIToolkit(
            SerializedProperty property,
            string label,
            Type[] types,
            TypeAllow allow)
        {
            var typeSelector = new VisualElement()
                .AddClass(AspidStyles.Adapter)
                .AddClass(AspidStyles.AdapterMargin)
                .AddStyleSheetsFromResource(StyleSheetPath)
                .AddChild(new PropertyField(property)
                    .SetDisplay(DisplayStyle.None));

            var button = new Button()
                .SetText(GetCaption(property.stringValue))
                .SetTooltip(GetTooltip(property.stringValue));

            var propertyPath = property.propertyPath;
            var targets = property.serializedObject.targetObjects;

            var openButton = new Button()
                .SetDisplay(DisplayStyle.None)
                .AddClass(OpenButtonClass)
                .AddChild(new VisualElement())
                .AddClicked(() =>
                {
                    if (!TryReadValue(targets, propertyPath, out var currentValue)) return;

                    var currentType = GetType(currentValue);
                    if (currentType is not null)
                        OpenScript(currentType);
                });

            button.AddClicked(() =>
            {
                if (!TryReadValue(targets, propertyPath, out var current)) return;

                var window = EditorWindow.focusedWindow;
                if (window is null) return;

                var worldBound = button.worldBound;
                var screenRect = new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMin, worldBound.width, worldBound.height);

                TypeSelectorWindow.Show(
                    screenRect: screenRect,
                    types: types,
                    currentAqn: current,
                    allow: allow,
                    onSelected: assemblyQualifiedName =>
                    {
                        var value = assemblyQualifiedName ?? string.Empty;
                        if (!TryWriteValue(targets, propertyPath, value)) return;

                        button.SetText(GetCaption(value))
                            .SetTooltip(GetTooltip(value));

                        UpdateOpenButtonVisibility(value);
                    });
            });
            
            var buttons = new VisualElement()
                .AddChild(button)
                .AddChild(openButton)
                .AddClass(ButtonsClass);

            if (!string.IsNullOrEmpty(label))
                typeSelector.AddChild(new Label(label));
            else buttons.AddClass(OnlyButtonsClass);
            
            UpdateOpenButtonVisibility(property.stringValue);
            return typeSelector.AddChild(buttons);
            
            void UpdateOpenButtonVisibility(string assemblyQualifiedName)
            {
                var hasValidType = !string.IsNullOrWhiteSpace(assemblyQualifiedName) && GetType(assemblyQualifiedName) is not null;
                openButton.SetDisplay(hasValidType ? DisplayStyle.Flex : DisplayStyle.None);
            }
        }
        
        private static void OpenScript(Type type)
        {
            var (monoScript, lineNumber) = type.FindMonoScriptWithLine();
            
            if (monoScript is not null)
                AssetDatabase.OpenAsset(monoScript, lineNumber);
        }

        private static string GetTooltip(string assemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(assemblyQualifiedName))
                return string.Empty;

            var type = GetType(assemblyQualifiedName);
            return type is null ? $"Missing type: {assemblyQualifiedName}" : type.FullName;
        }
        
        private static string GetCaption(string assemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(assemblyQualifiedName)) 
                return Constants.NoneOption;
            
            var type = GetType(assemblyQualifiedName);
            return type is null ? Constants.MissingOption : type.Name;
        }
        
        private static Type GetType(string assemblyQualifiedName) =>
            Type.GetType(assemblyQualifiedName, throwOnError: false);

        private static bool TryReadValue(UnityEngine.Object[] targets, string propertyPath, out string value)
        {
            value = string.Empty;

            var alive = GetAliveTargets(targets);
            if (alive.Length == 0) return false;

            using var so = new SerializedObject(alive);
            var prop = so.FindProperty(propertyPath);
            if (prop is null) return false;

            value = prop.stringValue ?? string.Empty;
            return true;
        }

        private static bool TryWriteValue(UnityEngine.Object[] targets, string propertyPath, string value)
        {
            var alive = GetAliveTargets(targets);
            if (alive.Length == 0) return false;

            using var so = new SerializedObject(alive);
            var prop = so.FindProperty(propertyPath);
            if (prop is null) return false;

            prop.stringValue = value;
            so.ApplyModifiedProperties();
            return true;
        }

        private static UnityEngine.Object[] GetAliveTargets(UnityEngine.Object[] targets) =>
            targets is null ? Array.Empty<UnityEngine.Object>() : targets.Where(t => t != null).ToArray();
    }
}
