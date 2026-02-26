#if !ASPID_MVVM_EDITOR_DISABLED
#nullable enable
using System;
using System.Linq;
using UnityEditor;
using Aspid.UnityFastTools;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// UIElements visual element that displays a warning listing any <see cref="MonoBinder"/> slots
    /// in a <see cref="MonoView"/> that have not been assigned.
    /// </summary>
    /// <typeparam name="TMonoView">The concrete <see cref="MonoView"/> type being inspected.</typeparam>
    /// <typeparam name="TEditor">The corresponding <see cref="MonoViewEditor{TMonoView,TEditor}"/> type.</typeparam>
    public sealed class UnassignedBindersVisualElement<TMonoView, TEditor> : VisualElement
        where TMonoView : MonoView
        where TEditor : MonoViewEditor<TMonoView, TEditor>
    {
        private const string Warning = "It is recommended not to leave unassigned Binders";

        private readonly TEditor _editor;
        private readonly VisualElement _unassignedBindersContainer;
        private readonly Action<IMonoBinderValidable>? _onBinderClicked;

        private IMonoBinderValidable[]? LastBinders;
        
        public UnassignedBindersVisualElement(TEditor editor, Action<IMonoBinderValidable>? onBinderClicked = null)
        {
            _editor = editor;
            _onBinderClicked = onBinderClicked;
            
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
            var binders = _editor.UnassignedBinders.ToArray();
            if (binders.Length == LastBinders?.Length)
            {
                if (LastBinders.SequenceEqual(binders))
                {
                    return;
                }
            }

            LastBinders = binders;
            var count = binders.Length;
            _unassignedBindersContainer.Clear();
            
            foreach (var unassignedBinder in LastBinders)
            {
                var field = new ObjectField()
                    .SetOpacity(1)
                    .SetValue((Object)unassignedBinder)
                    .SetMargin(top: count > 0 ? 5 : 0, bottom: 0, left: 0, right: 0);

                // Disable the object field's dropdown
                field.Q<VisualElement>(className: "unity-object-field__selector")
                    .SetDisplay(DisplayStyle.None);
                
                var isDraggingBinder = false;
                
                field.RegisterCallback<MouseDownEvent>(evt =>
                {
                    if (evt.button is not 0) return;
                    
                    _onBinderClicked?.Invoke(unassignedBinder);
                    isDraggingBinder = false;
                }, TrickleDown.TrickleDown);

                field.RegisterCallback<MouseMoveEvent>(evt =>
                {
                    if (evt.pressedButtons is not 1 || isDraggingBinder) return;

                    isDraggingBinder = true;
                    DragAndDrop.PrepareStartDrag();
                    DragAndDrop.objectReferences = new[] { (Object)unassignedBinder };
                    DragAndDrop.StartDrag(((Object)unassignedBinder).name);
                    evt.StopPropagation();
                }, TrickleDown.TrickleDown);

                _unassignedBindersContainer.AddChild(field);
                count++;
            }

            style.SetDisplay(_unassignedBindersContainer.childCount > 0 ? DisplayStyle.Flex : DisplayStyle.None);
        }
    }
}
#endif
