using System;
using Aspid.MVVM;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    [CustomPropertyDrawer(typeof(TypeSelectorAttribute))]
    public sealed class TypeSelectorPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var button = new Button()
                .SetUnityTextAlign(TextAnchor.MiddleLeft)
                .SetText(GetCaption(property.stringValue))
                .SetTooltip(GetTooltip(property.stringValue))
                .SetMargin(top: 0, bottom: 0, left: 0, right: 0);

            button.clicked += () =>
            {
                var window = EditorWindow.focusedWindow;
                var worldBound = button.worldBound;
                
                var screenRect = window is not null
                    ? new Rect(window.position.x + worldBound.xMin, window.position.y + worldBound.yMax, worldBound.width, worldBound.height)
                    : new Rect(worldBound.xMin, worldBound.yMax, worldBound.width, worldBound.height);

                var current = property.stringValue ?? string.Empty;

                ViewModelPickerWindow.Show(screenRect, current, onSelected: (assemblyQualifiedName, caption) =>
                {
                    property.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    
                    button.SetText(GetCaption(property.stringValue));
                    button.SetTooltip(GetTooltip(property.stringValue));
                });
            };

            return button;
        }

        private static string GetTooltip(string assemblyQualifiedName)
        {
            // TODO Aspid.MVVM Unity – Add Tooltip for missing types
            var type = GetType(assemblyQualifiedName);
            return type is null ? string.Empty : type.FullName;
        }
        
        private static string GetCaption(string assemblyQualifiedName)
        {
            if (string.IsNullOrEmpty(assemblyQualifiedName)) return "<None>";
            
            var type = GetType(assemblyQualifiedName);
            return type is null ? "<Missing>" : type.Name;
        }

        private static Type GetType(string assemblyQualifiedName) =>
            Type.GetType(assemblyQualifiedName, false);
    }
}