#nullable enable
using System;
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// Helpers shared by the <see cref="EnumValues{TValue}"/> property drawer.
    /// </summary>
    internal static class EnumValuesPropertyDrawerHelper
    {
        private const string PopulateMenuItem = "Populate Missing Enum Members";

        /// <summary>
        /// Builds a context-menu manipulator that adds a "Populate Missing Enum Members"
        /// action. The action appends entries for every enum member of the configured type
        /// that is not yet present in <paramref name="values"/>, using
        /// <paramref name="defaultValue"/> as the seed for the new entries.
        /// The action is disabled when nothing is missing.
        /// </summary>
        /// <remarks>
        /// For <c>[Flags]</c> enums <see cref="Enum.GetNames"/> also returns named composite
        /// values (e.g. <c>All = A | B</c>), so they will be added as separate rows.
        /// </remarks>
        public static ContextualMenuManipulator CreatePopulateMenuManipulator(
            SerializedProperty values,
            SerializedProperty enumType,
            SerializedProperty defaultValue)
        {
            var serializedObject = values.serializedObject;
            var valuesPath = values.propertyPath;
            var enumTypePath = enumType.propertyPath;
            var defaultValuePath = defaultValue.propertyPath;

            return new ContextualMenuManipulator(evt =>
            {
                var valuesProp = serializedObject.FindProperty(valuesPath);
                var enumTypeProp = serializedObject.FindProperty(enumTypePath);
                var defaultValueProp = serializedObject.FindProperty(defaultValuePath);

                var status = HasMissingMembers(valuesProp, enumTypeProp)
                    ? DropdownMenuAction.Status.Normal
                    : DropdownMenuAction.Status.Disabled;

                evt.menu.AppendAction(
                    PopulateMenuItem,
                    _ => PopulateMissing(valuesProp, enumTypeProp, defaultValueProp),
                    status);
            });
        }

        private static void PopulateMissing(
            SerializedProperty values,
            SerializedProperty enumType,
            SerializedProperty defaultValue)
        {
            var type = Type.GetType(enumType.stringValue, throwOnError: false);
            if (type is null || !type.IsEnum) return;

            var existing = CollectExistingKeys(values);
            var added = false;

            foreach (var name in Enum.GetNames(type))
            {
                if (!existing.Add(name)) continue;

                values.arraySize++;
                var element = values.GetArrayElementAtIndex(values.arraySize - 1);
                element.FindPropertyRelative("_key").stringValue = name;
                element.FindPropertyRelative("_enumType").stringValue = enumType.stringValue;
                element.FindPropertyRelative("_value").boxedValue = defaultValue.boxedValue;
                added = true;
            }

            if (added) values.serializedObject.ApplyModifiedProperties();
        }

        private static bool HasMissingMembers(SerializedProperty values, SerializedProperty enumType)
        {
            var type = Type.GetType(enumType.stringValue, throwOnError: false);
            if (type is null || !type.IsEnum) return false;

            var existing = CollectExistingKeys(values);
            return Enum.GetNames(type).Any(name => !existing.Contains(name));
        }

        private static HashSet<string> CollectExistingKeys(SerializedProperty values)
        {
            var set = new HashSet<string>(values.arraySize);
            for (var i = 0; i < values.arraySize; i++)
            {
                var element = values.GetArrayElementAtIndex(i);
                set.Add(element.FindPropertyRelative("_key").stringValue);
            }
            return set;
        }
    }
}
