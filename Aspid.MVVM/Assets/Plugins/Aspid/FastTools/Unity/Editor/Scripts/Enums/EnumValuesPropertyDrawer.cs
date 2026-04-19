using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    [CustomPropertyDrawer(typeof(EnumValues<>))]
    internal sealed class EnumValuesPropertyDrawer : PropertyDrawer
    {
        private const string StylesheetPath = "Styles/Aspid-FastTools-EnumValues";
        private const string RootClass = "aspid-fasttools-enum-values";
        private const string HeaderClass = "aspid-fasttools-enum-values-header";
        private const string ContainerClass = "aspid-fasttools-enum-values-container";
        private const string ValuesClass = "aspid-fasttools-enum-values-values";
        private const string DefaultValueClass = "aspid-fasttools-enum-values-default-value";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var serializedObject = property.serializedObject;
            var valuesPath = property.FindPropertyRelative("_values").propertyPath;
            var enumTypePath = property.FindPropertyRelative("_enumType").propertyPath;
            var defaultValuePath = property.FindPropertyRelative("_defaultValue").propertyPath;

            var root = new VisualElement().SetName($"enum-values-{property.displayName}")
                .AddStyleSheetsFromResource(StylesheetPath)
                .AddClass(RootClass);

            root.AddManipulator(EnumValuesDrawer.CreatePopulateMenuManipulator(
                serializedObject.FindProperty(valuesPath),
                serializedObject.FindProperty(enumTypePath),
                serializedObject.FindProperty(defaultValuePath)));

            var enumTypeField = new PropertyField(serializedObject.FindProperty(enumTypePath), label: string.Empty)
                .AddValueChanged(_ => UpdateValues());

            var valuesField = new PropertyField(serializedObject.FindProperty(valuesPath))
                .AddClass(ValuesClass)
                .AddValueChanged(_ => UpdateValues());

            return root
                .AddChild(new VisualElement()
                    .AddClass(HeaderClass)
                    .AddChild(new Label(property.displayName))
                    .AddChild(enumTypeField)
                )
                .AddChild(new VisualElement()
                    .AddClass(ContainerClass)
                    .AddChild(valuesField)
                    .AddChild(new PropertyField(serializedObject.FindProperty(defaultValuePath))
                        .AddClass(DefaultValueClass)
                    )
                );

            void UpdateValues()
            {
                var values = serializedObject.FindProperty(valuesPath);
                var enumTypeValue = serializedObject.FindProperty(enumTypePath).stringValue;

                for (var i = 0; i < values.arraySize; i++)
                {
                    var element = values.GetArrayElementAtIndex(i);
                    element.FindPropertyRelative("_enumType").SetStringAndApply(enumTypeValue);
                }
            }
        }
    }
}
