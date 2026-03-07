#nullable enable
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;
using Aspid.FastTools.Editors;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal sealed class MonoBinderDragHandler
    {
        private const string DropHighlightStyle = "mono-binder-property__drop-highlight";
        
        private readonly VisualElement _highlightTarget;
        private readonly MonoBinderPropertyField _field; 

        public MonoBinderDragHandler(
            MonoBinderPropertyField field,
            VisualElement highlightTarget)
        {
            _field = field;
            _highlightTarget = highlightTarget;

            field.RegisterCallback<DragUpdatedEvent>(OnDragUpdated);
            field.RegisterCallback<DragLeaveEvent>(_ => RemoveHighlight());
            field.RegisterCallback<DragExitedEvent>(_ => RemoveHighlight());
            field.RegisterCallback<DragPerformEvent>(OnDragPerformed, TrickleDown.TrickleDown);
        }

        private void OnDragUpdated(DragUpdatedEvent evt)
        {
            var hasCompatibleBinder = DragAndDrop.objectReferences
                .OfType<IMonoBinderValidable>()
                .Any(b => _field.IsCompatibleBinderWithField(b));

            if (!hasCompatibleBinder)
            {
                RemoveHighlight();
                return;
            }

            DragAndDrop.visualMode = DragAndDropVisualMode.Link;
            _highlightTarget.AddToClassList(DropHighlightStyle);
            evt.StopPropagation();
        }

        private void OnDragPerformed(DragPerformEvent evt)
        {
            var compatibleBinders = DragAndDrop.objectReferences
                .OfType<IMonoBinderValidable>()
                .Where(b => _field.IsCompatibleBinderWithField(b))
                .ToArray();

            if (compatibleBinders.Length is 0) return;

            // Stop propagation before processing so that child elements (e.g. the inner
            // PropertyField/ObjectField) do not also handle these drop and duplicate items.
            evt.StopPropagation();
            DragAndDrop.AcceptDrag();
            RemoveHighlight();
            
            var property = _field.Property;

            if (property.isArray)
            {
                var startIndex = property.arraySize;
                property.arraySize += compatibleBinders.Length;

                for (var i = 0; i < compatibleBinders.Length; i++)
                    property.GetArrayElementAtIndex(startIndex + i).objectReferenceValue = (Object)compatibleBinders[i];

                property.ApplyModifiedProperties();
            }
            else
            {
                property.objectReferenceValue = (Object)compatibleBinders[0];
            }
        }

        private void RemoveHighlight() => _highlightTarget.RemoveFromClassList(DropHighlightStyle);
    }
}
