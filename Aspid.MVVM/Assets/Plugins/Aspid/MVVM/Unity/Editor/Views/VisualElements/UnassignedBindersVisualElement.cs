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
                    .SetFontSize(14))
                .AddChild(_unassignedBindersContainer
                    .SetMargin(top: 10));
        }

        public void Update()
        {
            _unassignedBindersContainer.Clear();
            
            foreach (var unassignedBinder in _editor.UnassignedBinders)
            {
                var field = new ObjectField().SetValue((Object)unassignedBinder);
                field.SetEnabled(false);
                
                _unassignedBindersContainer.AddChild(field);
            }
            
            style.display = _unassignedBindersContainer.childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None; 
        }
    }
}