using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.Types.Editors;
using Aspid.FastTools.UIElements.Editors;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// UIToolkit rendering for <see cref="EnumValuesPropertyDrawer"/>.
    /// </summary>
    internal static class EnumValuesUIToolkitPropertyDrawer
    {
        private const string StylesheetPath = "UI/Enums/Aspid-FastTools-EnumValues";

        private const string UssClass = "aspid-fasttools-enum-values";
        private const string HeaderClass = UssClass + "__header";
        private const string ContainerClass = UssClass + "__container";

        public static VisualElement Draw(SerializedProperty property, bool isTyped)
        {
            var serializedObject = property.serializedObject;
            var valuesPath = property.FindPropertyRelative("_values").propertyPath;
            var enumTypePath = property.FindPropertyRelative("_enumType").propertyPath;
            var defaultValuePath = property.FindPropertyRelative("_defaultValue").propertyPath;

            // Push the parent enum type into every existing entry up-front so already-serialized
            // arrays don't render with a stale per-element _enumType until the user re-edits.
            UpdateValues();

            var header = new VisualElement()
                .AddClass(HeaderClass)
                .AddChild(new Label(property.displayName));

            header.AddChild(isTyped
                ? new InspectorTypeField(label: null, serializedObject.FindProperty(enumTypePath))
                {
                    IsReadOnly = true
                }
                : new PropertyField(serializedObject.FindProperty(enumTypePath), label: string.Empty));

            var root = new VisualElement()
                .SetName($"enum-values-{property.name.ToKebabCase()}")
                .AddAspidThemeStyleSheets()
                .AddStyleSheetsFromResource(StylesheetPath)
                .AddManipulatorSelf(EnumValuesPropertyDrawerHelper.CreatePopulateMenuManipulator(
                    serializedObject: serializedObject,
                    values: valuesPath,
                    enumType: enumTypePath,
                    defaultValue: defaultValuePath)
                )
                .AddChild(header)
                .AddChild(new VisualElement()
                    .AddClass(ContainerClass)
                    .AddChild(new PropertyField(serializedObject.FindProperty(valuesPath))
                        .AddValueChanged(_ => UpdateValues())
                    )
                    .AddChild(new PropertyField(serializedObject.FindProperty(defaultValuePath)))
                );

            // The enum-type PropertyField hosts the TypeSelector custom drawer, which writes the
            // picked type straight into the SerializedProperty — PropertyField only forwards
            // UI-driven ChangeEvents from custom drawers, so a SerializedPropertyChangeEvent
            // callback would never fire. Track the property itself instead.
            // The typed variant's enum type never changes — nothing to track.
            if (!isTyped)
            {
                root.TrackPropertyValue(serializedObject.FindProperty(enumTypePath), _ => UpdateValues());
            }

            return root;

            void UpdateValues()
            {
                var values = serializedObject.FindProperty(valuesPath);
                var enumTypeValue = serializedObject.FindProperty(enumTypePath).stringValue;

                for (var i = 0; i < values.arraySize; i++)
                {
                    var enumTypeElement = values
                        .GetArrayElementAtIndex(i)
                        .FindPropertyRelative("_enumType");

                    if (enumTypeElement.stringValue != enumTypeValue)
                        enumTypeElement.SetStringAndApply(enumTypeValue);
                }
            }
        }
    }
}
