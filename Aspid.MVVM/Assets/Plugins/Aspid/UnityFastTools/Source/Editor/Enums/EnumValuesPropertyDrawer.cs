using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.UnityFastTools
{
    // TODO Aspid.UnityFastTools – Refactor
    [CustomPropertyDrawer(typeof(EnumValues<>))]
    public sealed class EnumValuesPropertyDrawer : PropertyDrawer
    {
        private static readonly StyleFloat _borderWidth = 1;
        private static readonly StyleLength _borderRadius = 5;
        private static readonly StyleColor _borderColor = new Color(r: 0.1254902f, g:0.1254902f, b: 0.1254902f);
        private static readonly StyleColor _backgroundColor = new Color(r: 0.2745098f, g:0.2745098f, b: 0.2745098f);
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            var values = property.FindPropertyRelative("_values");
            var enumType = property.FindPropertyRelative("_enumType");
            var defaultValueProperty = property.FindPropertyRelative("_defaultValue");

            var header = new VisualElement()
                .SetPadding(2)
                .SetBorderColor(_borderColor)
                .SetBorderWidth(_borderWidth)
                .SetBorderRadius(topLeft: _borderRadius, topRight: _borderRadius);

            root.Add(header);

            var enumTypeField = new PropertyField(enumType, label: string.Empty);
            enumTypeField.RegisterValueChangeCallback(e =>
            {
                UpdateValues();
            });

            header
                .AddChild(new Label(property.displayName))
                .AddChild(enumTypeField);

            var container = new VisualElement()
                .SetBorderColor(_borderColor)
                .SetBackgroundColor(_backgroundColor)
                .SetBorderRadius(bottomLeft: _borderRadius, bottomRight: _borderRadius)
                .SetBorderWidth(bottom: _borderWidth, left: _borderWidth, right: _borderWidth);

            container
                .AddChild(new PropertyField(values)
                    .SetMargin(top: 5, bottom: 2, left: 17, right: 5))
                .AddChild(new PropertyField(defaultValueProperty)
                .SetMargin(bottom: 5, left: 7, right: 7));
            
            return root
                .AddChild(container)
                .SetMargin(top: 2, bottom: 2, right: -2);

            void UpdateValues()
            {
                for (var i = 0; i < values.arraySize; i++)
                {
                    var element = values.GetArrayElementAtIndex(i);
                    element.FindPropertyRelative("_enumType").SetStringAndApply(enumType.stringValue);
                }
            }
        }
    }
}