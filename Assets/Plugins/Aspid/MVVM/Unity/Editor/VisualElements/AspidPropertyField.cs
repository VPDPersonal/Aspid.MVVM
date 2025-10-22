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
        private static readonly Color _backgroundColor = new(0.2196079f, 0.2196079f, 0.2196079f, 1);
        
        private bool _isArray;

        public AspidPropertyField(SerializedProperty property)
            : base(property)
        {
            Initialize(property);
        }

        public AspidPropertyField(SerializedProperty property, string label) 
            : base(property, label)
        {
            Initialize(property);
        }

        private void Initialize(SerializedProperty property)
        {
            _isArray = property.isArray;
            RegisterCallback<GeometryChangedEvent>(SetStyles);
        }
        
        private void SetStyles(GeometryChangedEvent e)
        {
            // For Field
            var field = Children().FirstOrDefault(element => !element.ClassListContains("unity-decorator-drawers-container"));
            
            field?.SetMargin(top: 4, left: 0)
                .SetBackgroundColor(_backgroundColor)
                .SetPadding(top: 1.5f, bottom: 1.5f, left: 3, right: 5)
                .SetBorderRadius(topLeft: 5, topRight: 5, bottomLeft: 5, bottomRight: 5);
            
            // For Foldout
            foreach (var element in this.Query<VisualElement>(className: "unity-foldout__toggle--inspector").Build())
            {
                element.SetMargin(left: 0);
            }

            // For IMGUIContainer
            foreach (var element in this.Query<IMGUIContainer>().Build())
            {
                element.SetMargin(bottom: 1.5f, left: 15, right: 0);
            }
            
            // For [SerializeReferenceDropdown]
            foreach (var dropdown in this.Query<VisualElement>("dropdown-group").Build())
            {
                var labelElement = dropdown.parent?.Q<Label>();
                if (labelElement is null) continue;
                
                var size = labelElement.MeasureTextSize(labelElement.text, 0, MeasureMode.Undefined, 0, MeasureMode.Undefined);
                dropdown.style.left = Mathf.Max(75, size.x);
                dropdown.SetMargin(left: 15);
            }
            
            // For Array and List
            if (_isArray)
            {
                foreach (var element in this.Query<ListView>().Build())
                { 
                    element.Q<TextField>("unity-list-view__size-field")
                        ?.SetMargin(right: 5)
                        .SetSize(width: 100);
                    
                    element.Q<ScrollView>()
                        ?.SetMargin(top: 3);
                }
            }
        }
    }
}