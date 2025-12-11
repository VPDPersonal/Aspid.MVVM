using System;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class AspidDelegateField : Foldout
    {
        public AspidDelegateField(string label, Delegate value)
        {
            this.SetText(label)
                .SetMargin(left: 12);

            this.Q("unity-content")
                .SetMargin(left: 0);

            var delegates = value.GetInvocationList();
            
            foreach (var @delegate in delegates)
            {
                if (@delegate?.Target is null || @delegate.Target is Object obj && !obj)
                {
                    this.AddChild(new AspidHelpBox("Delegate is null", HelpBoxMessageType.Error));
                    continue;
                }

                this.AddChild(new VisualElement()
                    .SetFlexDirection(FlexDirection.Row)
                    .AddChild(CreateTargetField(@delegate))
                    .AddChild(CreateMethodField(@delegate)
                        .SetMargin(right: 3)));
            }
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
                    : type.GetTypeDisplayName();
            }
        }

        private static VisualElement CreateMethodField(Delegate value)
        {
            var field = new TextField().SetValue(value.Method.Name);
            return SetupFiled(field);
        }
        
        private static VisualElement SetupFiled(VisualElement field)
        {
            field.SetEnabled(false);
            field.SetFlexShrink(1);
            field.SetSize(width: new StyleLength(new Length(50, LengthUnit.Percent)));
            
            return field;
        }
    }
}