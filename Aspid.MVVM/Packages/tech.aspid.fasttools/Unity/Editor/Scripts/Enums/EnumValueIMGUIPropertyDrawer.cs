using System;
using UnityEditor;
using UnityEngine;
using Aspid.FastTools.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// IMGUI rendering for <see cref="EnumValuePropertyDrawer"/>.
    /// </summary>
    internal static class EnumValueIMGUIPropertyDrawer
    {
        private const float FieldSpacing = 4f;
        internal const float FoldoutArrowWidth = 13f;

        private static readonly GUIContent _valueLabel = new("Value");

        public static float GetHeight(SerializedProperty property)
        {
            var valueProperty = property.FindPropertyRelative("_value");

            if (!valueProperty.HasFoldout())
                return EditorGUIUtility.singleLineHeight;

            return EditorGUIUtility.singleLineHeight
                + EditorGUIUtility.standardVerticalSpacing
                + EditorGUI.GetPropertyHeight(valueProperty, includeChildren: true);
        }

        public static void Draw(Rect position, SerializedProperty property)
        {
            var keyProperty = property.FindPropertyRelative("_key");
            var valueProperty = property.FindPropertyRelative("_value");
            var enumTypeProperty = property.FindPropertyRelative("_enumType");

            var hasFoldout = valueProperty.HasFoldout();

            Rect keyRect;
            Rect valueRect;
            GUIContent label;

            if (hasFoldout)
            {
                label = _valueLabel;

                keyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

                var valueY = keyRect.yMax + EditorGUIUtility.standardVerticalSpacing;
                valueRect = new Rect(
                    position.x + FoldoutArrowWidth,
                    valueY,
                    position.width - FoldoutArrowWidth,
                    position.yMax - valueY);
            }
            else
            {
                label = GUIContent.none;

                var halfWidth = (position.width - FieldSpacing) / 2f;
                keyRect = new Rect(position.x, position.y, halfWidth, position.height);

                valueRect = new Rect(
                    keyRect.xMax + FieldSpacing,
                    position.y,
                    halfWidth,
                    position.height);
            }

            DrawKey(keyRect, keyProperty, enumTypeProperty);

            // includeChildren must match the foldout case: for a complex TValue the height reserved
            // in GetHeight covers the expanded children, so they have to be drawn here too — the
            // 3-arg PropertyField overload defaults includeChildren to false and would leave the
            // reserved space blank under the "Value" foldout.
            EditorGUI.PropertyField(valueRect, valueProperty, label, includeChildren: hasFoldout);
        }

        private static void DrawKey(Rect rect, SerializedProperty keyProperty, SerializedProperty enumTypeProperty)
        {
            var enumType = Type.GetType(enumTypeProperty.stringValue, throwOnError: false);

            // A resolvable non-enum type (e.g. an enum refactored into a class/struct with the
            // same name) would make Enum.TryParse/Enum.GetValues below throw — fall back to the
            // raw string field, same as EnumValuesPropertyDrawerHelper's guard.
            if (enumType is null || !enumType.IsEnum)
            {
                EditorGUI.PropertyField(rect, keyProperty, GUIContent.none);
                return;
            }

            if (!Enum.TryParse(enumType, keyProperty.stringValue, out var parsed))
            {
                // Stored key doesn't match any member (first-time init, or the enum was
                // edited/renamed since). Fall back to the first member and persist it, migrating
                // the stale key rather than leaving the row unusable.
                var values = Enum.GetValues(enumType);

                if (values.Length is 0)
                {
                    EditorGUI.PropertyField(rect, keyProperty, GUIContent.none);
                    return;
                }

                parsed = values.GetValue(0);
            }

            var enumValue = (Enum)parsed;

            if (keyProperty.stringValue != enumValue.ToString())
                keyProperty.SetStringAndApply(enumValue.ToString());

            var selected = enumType.IsDefined(typeof(FlagsAttribute), false)
                ? EditorGUI.EnumFlagsField(rect, enumValue)
                : EditorGUI.EnumPopup(rect, enumValue);

            if (!Equals(selected, enumValue))
                keyProperty.SetStringAndApply(selected.ToString());
        }
    }
}
