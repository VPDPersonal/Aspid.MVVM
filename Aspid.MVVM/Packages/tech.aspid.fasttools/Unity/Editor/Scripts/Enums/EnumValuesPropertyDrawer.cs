using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors;
using Aspid.FastTools.UIElements.Manipulators;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// Property drawer for <see cref="EnumValues{TValue}"/>. Renders a header with the enum-type
    /// picker, the entries list, and the default-value field, and exposes a context-menu action
    /// that fills in any missing enum members from the configured type.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumValues<>))]
    internal sealed class EnumValuesPropertyDrawer : PropertyDrawer
    {
        private const string StylesheetPath = "UI/Enums/Aspid-FastTools-EnumValues";
        private const string HeaderClass = "aspid-fasttools-enum-values__header";
        private const string ContainerClass = "aspid-fasttools-enum-values__container";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var serializedObject = property.serializedObject;
            var valuesPath = property.FindPropertyRelative("_values").propertyPath;
            var enumTypePath = property.FindPropertyRelative("_enumType").propertyPath;
            var defaultValuePath = property.FindPropertyRelative("_defaultValue").propertyPath;

            // Push the parent enum type into every existing entry up-front so already-serialized
            // arrays don't render with a stale per-element _enumType until the user re-edits.
            UpdateValues();
            
            return new VisualElement()
                .SetName($"enum-values-{property.displayName}")
                .AddStyleSheetsFromResource(StylesheetPath)
                .AddStyleSheetsFromResource(AspidStyles.DefaultStyleSheet)
                .AddManipulatorSelf(EnumValuesPropertyDrawerHelper.CreatePopulateMenuManipulator(
                    values: serializedObject.FindProperty(valuesPath),
                    enumType: serializedObject.FindProperty(enumTypePath),
                    defaultValue: serializedObject.FindProperty(defaultValuePath))
                )
                .AddChild(new VisualElement()
                    .AddClass(HeaderClass)
                    .AddChild(new Label(property.displayName))
                    .AddChild(new PropertyField(serializedObject.FindProperty(enumTypePath), label: string.Empty)
                        .AddValueChanged(_ => UpdateValues())
                    )
                )
                .AddChild(new VisualElement()
                    .AddClass(ContainerClass)
                    .AddChild(new PropertyField(serializedObject.FindProperty(valuesPath))
                        .AddValueChanged(_ => UpdateValues())
                    )
                    .AddChild(new PropertyField(serializedObject.FindProperty(defaultValuePath)))
                );

            void UpdateValues()
            {
                var values = serializedObject.FindProperty(valuesPath);
                var enumTypeValue = serializedObject.FindProperty(enumTypePath).stringValue;

                for (var i = 0; i < values.arraySize; i++)
                {
                    var enumTypeElement = values.GetArrayElementAtIndex(i).FindPropertyRelative("_enumType");

                    if (enumTypeElement.stringValue != enumTypeValue)
                        enumTypeElement.SetStringAndApply(enumTypeValue);
                }
            }
        }
    }
}
