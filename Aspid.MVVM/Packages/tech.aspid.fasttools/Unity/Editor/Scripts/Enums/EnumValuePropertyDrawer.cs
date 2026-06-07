using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums.Editors
{
    /// <summary>
    /// Property drawer for <see cref="EnumValue{TValue}"/>. Picks an EnumField/EnumFlagsField
    /// for the row's key based on the enum type configured on the parent
    /// <see cref="EnumValues{TValue}"/>, and falls back to a raw string field when the type
    /// can't be resolved.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumValue<>))]
    internal sealed class EnumValuePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();

            var keyProperty = property.FindPropertyRelative("_key");
            var enumTypeProperty = property.FindPropertyRelative("_enumType");

            var enumTypeField = new PropertyField(enumTypeProperty, label: string.Empty).SetDisplay(DisplayStyle.None);
            var keyField = new PropertyField(keyProperty, label: string.Empty).SetDisplay(DisplayStyle.None);

            var keyEnumField = new EnumField(label: string.Empty).SetDisplay(DisplayStyle.None);
            var keyEnumFlagField = new EnumFlagsField(label: string.Empty).SetDisplay(DisplayStyle.None);

            enumTypeField.AddValueChanged(_ => UpdateValue());

            keyEnumField.AddValueChanged(e =>
            {
                keyProperty.SetStringAndApply(e.newValue.ToString());
            });

            keyEnumFlagField.AddValueChanged(e =>
            {
                keyProperty.SetStringAndApply(e.newValue.ToString());
            });

            container
                .AddChild(enumTypeField)
                .AddChild(keyField)
                .AddChild(keyEnumField)
                .AddChild(keyEnumFlagField)
                .AddChild(new PropertyField(property.FindPropertyRelative("_value"), label: string.Empty));

            // Sync visibility with the currently serialized enum type — without this the
            // EnumField/EnumFlagsField stay hidden until the user edits the type.
            UpdateValue();

            return container;

            void UpdateValue()
            {
                var enumType = Type.GetType(enumTypeProperty.stringValue, throwOnError: false);

                keyField.SetDisplay(DisplayStyle.None);
                keyEnumField.SetDisplay(DisplayStyle.None);
                keyEnumFlagField.SetDisplay(DisplayStyle.None);

                if (enumType is null)
                {
                    keyField.SetDisplay(DisplayStyle.Flex);
                    return;
                }

                if (!Enum.TryParse(enumType, keyProperty.stringValue, out var parsed))
                {
                    var values = Enum.GetValues(enumType);
                    if (values.Length is 0)
                    {
                        keyField.SetDisplay(DisplayStyle.Flex);
                        return;
                    }
                    parsed = values.GetValue(0);
                }

                var enumValue = (Enum)parsed;

                if (keyProperty.stringValue != enumValue.ToString())
                    keyProperty.SetStringAndApply(enumValue.ToString());

                if (enumType.IsDefined(typeof(FlagsAttribute), false))
                {
                    keyEnumFlagField
                        .SetValue(null, notify: false)
                        .Initialize(enumValue)
                        .SetDisplay(DisplayStyle.Flex);
                }
                else
                {
                    keyEnumField
                        .Initialize(enumValue)
                        .SetDisplay(DisplayStyle.Flex);
                }
            }
        }
    }
}
