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
        private readonly SerializedProperty _property;
        private readonly string _assemblyQualifiedName;

        public MonoBinderDragHandler(
            VisualElement eventTarget,
            VisualElement highlightTarget,
            SerializedProperty property,
            string assemblyQualifiedName)
        {
            _highlightTarget = highlightTarget;
            _property = property;
            _assemblyQualifiedName = assemblyQualifiedName;

            eventTarget.RegisterCallback<DragUpdatedEvent>(OnDragUpdated);
            eventTarget.RegisterCallback<DragLeaveEvent>(_ => RemoveHighlight());
            eventTarget.RegisterCallback<DragExitedEvent>(_ => RemoveHighlight());
            eventTarget.RegisterCallback<DragPerformEvent>(OnDragPerformed, TrickleDown.TrickleDown);
        }

        private void OnDragUpdated(DragUpdatedEvent evt)
        {
            var hasCompatibleBinder = DragAndDrop.objectReferences
                .OfType<IMonoBinderValidable>()
                .Any(b => MonoBinderPropertyField.IsCompatibleBinderWithField(b, _assemblyQualifiedName));

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
                .Where(b => MonoBinderPropertyField.IsCompatibleBinderWithField(b, _assemblyQualifiedName))
                .ToArray();

            if (compatibleBinders.Length is 0) return;

            // Stop propagation before processing so that child elements (e.g. the inner
            // PropertyField/ObjectField) do not also handle these drop and duplicate items.
            evt.StopPropagation();
            DragAndDrop.AcceptDrag();
            RemoveHighlight();

            if (_property.isArray)
            {
                var startIndex = _property.arraySize;
                _property.arraySize += compatibleBinders.Length;

                for (var i = 0; i < compatibleBinders.Length; i++)
                    _property.GetArrayElementAtIndex(startIndex + i).objectReferenceValue = (Object)compatibleBinders[i];

                _property.ApplyModifiedProperties();
            }
            else
            {
                _property.objectReferenceValue = (Object)compatibleBinders[0];
            }
        }

        private void RemoveHighlight() => _highlightTarget.RemoveFromClassList(DropHighlightStyle);
    }
}
