#nullable enable
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Aspid.MVVM.Validation;
using UnityEngine.UIElements;
using Aspid.FastTools.UIElements.Editors.Internal;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// The Inspector row for one binder field on a view: the property itself, plus the drop target that accepts
    /// a binder dragged onto it.
    /// </summary>
    public class MonoBinderPropertyField : VisualElement
    {
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-MonoBinderPropertyField");

        private readonly string _binderId;
        private readonly string? _assemblyQualifiedName;
        private readonly MonoBinderHighlightGradient _highlightGradient;

        /// <summary>
        /// The view field this row edits.
        /// </summary>
        public SerializedProperty Property { get; }
        
        public MonoBinderPropertyField(
            SerializedProperty property, 
            string binderId,
            string? assemblyQualifiedName)
            : this(property, label: string.Empty, binderId, assemblyQualifiedName) { }

        public MonoBinderPropertyField(SerializedProperty property, string label, string binderId, string? assemblyQualifiedName)
        {
            styleSheets.Add(_styleSheet);

            Property = property;

            _binderId = binderId;
            _assemblyQualifiedName = assemblyQualifiedName;

            var slotWrapper = string.IsNullOrWhiteSpace(label)
                ? new AspidPropertyField(property)
                : new AspidPropertyField(property, label);

            Add(slotWrapper);

            _highlightGradient = new MonoBinderHighlightGradient();

            slotWrapper.RegisterCallback<GeometryChangedEvent>(AttachGradientToInnerPanel);
            _ = new MonoBinderDragHandler(field: this, slotWrapper);
            return;

            void AttachGradientToInnerPanel(GeometryChangedEvent _)
            {
                var panelClass = ThemeStyle.LightnessClass;
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

        /// <summary>
        /// Flashes the row in the given colour, to show which field a dropped binder landed in.
        /// </summary>
        /// <param name="color">The colour to flash.</param>
        public void AnimateHighlight(Color color) =>
            _highlightGradient.AnimateHighlight(color);
        
        /// <summary>
        /// Reports how well the given binder matches this field.
        /// </summary>
        /// <param name="binder">The binder being dragged.</param>
        /// <returns>The quality of the match; see <see cref="CompatibleBinderWithField"/>.</returns>
        public CompatibleBinderWithField IsCompatibleBinderWithField(IMonoBinderValidatable binder)
        {
            var binderType = ((Component)binder).GetType();
            
            var previousId = binder.PreviousId.Id ?? string.Empty;
            if (previousId.Contains("DesignViewModel."))
                previousId = previousId[16..];

            if (typeof(IAnyBinder).IsAssignableFrom(binderType))
            {
                return previousId == _binderId 
                    ? CompatibleBinderWithField.TypeAndId
                    : CompatibleBinderWithField.Type;
            }
            
            if (string.IsNullOrEmpty(_assemblyQualifiedName)) return CompatibleBinderWithField.None;

            var propertyType = Type.GetType(_assemblyQualifiedName);
            if (propertyType is null) return CompatibleBinderWithField.None;

            var hasType = binderType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBinder<>))
                .Select(i => i.GetGenericArguments()[0])
                .Any(binderTypeArg =>
                    binderTypeArg.IsAssignableFrom(propertyType) ||
                    propertyType.IsAssignableFrom(binderTypeArg));

            if (!hasType) return CompatibleBinderWithField.None;
            
            return previousId == _binderId
                ? CompatibleBinderWithField.TypeAndId
                : CompatibleBinderWithField.Type;
        }
    }
}