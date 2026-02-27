#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.UnityFastTools;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoBinderPropertyField : VisualElement
    {
        private const string DropHighlightStyle = "mono-binder-property__drop-highlight";
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Styles/aspid-mvvm-mono-binder-property-field");
        
        private IVisualElementScheduledItem? _highlightedAnimation;
        
        public MonoBinderPropertyField(SerializedProperty property, string assemblyQualifiedName)
            : this(property, label: string.Empty, assemblyQualifiedName) { }

        public MonoBinderPropertyField(SerializedProperty property, string label, string assemblyQualifiedName)
        {
            styleSheets.Add(_styleSheet);

            var slotWrapper = string.IsNullOrWhiteSpace(label)
                ? new AspidPropertyField(property)
                : new AspidPropertyField(property, label);
            
            Add(slotWrapper);
            
            RegisterCallback<DragUpdatedEvent>(evt =>
            {
                var hasCompatibleBinder = DragAndDrop.objectReferences
                    .OfType<IMonoBinderValidable>()
                    .Any(b => IsCompatibleBinderWithField(b, assemblyQualifiedName));

                if (!hasCompatibleBinder)
                {
                    slotWrapper.RemoveFromClassList(DropHighlightStyle);
                    return;
                }

                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
                slotWrapper.AddToClassList(DropHighlightStyle);

                evt.StopPropagation();
            });

            RegisterCallback<DragLeaveEvent>(_ => slotWrapper.RemoveFromClassList(DropHighlightStyle));
            RegisterCallback<DragExitedEvent>(_ => slotWrapper.RemoveFromClassList(DropHighlightStyle));

            RegisterCallback<DragPerformEvent>(evt =>
            {
                var compatibleBinders = DragAndDrop.objectReferences
                    .OfType<IMonoBinderValidable>()
                    .Where(b => IsCompatibleBinderWithField(b, assemblyQualifiedName))
                    .ToArray();

                if (compatibleBinders.Length is 0) return;

                // Stop propagation before processing so that child elements (e.g. the inner
                // PropertyField/ObjectField) do not also handle these drop and duplicate items.
                evt.StopPropagation();
                DragAndDrop.AcceptDrag();
                slotWrapper.RemoveFromClassList(DropHighlightStyle);

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
            }, TrickleDown.TrickleDown);
        }
        
        public void AnimateHighlight()
        {
            const int totalSteps = 50;
            _highlightedAnimation?.Pause();
            _highlightedAnimation = null;
            
            var element = this[0][0];
            element.style.backgroundColor = new StyleColor(StyleKeyword.Null);
            
            var step = 0;
            var initialColor = element.resolvedStyle.backgroundColor;

            IVisualElementScheduledItem? scheduledItem = null;
            scheduledItem = element.schedule.Execute(() =>
            {
                step++;
                if (step >= totalSteps)
                {
                    element.style.backgroundColor = new StyleColor(StyleKeyword.Null);
                    scheduledItem?.Pause();
                    return;
                }

                var time = 1f - (float)step / totalSteps;
                element.style.backgroundColor = Color.Lerp(initialColor, new Color(1f, 0.72f, 0.26f, 1f), time);
            }).Every(16);

            _highlightedAnimation = scheduledItem;
        }
        
        public static bool IsCompatibleBinderWithField(IMonoBinderValidable binder, string? assemblyQualifiedName)
        {
            var binderType = ((Component)binder).GetType();

            if (typeof(IAnyBinder).IsAssignableFrom(binderType)) return true;
            if (string.IsNullOrEmpty(assemblyQualifiedName)) return false;

            var propertyType = Type.GetType(assemblyQualifiedName);
            if (propertyType is null) return false;

            return binderType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBinder<>))
                .Select(i => i.GetGenericArguments()[0])
                .Any(binderTypeArg =>
                    binderTypeArg.IsAssignableFrom(propertyType) ||
                    propertyType.IsAssignableFrom(binderTypeArg));
        }
    }
}