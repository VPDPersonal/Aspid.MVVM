#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM Unity – Write summary
    public sealed class UnassignedBindersVisualElement<TMonoView, TEditor> : VisualElement
        where TMonoView : MonoView 
        where TEditor : MonoViewEditor<TMonoView, TEditor>
    {
        private const string Warning = "It is recommended not to leave unassigned Binders";
        
        private readonly TEditor _editor;
        private readonly VisualElement _unassignedBindersContainer;

        public UnassignedBindersVisualElement(TEditor editor)
        {
            _editor = editor;
            // TODO Aspid.MVVM Unity – Rename Name
            _unassignedBindersContainer = new VisualElement().SetName("UnassignedBindersContainer");
            
            style.display = DisplayStyle.None;
            Add(Build());
        }

        private VisualElement Build()
        {
            return new AspidContainer()
                .AddChild(new AspidTitle("Unassigned Binders"))
                .AddChild(new AspidHelpBox(Warning, HelpBoxMessageType.Warning)
                    .SetMargin(bottom:5))
                .AddChild(_unassignedBindersContainer);
        }

        public void Update()
        {
            var count = 0;
            _unassignedBindersContainer.Clear();
            
            foreach (var unassignedBinder in _editor.UnassignedBinders)
            {
                var field = new ObjectField()
                    .SetValue((Object)unassignedBinder)
                    .SetMargin(count > 0 ? 5 : 0, 0, 0,0);
                
                field.SetEnabled(false);
                field.style.opacity = 1;
                field.Q<VisualElement>(className: "unity-object-field__selector").SetDisplay(DisplayStyle.None);
                
                _unassignedBindersContainer.AddChild(field);
                count++;
            }
            
            style.display = _unassignedBindersContainer.childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None; 
        }
    }
}