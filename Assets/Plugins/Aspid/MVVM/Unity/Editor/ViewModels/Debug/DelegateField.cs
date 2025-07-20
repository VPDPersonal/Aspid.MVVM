using System;
using UnityEditor;
using Aspid.CustomEditors;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.Unity
{
    public sealed class DelegateField : VisualElement
    {
        public DelegateField(Delegate value, string label, string prefsKey = null)
        {
            if (label is null) throw new ArgumentNullException(nameof(label));
            
            if (value is null)
            {
                this.AddChild(new NullField(label));
                return;
            }

            var container = Elements.CreateContainer(EditorColor.LighterContainer);
            var subcontainer = CreateSubcontainer(container, value, label, prefsKey);

            var delegates = value.GetInvocationList();
            
            foreach (var @delegate in delegates)
            {
                if (@delegate?.Target is null || @delegate.Target is Object obj && !obj)
                {
                    subcontainer.AddChild(Elements.CreateHelpBox("Delegate is null", HelpBoxMessageType.Error));
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

        private static VisualElement CreateSubcontainer(VisualElement container, Delegate value, string label, string prefsKey)
        {
            var delegates = value.GetInvocationList();

            if (delegates.Length is 1)
            {
                return container
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
                
            container.AddChild(foldout);
            return foldout;
        }

        private static VisualElement CreateTargetField(Delegate value)
        {
            VisualElement field;
            
            if (value.Target is Object obj)
            {
                field = new ObjectField
                {
                    value = obj
                };
            }
            else
            {
                var targetType = value.Target.GetType();
                var name = targetType.Namespace;
                if (!string.IsNullOrEmpty(name)) name += ".";
                name = (name ?? "") + targetType.Name;
                
                field = new TextField
                {
                    value = $"{targetType.Namespace}.{name}"
                };
            }
            
            field.SetEnabled(false);
            SetupFiled(field);

            return field;
        }

        private static VisualElement CreateMethodField(Delegate value)
        {
            var field = new TextField { value = value.Method.Name };
            SetupFiled(field);

            return field;
        }
        
        private static void SetupFiled(VisualElement field)
        {
            var width = new StyleLength(new Length(50, LengthUnit.Percent));
            
            field.SetEnabled(false);
            field.SetSize(width: width);
            field.SetMargin(0, 0, 0, 0);
        }
    }
}