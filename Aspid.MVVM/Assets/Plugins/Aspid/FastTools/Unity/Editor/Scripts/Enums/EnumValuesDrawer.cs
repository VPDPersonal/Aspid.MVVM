#nullable enable
using System;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    internal static class EnumValuesDrawer
    {
        private const string PopulateMenuItem = "Populate Missing Enum Members";

        internal static ContextualMenuManipulator CreatePopulateMenuManipulator(
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
            foreach (var name in Enum.GetNames(type))
            {
                if (!existing.Contains(name)) return true;
            }
            return false;
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
