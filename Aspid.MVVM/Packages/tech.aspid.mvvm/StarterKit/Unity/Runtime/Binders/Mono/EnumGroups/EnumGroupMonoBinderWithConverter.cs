using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="EnumGroupMonoBinder{TElement}"/> that applies serialized default and selected
    /// <typeparamref name="TValue"/> instances, optionally converted via <see cref="IConverter{TFrom, TTo}"/>,
    /// to group elements via <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of element in the group that receives the selected or default value.</typeparam>
    /// <typeparam name="TValue">The type of value applied to each element.</typeparam>
    public abstract class EnumGroupMonoBinderWithConverter<TElement, TValue> : EnumGroupMonoBinder<TElement>
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
