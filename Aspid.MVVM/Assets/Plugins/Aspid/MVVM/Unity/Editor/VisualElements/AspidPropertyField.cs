#nullable enable
using System.Linq;
using UnityEngine;
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class AspidPropertyField : PropertyField
    {
        public const string StyleClass = "aspid-property-field";
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Editor/Styles/aspid-mvvm-property-field");
        
        public AspidPropertyField(SerializedProperty property)
            : base(property)
        {
            Initialize();
        }

        public AspidPropertyField(SerializedProperty property, string label) 
            : base(property, label)
        {
            Initialize();
        }

        private void Initialize()
        {
            styleSheets.Add(StyleSheet);
            RegisterCallback<GeometryChangedEvent>(SetStyles);
        }
        
        private void SetStyles(GeometryChangedEvent e)
        {
            // For Field
            var field = Children().FirstOrDefault(element => !element.ClassListContains("unity-decorator-drawers-container"));

            if (parent is AspidContainer container)
            {
                if (container[0] == this)
                {
                    Children()
                        .FirstOrDefault(element => element.ClassListContains("unity-decorator-drawers-container"))
                        ?.Children()
                        .FirstOrDefault(element => element.ClassListContains("unity-header-drawer__label"))
                        ?.SetMargin(top: 0);
                }
            }
            
            field?.AddToClassList(StyleClass);
            field?.AddToClassList(AspidContainer.GetStyleClass(AspidContainer.StyleType.Lighter));
            
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