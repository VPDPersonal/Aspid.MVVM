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
    /// <see cref="VisualElement"/> for a View binder slot that accepts dragged binders.
    /// </summary>
    public class MonoBinderPropertyField : VisualElement
    {
        private static readonly StyleSheet _styleSheet = Resources.Load<StyleSheet>("Styles/Aspid-MVVM-MonoBinderPropertyField");

        private readonly string _binderId;
        private readonly BindMode _memberMode;
        private readonly string? _assemblyQualifiedName;
        private readonly MonoBinderHighlightGradient _highlightGradient;

        /// <summary>
        /// Gets the serialized View field edited by this row.
        /// </summary>
        public SerializedProperty Property { get; }
        
        /// <summary>
        /// Creates a binder slot row accepting both binding directions.
        /// </summary>
        /// <param name="property">The binder slot property.</param>
        /// <param name="binderId">The slot binding identifier.</param>
        /// <param name="assemblyQualifiedName">The bound value type name; <see langword="null"/> requires untyped binder channels.</param>
        public MonoBinderPropertyField(
            SerializedProperty property, 
            string binderId,
            string? assemblyQualifiedName)
            : this(property, label: string.Empty, binderId, assemblyQualifiedName) { }

        /// <summary>
        /// Creates a binder slot row accepting both binding directions.
        /// </summary>
        /// <param name="property">The binder slot property.</param>
        /// <param name="label">The row label; blank uses the property display name.</param>
        /// <param name="binderId">The slot binding identifier.</param>
        /// <param name="assemblyQualifiedName">The bound value type name; <see langword="null"/> requires untyped binder channels.</param>
        public MonoBinderPropertyField(SerializedProperty property, string label, string binderId, string? assemblyQualifiedName)
            : this(property, label, binderId, assemblyQualifiedName, BindMode.TwoWay) { }

        /// <summary>
        /// Creates a binder slot row with the specified binding mode.
        /// </summary>
        /// <param name="property">The binder slot property.</param>
        /// <param name="label">The row label; blank uses the property display name.</param>
        /// <param name="binderId">The slot binding identifier.</param>
        /// <param name="assemblyQualifiedName">The bound value type name; <see langword="null"/> requires untyped binder channels.</param>
        /// <param name="memberMode">The binding mode exposed by the ViewModel member.</param>
        public MonoBinderPropertyField(SerializedProperty property, string label, string binderId, string? assemblyQualifiedName, BindMode memberMode)
        {
            styleSheets.Add(_styleSheet);

            Property = property;

            _binderId = binderId;
            _memberMode = memberMode;
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
        /// Flashes the row to identify a drop target.
        /// </summary>
        /// <param name="color">The highlight color.</param>
        public void AnimateHighlight(Color color) =>
            _highlightGradient.AnimateHighlight(color);
        
        /// <summary>
        /// Rates the binder by its supported value type, binding mode and previous identifier.
        /// </summary>
        /// <param name="binder">The binder being dragged.</param>
        /// <returns>The compatibility rating.</returns>
        public CompatibleBinderWithField IsCompatibleBinderWithField(IMonoBinderValidatable binder)
        {
            var previousId = binder.PreviousId.Id ?? string.Empty;
            if (previousId.Contains("DesignViewModel."))
                previousId = previousId[16..];

            var propertyType = string.IsNullOrEmpty(_assemblyQualifiedName) ? null : Type.GetType(_assemblyQualifiedName);
            if (!BinderCompatibility.Matches((IBinder)binder, propertyType, _memberMode))
                return CompatibleBinderWithField.None;

            return previousId == _binderId
                ? CompatibleBinderWithField.TypeAndId
                : CompatibleBinderWithField.Type;
        }
    }
}