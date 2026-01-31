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
        
        internal static void DrawIMGUI(Rect position, SerializedProperty property, GUIContent label, Type type)
        {
            if (!string.IsNullOrWhiteSpace(label.text))
            {
                EditorGUI.LabelField(position, label);
                position.x += EditorGUIUtility.labelWidth;
                position.width -= EditorGUIUtility.labelWidth;
            }

            var currentType = GetType(property.stringValue);
            var hasValidType = currentType != null;
            
            // Резервируем место для кнопки Open если тип валидный
            const float openButtonWidth = 50f;
            var dropdownRect = position;
            if (hasValidType)
                dropdownRect.width -= openButtonWidth + 2f;
                
            var caption = GetCaption(property.stringValue);
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(caption), FocusType.Passive))
            {
                var current = property.stringValue ?? string.Empty;
                var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(dropdownRect.x, dropdownRect.y));
                var screenRect = new Rect(screenPosition.x, screenPosition.y, dropdownRect.width, dropdownRect.height);
                
                TypeSelectorWindow.Show(type, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                });    
            }
            
            // Рисуем кнопку Open если тип валидный
            if (hasValidType)
            {
                var openButtonRect = new Rect(dropdownRect.xMax + 2f, position.y, openButtonWidth, position.height);
                if (GUI.Button(openButtonRect, "Open"))
                {
                    OpenScript(currentType);
                }
            }
        }
        
        internal static VisualElement DrawUIToolkit(SerializedProperty property, string label, Type type)
        {
            var typeSelector = new VisualElement()
                .SetFlexDirection(FlexDirection.Row)
                .AddChild(new PropertyField(property).SetDisplay(DisplayStyle.None));
            
            var button = new Button()
                .SetMargin(0)
                .SetFlexGrow(1)
                .SetUnityTextAlign(TextAnchor.MiddleLeft)
                .SetText(GetCaption(property.stringValue))
                .SetTooltip(GetTooltip(property.stringValue));

            var propertyPath = property.propertyPath;
            var serializedObject = property.serializedObject;
            
            var openButton = new Button()
            {
                text = "Open"
            };
            openButton.SetMargin(left: 4);
            openButton.SetDisplay(DisplayStyle.None);

            button.clicked += () =>
            {
                
                var window = EditorWindow.focusedWindow;
                var worldBound = button.worldBound;
                var screenRect = new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMin, worldBound.width, worldBound.height);

                var current = GetProperty(serializedObject, propertyPath).stringValue ?? string.Empty;
                
                TypeSelectorWindow.Show(type, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    var currentProperty = GetProperty(serializedObject, propertyPath);
                    currentProperty.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    
                    button.SetText(GetCaption(currentProperty.stringValue));
                    button.SetTooltip(GetTooltip(currentProperty.stringValue));
                    
                    UpdateOpenButtonVisibility(openButton, currentProperty.stringValue);
                });
            };
            
            openButton.clicked += () =>
            {
                var currentProperty = GetProperty(serializedObject, propertyPath);
                var currentType = GetType(currentProperty.stringValue);
                if (currentType != null)
                    OpenScript(currentType);
            };

            if (!string.IsNullOrEmpty(label))
            {
                typeSelector.AddChild(new Label(label)
                    .SetUnityTextAlign(TextAnchor.MiddleLeft)
                    .SetMargin(right: 15));
            }
            
            typeSelector.AddChild(button);
            typeSelector.AddChild(openButton);
            
            // Обновляем видимость кнопки Open сразу
            UpdateOpenButtonVisibility(openButton, property.stringValue);

            return typeSelector;
        }
        
        private static void UpdateOpenButtonVisibility(Button openButton, string assemblyQualifiedName)
        {
            var hasValidType = !string.IsNullOrWhiteSpace(assemblyQualifiedName) && GetType(assemblyQualifiedName) != null;
            openButton.SetDisplay(hasValidType ? DisplayStyle.Flex : DisplayStyle.None);
        }
        
        private static void OpenScript(Type type)
        {
            var (monoScript, lineNumber) = GetMonoScriptFromType(type);
            if (monoScript != null)
                AssetDatabase.OpenAsset(monoScript, lineNumber);
        }
        
        private static (MonoScript script, int lineNumber) GetMonoScriptFromType(Type type)
        {
            var scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
            var typeName = type.Name;
            var typeNamespace = type.Namespace;
            var isEnum = type.IsEnum;
            
            // Regex для точного поиска объявления типа (с границами слова)
            var pattern = isEnum 
                ? $@"\benum\s+{Regex.Escape(typeName)}\b"
                : $@"\b(class|struct|record)\s+{Regex.Escape(typeName)}\b";
            var regex = new Regex(pattern);
            
            // Сначала пробуем найти по GetClass() - это работает когда имя класса совпадает с именем файла
            foreach (var script in scripts)
            {
                if (script.GetClass() == type)
                {
                    var line = FindTypeLineNumber(script.text, typeName, isEnum);
                    return (script, line);
                }
            }
            
            // Если не нашли, ищем по имени типа в тексте скрипта
            foreach (var script in scripts)
            {
                var text = script.text;
                if (string.IsNullOrEmpty(text)) continue;
                
                // Проверяем namespace если есть
                if (!string.IsNullOrEmpty(typeNamespace) && !text.Contains($"namespace {typeNamespace}"))
                    continue;
                
                // Ищем объявление типа с помощью regex
                if (regex.IsMatch(text))
                {
                    var line = FindTypeLineNumber(text, typeName, isEnum);
                    return (script, line);
                }
            }
            
            return (null, 1);
        }
        
        private static int FindTypeLineNumber(string text, string typeName, bool isEnum)
        {
            if (string.IsNullOrEmpty(text)) return 1;
            
            // Regex для точного поиска объявления типа
            var pattern = isEnum 
                ? $@"\enum\s+{Regex.Escape(typeName)}\b"
                : $@"\b(class|struct|record)\s+{Regex.Escape(typeName)}\b";
            var regex = new Regex(pattern);
            
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
        
        private static Type GetType(string assemblyQualifiedName) =>
            Type.GetType(assemblyQualifiedName, throwOnError: false);
    }
}