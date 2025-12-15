using System;
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class AspidDelegateField : Foldout
    {
        private const string StyleSheetPath = "Styles/Fields/aspid-delegate";
        
        public AspidDelegateField(string label, Delegate value)
        {
            this.SetText(label)
                .styleSheets.Add(styleSheet: Resources.Load<StyleSheet>(StyleSheetPath));;
            
            var delegates = value.GetInvocationList();
            
            foreach (var @delegate in delegates)
            {
                if (@delegate?.Target is null || @delegate.Target is Object obj && !obj)
                {
                    this.AddChild(new AspidHelpBox(message: "Delegate is null", HelpBoxMessageType.Error));
                    continue;
                }

                this.AddChild(new VisualElement().SetName("delegate-content")
                    .AddChild(CreateTargetField(@delegate).SetName("content-1"))
                    .AddChild(CreateMethodField(@delegate).SetName("content-2")));
            }
        }

        private static VisualElement CreateTargetField(Delegate value)
        {
            VisualElement field ;

            if (value.Target is Object obj)
            {
                field = new ObjectField().SetValue(obj);
                field.SetEnabled(false);
            }
            else
            {
                var textField = new TextField().SetValue(GetTargetValue());
                textField.isReadOnly = true;
                
                field = textField;
            }
            
            return field;

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
            var field = new TextField()
                .SetValue(value.Method.Name);

            field.isReadOnly = true;
            return field;
        }
    }
}