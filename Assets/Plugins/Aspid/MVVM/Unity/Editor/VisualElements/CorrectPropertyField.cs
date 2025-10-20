using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class CorrectPropertyField : PropertyField
    {
        public CorrectPropertyField()
        {
            RegisterCallback<GeometryChangedEvent>(Fix);
        }

        public CorrectPropertyField(SerializedProperty property)
            : base(property)
        {
            RegisterCallback<GeometryChangedEvent>(Fix);
        }

        public CorrectPropertyField(SerializedProperty property, string label) 
            : base(property, label)
        {
            RegisterCallback<GeometryChangedEvent>(Fix);
        }
        
        private void Fix(GeometryChangedEvent e)
        {
            var toggle = this.Q<Foldout>()?.Q<Toggle>();
            if (toggle is null) return;
            
            toggle.SetMargin(left: 0);
            
            var dropdown = this.Q<VisualElement>("dropdown-group");
            if (dropdown is null) return;

            var labelElement = toggle.Q<Label>();
            if (labelElement is null) return;

            var size = labelElement.MeasureTextSize(labelElement.text, 0, MeasureMode.Undefined, 0, MeasureMode.Undefined);
          
            dropdown.style.left = size.x;
            dropdown.SetMargin(left: 15);
        }
    }
}