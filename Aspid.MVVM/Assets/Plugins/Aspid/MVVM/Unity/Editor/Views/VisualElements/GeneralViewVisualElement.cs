#nullable enable
using Aspid.UnityFastTools;
using Aspid.MVVM.StarterKit;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class GeneralViewVisualElement : MonoViewVisualElement<GeneralView, GeneralViewEditor>
    {
        public const string GeneralBindersId = "general-binders";
        
        protected override IEnumerable<string> PropertiesExcluding
        {
            get
            {
                foreach (var property in base.PropertiesExcluding)
                    yield return property;
                
                yield return "_designViewModel";
                yield return "_bindersList";
            }
        }
        
        public GeneralViewVisualElement(GeneralViewEditor editor) 
            : base(editor) { }

        protected override VisualElement? OnBuiltHeader() =>
            BuiltGeneralBinders();
        
        protected VisualElement BuiltGeneralBinders()
        {
            var container = new AspidContainer()
                .AddChild(new AspidPropertyField(Editor.DesignViewModel));
            
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
        }
    }
}