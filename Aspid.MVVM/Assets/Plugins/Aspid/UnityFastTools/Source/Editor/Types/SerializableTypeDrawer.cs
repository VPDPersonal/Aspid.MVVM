using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
                
            var caption = GetCaption(property.stringValue);
            if (EditorGUI.DropdownButton(position, new GUIContent(caption), FocusType.Passive))
            {
                var current = property.stringValue ?? string.Empty;
                var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(position.x, position.y));
                var screenRect = new Rect(screenPosition.x, screenPosition.y, position.width, position.height);
                
                TypeSelectorWindow.Show(type, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                });    
            }
        }
        
        internal static VisualElement DrawUIToolkit(SerializedProperty property, string label, Type type)
        {
            var typeSelector = new VisualElement()
                .SetFlexDirection(FlexDirection.Row);
            
            var button = new Button()
                .SetMargin(0)
                .SetFlexGrow(1)
                .SetUnityTextAlign(TextAnchor.MiddleLeft)
                .SetText(GetCaption(property.stringValue))
                .SetTooltip(GetTooltip(property.stringValue));

            button.clicked += () =>
            {
                var window = EditorWindow.focusedWindow;
                var worldBound = button.worldBound;
                var screenRect = new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMin, worldBound.width, worldBound.height);

                var current = property.stringValue ?? string.Empty;
                
                TypeSelectorWindow.Show(type, screenRect, current, onSelected: assemblyQualifiedName =>
                {
                    property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    
                    button.SetText(GetCaption(property.stringValue));
                    button.SetTooltip(GetTooltip(property.stringValue));
                });
            };

            if (!string.IsNullOrEmpty(label))
            {
                typeSelector.AddChild(new Label(label)
                    .SetUnityTextAlign(TextAnchor.MiddleLeft)
                    .SetMargin(right: 15));
            }

            return typeSelector.AddChild(button);
        }
        
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
            Type.GetType(assemblyQualifiedName, false);
    }
}