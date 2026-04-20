#nullable enable
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// A styled property field visual element used in the Aspid MVVM inspector UI.
    /// Extends <see cref="PropertyField"/> with the <c>Styles/Aspid-MVVM-AspidPropertyField</c> style sheet and class.
    /// </summary>
    public class AspidPropertyField : PropertyField
    {
        public const string StyleClass = "aspid-property-field";
        public static readonly StyleSheet StyleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-AspidPropertyField");
        
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
            this.Bind(property.serializedObject);
            
            styleSheets.Add(StyleSheet);
            RegisterCallback<GeometryChangedEvent>(SetStyles);
        }
        
        private void SetStyles(GeometryChangedEvent e)
        {
            // For Field
            var field = Children().FirstOrDefault(element => !element.ClassListContains("unity-decorator-drawers-container"));

            var wrapper = (VisualElement)this;
            while (wrapper.parent is not null and not AspidBox)
                wrapper = wrapper.parent;

            if (wrapper.parent is AspidBox container && container[0] == wrapper)
            {
                Children()
                    .FirstOrDefault(element => element.ClassListContains("unity-decorator-drawers-container"))
                    ?.Children()
                    .FirstOrDefault(element => element.ClassListContains("unity-header-drawer__label"))
                    ?.SetMargin(top: 0);
            }
            
            field?.AddToClassList(StyleClass);
            field?.AddToClassList(StyleClasses.Theme.Lightness);
            
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