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

            var slotWrapper = new AspidPropertyField(property, label);
            Add(slotWrapper);
            
            RegisterCallback<DragUpdatedEvent>(evt =>
            {
                var droppedBinder = DragAndDrop.objectReferences
                    .OfType<IMonoBinderValidable>()
                    .FirstOrDefault();
                
                if (droppedBinder is null || !IsCompatibleBinderWithField(droppedBinder, assemblyQualifiedName))
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
                var droppedBinder = DragAndDrop.objectReferences.OfType<IMonoBinderValidable>().FirstOrDefault();
                if (droppedBinder is not Object unityObjectDropped) return;
                if (!IsCompatibleBinderWithField(droppedBinder, assemblyQualifiedName)) return;
                
                DragAndDrop.AcceptDrag();
                slotWrapper.RemoveFromClassList(DropHighlightStyle);

                if (property.isArray)
                {
                    property.arraySize++;
                    property.GetArrayElementAtIndex(property.arraySize - 1).objectReferenceValue = unityObjectDropped;
                    property.ApplyModifiedProperties();
                }
                else
                {
                    property.objectReferenceValue = unityObjectDropped;
                }
                
                evt.StopPropagation();
            });
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
