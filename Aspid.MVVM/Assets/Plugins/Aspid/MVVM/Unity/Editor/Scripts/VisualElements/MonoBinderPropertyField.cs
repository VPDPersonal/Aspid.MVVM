#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public class MonoBinderPropertyField : VisualElement
    {
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Styles/aspid-mvvm-mono-binder-property-field");

        private readonly MonoBinderHighlightGradient _highlightGradient;

        public MonoBinderPropertyField(SerializedProperty property, string assemblyQualifiedName)
            : this(property, label: string.Empty, assemblyQualifiedName) { }

        public MonoBinderPropertyField(SerializedProperty property, string label, string assemblyQualifiedName)
        {
            styleSheets.Add(_styleSheet);

            var slotWrapper = string.IsNullOrWhiteSpace(label)
                ? new AspidPropertyField(property)
                : new AspidPropertyField(property, label);

            Add(slotWrapper);

            _highlightGradient = new MonoBinderHighlightGradient();

            slotWrapper.RegisterCallback<GeometryChangedEvent>(AttachGradientToInnerPanel);

            _ = new MonoBinderDragHandler(this, slotWrapper, property, assemblyQualifiedName);

            void AttachGradientToInnerPanel(GeometryChangedEvent _)
            {
                var panelClass = AspidContainer.GetStyleClass(AspidContainer.StyleType.Lighter);
                var innerPanel = slotWrapper.Q(className: panelClass);
                if (innerPanel is null) return;

                innerPanel.style.overflow = Overflow.Hidden;

                if (_highlightGradient.parent != innerPanel)
                {
                    _highlightGradient.RemoveFromHierarchy();
                    innerPanel.hierarchy.Add(_highlightGradient);
                }

                slotWrapper.UnregisterCallback<GeometryChangedEvent>(AttachGradientToInnerPanel);
            }
        }

        public void AnimateHighlight() =>
            _highlightGradient.AnimateHighlight();
        
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