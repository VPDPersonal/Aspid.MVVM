#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using Aspid.MVVM.StarterKit;
using UnityEngine.UIElements;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class GeneralViewVisualElement : MonoViewVisualElement<GeneralView, GeneralViewEditor>
    {
        private VisualElement? _generalBinders;
        
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
            
            _generalBinders = new VisualElement();
            FillGeneralBinders();
            
            return container.AddChild(_generalBinders);
        }

        private void FillGeneralBinders()
        {
            for (var i = 0; i < Editor.BindersList.ArraySize; i++)
            {
                var element = Editor.BindersList.GetArrayElementAtIndex(i);
                _generalBinders.AddChild(new AspidPropertyField(element.MonoBindersProperty, element.Id)
                    .SetMargin(top: 3));
            }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            // FillGeneralBinders();
        }
    }
}