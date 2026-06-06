using System;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeIMGUIPropertyDrawer
    {
        private const string FolderClosedIconPath = "d_Folder Icon";
        private const string FolderOpenedIconPath = "d_FolderOpened Icon";
        
        public static void DrawOpenScriptButton(Rect rect, Type type)
        {
            var clicked = GUI.Button(rect, GUIContent.none);

            if (Event.current.type == EventType.Repaint)
            {
                var isHover = rect.Contains(Event.current.mousePosition);
                var icon = EditorGUIUtility.IconContent(isHover ? FolderOpenedIconPath : FolderClosedIconPath).image;
               
                GUI.DrawTexture(rect, icon, ScaleMode.ScaleToFit);
            }

            if (clicked) type.OpenInScriptEditor();
        }

        internal static void Draw(
            Rect position,
            GUIContent label,
            SerializedProperty property,
            TypeAllow allow = TypeAllow.All,
            params Type[] types)
        {
            var isArray = property.propertyPath.EndsWith("]");
            var openButtonSize = isArray ? position.height - 2 : position.height;

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
                dropdownRect.width -= openButtonSize + 1f;
              
            var caption = GetCaption(property.stringValue);
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(caption), FocusType.Passive))
            {
                var persistent = property.Persistent();
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
                        persistent.SetStringAndApply(assemblyQualifiedName ?? string.Empty);
                    });
            }

            if (!hasValidType) return;
            
            var openButtonRect = new Rect(dropdownRect.xMax + 1f, position.y, openButtonSize, openButtonSize);
            DrawOpenScriptButton(openButtonRect, currentType);
        }

        private static string GetCaption(string assemblyQualifiedName)
        {
            var type = GetType(assemblyQualifiedName);
            return TypeSelectorHelpers.GetTypeSelectorTitle(type, assemblyQualifiedName);
        }

        private static Type GetType(string assemblyQualifiedName) =>
            Type.GetType(assemblyQualifiedName, throwOnError: false);
    }
}
