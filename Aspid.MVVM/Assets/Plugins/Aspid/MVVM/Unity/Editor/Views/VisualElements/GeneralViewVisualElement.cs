#nullable enable
using System;
using Aspid.UnityFastTools;
using Aspid.MVVM.StarterKit;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public class GeneralViewVisualElement : MonoViewVisualElement<GeneralView, GeneralViewEditor>
    {
        private const string GeneralBindersId = "general-binders";
        
        protected override IEnumerable<string> PropertiesExcluding
        {
            get
            {
                foreach (var property in base.PropertiesExcluding)
                    yield return property;
                
                yield return Editor.BindersList.Property.propertyPath;
                yield return Editor.DesignViewModel.propertyPath;
            }
        }
        
        public GeneralViewVisualElement(GeneralViewEditor editor) 
            : base(editor) { }

        protected override VisualElement? OnBuiltHeader()
        {
            return new VisualElement()
                .AddChild(BuildDesignViewModel())
                .AddChild(BuiltGeneralBinders());
        }

        protected VisualElement BuildDesignViewModel()
        {
            var container = new AspidContainer(AspidContainer.StyleType.Dark);
            container.AddChild(new AspidTitle("Design ViewModel"));
            
            var propertyField = new PropertyField(Editor.DesignViewModel);
            propertyField.Bind(Editor.serializedObject);
            
            return container.AddChild(propertyField);
        }
        
        protected VisualElement BuiltGeneralBinders()
        {
            var container = new AspidContainer();
            
            return container
                .AddChild(BuildGeneralBinders());
        }

        private VisualElement BuildGeneralBinders()
        {
            var container = new VisualElement().SetName(GeneralBindersId);
            
            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);

                var property = new AspidPropertyField(element.MonoBindersProperty, element.Id)
                    .SetMargin(top: 3);
                
                property.Bind(Editor.serializedObject);
                container.AddChild(property);
            }

            return container;
        }

        public void UpdateGeneralBinders()
        {
            var generalBinders = this.Q<VisualElement>(GeneralBindersId);
            if (generalBinders is null) return;
            
            var generalBindersParent = generalBinders.parent;
            generalBindersParent.Remove(this.Q<VisualElement>(GeneralBindersId));
            generalBindersParent.AddChild(BuildGeneralBinders());

            generalBindersParent.SetDisplay(this.Q<VisualElement>(GeneralBindersId).childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
        }

        protected override string GetScriptName()
        {
            if (Editor.TargetAsView.GetType() != typeof(GeneralView)) return base.GetScriptName();

            if (!string.IsNullOrWhiteSpace(Editor.DesignViewModel.stringValue))
            {
                return Type.GetType(Editor.DesignViewModel.stringValue).Name + " (General View)";
            }

            return "General View";
        }
    }
}