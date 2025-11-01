using System;
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class DelegateField : VisualElement
    {
        public DelegateField(Delegate value, string label, string prefsKey)
        {
            if (value is null)
            {
                this.AddChild(new NullField(label));
                return;
            }

            var container = new AspidContainer(AspidContainer.StyleType.Lighter);
            var subcontainer = CreateSubcontainer(value, label, prefsKey);
            container.AddChild(subcontainer);

            var delegates = value.GetInvocationList();
            
            foreach (var @delegate in delegates)
            {
                if (@delegate?.Target is null || @delegate.Target is Object obj && !obj)
                {
                    subcontainer.AddChild(new AspidHelpBox("Delegate is null", HelpBoxMessageType.Error));
                    continue;
                }

                subcontainer
                    .AddChild(new VisualElement()
                        .SetFlexDirection(FlexDirection.Row)
                        .AddChild(CreateTargetField(@delegate))
                        .AddChild(CreateMethodField(@delegate)));
            }

            this.AddChild(container);
        }

        private static VisualElement CreateSubcontainer(Delegate value, string label, string prefsKey)
        {
            var delegates = value.GetInvocationList();

            if (delegates.Length is 1)
            {
                return new VisualElement()
                    .AddChild(new Label(label)
                        .SetMargin(bottom: 5));
            }
            
            var foldout = new Foldout()
                .SetText(label)
                .SetMargin(left: 5)
                .SetValue(!string.IsNullOrWhiteSpace(prefsKey) && EditorPrefs.GetBool(prefsKey, false));
                    
            foldout.RegisterValueChangedCallback(e =>
            {
                if (e.target == foldout && !string.IsNullOrWhiteSpace(prefsKey))
                    EditorPrefs.SetBool(prefsKey, e.newValue);
            });
            
            return foldout;
        }

        private static VisualElement CreateTargetField(Delegate value)
        {
            VisualElement field = value.Target switch
            {
                Object obj => new ObjectField().SetValue(obj),
                _ => new TextField().SetValue(GetTargetValue()),
            };
            
            return SetupFiled(field);

            string GetTargetValue()
            {
                var type = value.Target?.GetType();
                
                return type is null 
                    ? string.Empty 
                    : $"{type.Namespace?.TrimEnd('.')}{type.Name}";
            }
        }

        private static VisualElement CreateMethodField(Delegate value)
        {
            var field = new TextField().SetValue(value.Method.Name);
            return SetupFiled(field);
        }
        
        private static VisualElement SetupFiled(VisualElement field)
        {
            var width = new StyleLength(new Length(50, LengthUnit.Percent));
            
            field.SetEnabled(false);
            field.SetSize(width: width);
            field.SetMargin(0, 0, 0, 0);

            return field;
        }
    }
}