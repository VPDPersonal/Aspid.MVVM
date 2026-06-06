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

        private bool _initialized;

        /// <summary>
        /// Iterates all enum entries, calling <see cref="SetSelectedValue"/> for the matching one and <see cref="SetDefaultValue"/> for all others.
        /// </summary>
        /// <param name="value">The bound enum value received from the ViewModel.</param>
        [BinderLog]
        public void SetValue(Enum value)
        {
            foreach (var enumValue in _enumValues)
            {
                if (enumValue.Key is null)
                    throw new NullReferenceException("Key is null");

                if (!_enumValues.Equals(value, enumValue.Key)) SetDefaultValue(enumValue.Value);
                else SetSelectedValue(enumValue.Value);
            }
        }

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

        /// <inheritdoc/>
        protected sealed override void SetDefaultValue(TElement element) =>
            SetValue(element, _defaultValue);

        /// <inheritdoc/>
        protected sealed override void SetSelectedValue(TElement element) =>
            SetValue(element, _selectedValue);

        /// <summary>
        /// Applies <paramref name="value"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(TElement element, TValue value);
    }

    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement}"/> that applies serialized default and selected
    /// <typeparamref name="TValue"/> instances, optionally converted via <typeparamref name="TConverter"/>,
    /// to group elements via <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    /// <typeparam name="TValue">The type of value applied to each element.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the default and selected values before applying them.</typeparam>
    public abstract class EnumGroupMonoBinder<TElement, TValue, TConverter> : EnumGroupMonoBinder<TElement>
        where TConverter : IConverter<TValue, TValue>
    {
        [Tooltip("Value applied to non-matching group elements.")]
        [SerializeField] private TValue _defaultValue;

        [Tooltip("Value applied to the matching group element.")]
        [SerializeField] private TValue _selectedValue;

        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the default value before it is set.")]
        [SerializeReference] private TConverter _defaultConverter;

        [SerializeReferenceDropdown]
        [Tooltip("Optional converter applied to the selected value before it is set.")]
        [SerializeReference] private TConverter _selectedConverter;

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