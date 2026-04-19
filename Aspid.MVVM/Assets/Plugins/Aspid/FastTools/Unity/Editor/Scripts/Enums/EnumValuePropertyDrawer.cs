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

            return container
                .AddChild(enumTypeField)
                .AddChild(keyField)
                .AddChild(keyEnumField)
                .AddChild(keyEnumFlagField)
                .AddChild(new PropertyField(property.FindPropertyRelative("_value"), label: string.Empty));

            void UpdateValue()
            {
                var enumType = Type.GetType(enumTypeProperty.stringValue, throwOnError: false);

                keyField.SetDisplay(DisplayStyle.None);
                keyEnumField.SetDisplay(DisplayStyle.None);
                keyEnumFlagField.SetDisplay(DisplayStyle.None);
                
                if (enumType is null)
                {
                    keyField.SetDisplay(DisplayStyle.Flex);
                }
                else
                {
                    Enum enumValue;

                    try
                    {
                        enumValue = (Enum)Enum.Parse(enumType, keyProperty.stringValue);
                    }
                    catch
                    {
                        try
                        {
                            enumValue = (Enum)Enum.GetValues(enumType).GetValue(index: 0);
                        }
                        catch
                        {
                            keyField.SetDisplay(DisplayStyle.Flex);
                            return;
                        }
                    }
                    
                    keyProperty.SetStringAndApply(enumValue.ToString());
                    
                    if (enumType.IsDefined(typeof(FlagsAttribute), false))
                    {
                        keyEnumFlagField
                            .SetValue(null)
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
}
