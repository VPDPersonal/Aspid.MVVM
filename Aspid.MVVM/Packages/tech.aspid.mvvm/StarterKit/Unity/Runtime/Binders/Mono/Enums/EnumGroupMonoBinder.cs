using System;
using UnityEngine;
using Aspid.FastTools.Enums;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that maps a bound enum ViewModel value to a group of elements,
    /// calling <see cref="SetSelectedValue"/> for the matching entry and <see cref="SetDefaultValue"/> for all others.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    public abstract partial class EnumGroupMonoBinder<TElement> : MonoBinder, IBinder<Enum>
    {
        [Tooltip("Lookup table mapping each enum value to its corresponding group element.")]
        [SerializeField] private EnumValues<TElement> _enumValues;

        /// <summary>
        /// Iterates all enum entries, calling <see cref="SetSelectedValue"/> for the matching one and <see cref="SetDefaultValue"/> for all others.
        /// </summary>
        /// <remarks>
        /// An entry whose element is unassigned or destroyed is skipped rather than dereferenced, so the rest of the
        /// group still reaches a consistent state; a half-edited table is an ordinary thing to run into.
        /// Entries whose enum key failed to resolve never arrive here — <c>EnumValues</c> filters them out of its
        /// own enumerator.
        /// </remarks>
        /// <param name="value">The bound enum value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value)
        {
            foreach (var enumValue in _enumValues)
            {
                if (IsEmpty(enumValue.Value))
                {
                    ReportEmptyEntry(enumValue.Key);
                    continue;
                }

                if (!_enumValues.Equals(value, enumValue.Key)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

        /// <summary>
        /// Reports whether <paramref name="element"/> cannot be written to.
        /// </summary>
        /// <remarks>
        /// <typeparamref name="TElement"/> is unconstrained, and for a <see cref="UnityEngine.Object"/> a plain
        /// <c>is null</c> misses the destroyed-but-not-null case that Unity's own <c>==</c> reports.
        /// </remarks>
        private static bool IsEmpty(TElement element) =>
            element is UnityEngine.Object unityObject ? !unityObject : element is null;

        /// <summary>
        /// Reports an empty entry of this binder's table.
        /// </summary>
        private void ReportEmptyEntry(Enum key) =>
            this.LogError(
                problem: $"the {key.Describe()} entry of the enum table has no {typeof(TElement).Name} assigned",
                consequence: "The entry is skipped; the rest of the group is still updated.");

        /// <summary>
        /// Applies the default visual state to <paramref name="element"/>. Called for every non-matching entry.
        /// </summary>
        /// <param name="element">The element to reset to its default state.</param>
        protected abstract void SetDefaultValue(TElement element);

        /// <summary>
        /// Applies the selected visual state to <paramref name="element"/>. Called for the entry matching the bound enum value.
        /// </summary>
        /// <param name="element">The element to mark as selected.</param>
        protected abstract void SetSelectedValue(TElement element);
    }

    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement}"/> that applies serialized default and selected
    /// <typeparamref name="TValue"/> instances to group elements via <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    /// <typeparam name="TValue">The type of value applied to each element.</typeparam>
    public abstract class EnumGroupMonoBinder<TElement, TValue> : EnumGroupMonoBinder<TElement>
    {
        [Tooltip("Value applied to non-matching group elements.")]
        [SerializeField] private TValue _defaultValue;

        [Tooltip("Value applied to the matching group element.")]
        [SerializeField] private TValue _selectedValue;

        [Tooltip("Optional converter applied to the default value before it is set.")]
        [SerializeReference] private IConverter<TValue, TValue> _defaultConverter;

        [Tooltip("Optional converter applied to the selected value before it is set.")]
        [SerializeReference] private IConverter<TValue, TValue> _selectedConverter;

        /// <inheritdoc/>
        protected sealed override void SetDefaultValue(TElement element)
        {
            var value = _defaultConverter is null
                ? _defaultValue
                : _defaultConverter.Convert(_defaultValue);

            SetValue(element, value);
        }

        /// <inheritdoc/>
        protected sealed override void SetSelectedValue(TElement element)
        {
            var value = _selectedConverter is null
                ? _selectedValue
                : _selectedConverter.Convert(_selectedValue);

            SetValue(element, value);
        }

        /// <summary>
        /// Applies <paramref name="value"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TElement element, TValue value);
    }
}