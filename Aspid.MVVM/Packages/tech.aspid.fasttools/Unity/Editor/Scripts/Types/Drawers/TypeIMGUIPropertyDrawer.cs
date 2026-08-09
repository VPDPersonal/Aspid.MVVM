using System;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;
using Aspid.FastTools.SerializeReferences.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    internal static class TypeIMGUIPropertyDrawer
    {
        private const string FolderClosedIconPath = "d_Folder Icon";
        private const string FolderOpenedIconPath = "d_FolderOpened Icon";

        internal static void DrawOpenScriptButton(Rect rect, Type type)
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

        internal static float GetHeight(SerializedProperty property)
        {
            var height = EditorGUIUtility.singleLineHeight;
            if (SerializeReferenceRequiredGate.IsViolation(property))
                height += EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;

            return height;
        }

        internal static void Draw(
            Rect position,
            GUIContent label,
            SerializedProperty property,
            TypeAllow allow = TypeAllow.All,
            params Type[] types)
        {
            var rowRect = position;
            rowRect.height = EditorGUIUtility.singleLineHeight;

            var isArray = property.propertyPath.EndsWith("]");
            var openButtonSize = isArray ? rowRect.height - 2 : rowRect.height;

            var fieldRect = rowRect;
            if (!string.IsNullOrWhiteSpace(label.text))
            {
                EditorGUI.LabelField(rowRect, label);
                fieldRect.x += EditorGUIUtility.labelWidth;
                fieldRect.width -= EditorGUIUtility.labelWidth;
            }

            var dropdownRect = fieldRect;
            var currentType = GetType(property.stringValue);
            var hasValidType = currentType is not null;

            if (hasValidType)
                dropdownRect.width -= openButtonSize + 1f;

            var caption = TypeSelectorHelpers.GetTypeSelectorTitle(currentType, property.stringValue);
            if (EditorGUI.DropdownButton(dropdownRect, new GUIContent(caption), FocusType.Passive))
            {
                var persistent = property.Persistent();
                var current = property.stringValue ?? string.Empty;
                var screenPosition = GUIUtility.GUIToScreenPoint(new Vector2(dropdownRect.x, dropdownRect.y));
                var screenRect = new Rect(screenPosition.x, screenPosition.y, dropdownRect.width, dropdownRect.height);

                var filter = new TypeSelectorFilter
                {
                    Types = types,
                    Allow = allow,
                };

                TypeSelectorWindow.Show(
                    screenRect: screenRect,
                    filter: filter,
                    currentAqn: current,
                    onSelected: assemblyQualifiedName => persistent.SetStringAndApply(assemblyQualifiedName ?? string.Empty));
            }

            if (hasValidType)
            {
                var openButtonRect = new Rect(dropdownRect.xMax + 1f, rowRect.y, openButtonSize, openButtonSize);
                DrawOpenScriptButton(openButtonRect, currentType);
            }

            if (!SerializeReferenceRequiredGate.IsViolation(property)) return;

            const string message = "Required type is not set";
            var noticeRect = new Rect(position.x, rowRect.yMax + EditorGUIUtility.standardVerticalSpacing,
                position.width, EditorGUIUtility.singleLineHeight);

            SerializeReferenceIMGUIPropertyDrawer.DrawRequiredNotice(noticeRect, message,
                "This [TypeSelector] field is marked required but has no type.");
        }

        private static Type GetType(string assemblyQualifiedName) =>
            string.IsNullOrEmpty(assemblyQualifiedName) ? null : Type.GetType(assemblyQualifiedName, throwOnError: false);
    }
}
