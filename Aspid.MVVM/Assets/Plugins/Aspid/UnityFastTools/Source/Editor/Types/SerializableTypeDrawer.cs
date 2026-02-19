using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    internal static class SerializableTypeDrawer
    {
        private const string NoneOption = "<None>";
        private const string MissingOption = "<Missing>";
        
        internal static void DrawIMGUI(Rect position, SerializedProperty property, GUIContent label, params Type[] types)
        {
            const float openButtonWidth = 50f;
            
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
                
                TypeSelectorWindow.Show(types, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                });    
            }

            if (!hasValidType) return;
            
            var openButtonRect = new Rect(dropdownRect.xMax + 2f, position.y, openButtonWidth, position.height);
            if (GUI.Button(openButtonRect, text: "Open"))
                OpenScript(currentType);
        }
        
        internal static VisualElement DrawUIToolkit(SerializedProperty property, string label, params Type[] types)
        {
            var typeSelector = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(new PropertyField(property).SetDisplay(DisplayStyle.None));
            
            var button = new Button()
                .SetMargin(0)
                .SetFlexGrow(1)
                .SetFlexShrink(1)
                .SetOverflow(Overflow.Hidden)
                .SetWhiteSpace(WhiteSpace.NoWrap)
                .SetUnityTextAlign(TextAnchor.MiddleLeft)
                .SetText(GetCaption(property.stringValue))
                .SetTextOverflow(TextOverflow.Ellipsis)
                .SetTooltip(GetTooltip(property.stringValue));

            var propertyPath = property.propertyPath;
            var serializedObject = property.serializedObject;

            var openButton = new Button()
                .SetText("Open")
                .SetMargin(left: 4)
                .SetDisplay(DisplayStyle.None);

            button.clicked += () =>
            {
                var window = EditorWindow.focusedWindow;
                var worldBound = button.worldBound;
                var screenRect = new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMin, worldBound.width, worldBound.height);

                var current = GetProperty(serializedObject, propertyPath).stringValue ?? string.Empty;
                
                TypeSelectorWindow.Show(types, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    var currentProperty = GetProperty(serializedObject, propertyPath);
                    currentProperty.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    
                    button
                        .SetText(GetCaption(currentProperty.stringValue))
                        .SetTooltip(GetTooltip(currentProperty.stringValue));
                    
                    UpdateOpenButtonVisibility(openButton, currentProperty.stringValue);
                });
            };
            
            openButton.clicked += () =>
            {
                var currentProperty = GetProperty(serializedObject, propertyPath);
                var currentType = GetType(currentProperty.stringValue);
                
                if (currentType is not null)
                    OpenScript(currentType);
            };

            if (!string.IsNullOrEmpty(label))
            {
                typeSelector
                    .AddChild(new Label(label)
                    .SetUnityTextAlign(TextAnchor.MiddleLeft)
                    .SetMargin(right: 15));
            }
            
            typeSelector
                .AddChild(button)
                .AddChild(openButton);
            
            UpdateOpenButtonVisibility(openButton, property.stringValue);
            return typeSelector;
        }
        
        private static void UpdateOpenButtonVisibility(Button openButton, string assemblyQualifiedName)
        {
            var hasValidType = !string.IsNullOrWhiteSpace(assemblyQualifiedName) && GetType(assemblyQualifiedName) is not null;
            openButton.SetDisplay(hasValidType ? DisplayStyle.Flex : DisplayStyle.None);
        }
        
        private static void OpenScript(Type type)
        {
            var (monoScript, lineNumber) = GetMonoScriptFromType(type);
            
            if (monoScript is not null)
                AssetDatabase.OpenAsset(monoScript, lineNumber);
        }
        
        private static (MonoScript script, int lineNumber) GetMonoScriptFromType(Type type)
        {
            var isEnum = type.IsEnum;
            var typeName = type.Name;
            var typeNamespace = type.Namespace;
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();

            var regex = new Regex(GetPattern(isEnum, typeName));
            
            foreach (var script in scripts)
            {
                if (script.GetClass() != type) continue;
                
                var line = FindTypeLineNumber(script.text, typeName, isEnum);
                return (script, line);
            }
            
            foreach (var script in scripts)
            {
                var text = script.text;
                if (string.IsNullOrWhiteSpace(text)) continue;
                if (!string.IsNullOrWhiteSpace(typeNamespace) && !text.Contains($"namespace {typeNamespace}")) continue;
                if (!regex.IsMatch(text)) continue;

                var line = FindTypeLineNumber(text, typeName, isEnum);
                return (script, line);
            }
            
            return (script: null, lineNumber: 1);
        }
        
        private static int FindTypeLineNumber(string text, string typeName, bool isEnum)
        {
            if (string.IsNullOrEmpty(text)) return 1;
            
            var regex = new Regex(GetPattern(isEnum, typeName));
            var lines = text.Split('\n');
            
            for (var i = 0; i < lines.Length; i++)
            {
                if (regex.IsMatch(lines[i]))
                    return i + 1;
            }
            
            return 1;
        }

        private static SerializedProperty GetProperty(SerializedObject serializedObject, string propertyPath) =>
            serializedObject.FindProperty(propertyPath);
        
        private static string GetTooltip(string assemblyQualifiedName)
        {
            // TODO Aspid.MVVM Unity – Add Tooltip for missing types
            var type = GetType(assemblyQualifiedName);
            return type is null ? string.Empty : type.FullName;
        }
        
        private static string GetCaption(string assemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(assemblyQualifiedName)) return NoneOption;
            
            var type = GetType(assemblyQualifiedName);
            return type is null ? MissingOption : type.Name;
        }
        
        private static string GetPattern(bool isEnum, string typeName) => isEnum 
            ? $@"\benum\s+{Regex.Escape(typeName)}\b"
            : $@"\b(class|struct|record)\s+{Regex.Escape(typeName)}\b";
        
        private static Type GetType(string assemblyQualifiedName) =>
            Type.GetType(assemblyQualifiedName, throwOnError: false);
    }
}