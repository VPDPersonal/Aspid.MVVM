using System.Linq;
using UnityEngine;
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class AspidPropertyField : PropertyField
    {
        public AspidPropertyField(SerializedProperty property)
            : base(property)
        {
            Initialize();
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-property-field"));
        }

        public AspidPropertyField(SerializedProperty property, string label) 
            : base(property, label)
        {
            Initialize();
            styleSheets.Add(Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-property-field"));
        }

        private void Initialize()
        {
            RegisterCallback<GeometryChangedEvent>(SetStyles);
        }
        
        private void SetStyles(GeometryChangedEvent e)
        {
            // For Field
            var field = Children().FirstOrDefault(element => !element.ClassListContains("unity-decorator-drawers-container"));
            field?.AddToClassList("aspid-property-field");
            field?.AddToClassList("aspid-lighter-container");
            
            // For [SerializeReferenceDropdown]
            foreach (var dropdown in this.Query<VisualElement>("dropdown-group").Build())
            {
                var labelElement = dropdown.parent?.Q<Label>();
                if (labelElement is null) continue;
                
                var size = labelElement.MeasureTextSize(labelElement.text, 0, MeasureMode.Undefined, 0, MeasureMode.Undefined);
                dropdown.style.left = Mathf.Max(75, size.x);
                dropdown.SetMargin(left: 15);
            }
        }
    }
}