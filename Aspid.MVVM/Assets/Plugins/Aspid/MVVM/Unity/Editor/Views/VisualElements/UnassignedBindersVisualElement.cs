#nullable enable
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public sealed class UnassignedBindersVisualElement : VisualElement
    {
        private const string Warning = "It is recommended not to leave unassigned Binders";
        
        private readonly MonoViewEditor _editor;
        private readonly VisualElement _unassignedBindersContainer;

        public UnassignedBindersVisualElement(MonoViewEditor editor)
        {
            _editor = editor;
            _unassignedBindersContainer = new VisualElement().SetName("UnassignedBindersContainer");
            
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
                    .SetMargin(count > 0 ? 2 : 0, 0, 0,0);
                
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