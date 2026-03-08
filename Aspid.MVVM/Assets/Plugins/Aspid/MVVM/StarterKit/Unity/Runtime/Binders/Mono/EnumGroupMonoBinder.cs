using System;
using UnityEngine;
using Aspid.FastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder that maps a bound enum ViewModel value to a group of elements,
    /// calling <c>SetSelectedValue</c> for the matching entry and <c>SetDefaultValue</c> for all others.
    /// </summary>
    public abstract partial class EnumGroupMonoBinder<TElement> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private EnumValues<TElement> _enumValues;

        private bool _initialized;

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

        protected abstract void SetDefaultValue(TElement element);

        protected abstract void SetSelectedValue(TElement element);
    }

    /// <summary>
    /// Abstract base enum-group binder that uses a single serialized <c>_defaultValue</c> / <c>_selectedValue</c>
    /// pair of type <typeparamref name="TValue"/>, applying them to elements via <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    public abstract class EnumGroupMonoBinder<TElement, TValue> : EnumGroupMonoBinder<TElement>
    {
        [SerializeField] private TValue _defaultValue;
        [SerializeField] private TValue _selectedValue;

        protected sealed override void SetDefaultValue(TElement element) =>
            SetValue(element, _defaultValue);

        protected sealed override void SetSelectedValue(TElement element) =>
            SetValue(element, _selectedValue);

        protected abstract void SetValue(TElement element, TValue value);
    }

    /// <summary>
    /// Abstract base enum-group binder that applies an optional <typeparamref name="TConverter"/> to the default
    /// and selected values before forwarding them to elements via <see cref="SetValue(TElement, TValue)"/>.
    /// </summary>
    public abstract class EnumGroupMonoBinder<TElement, TValue, TConverter> : EnumGroupMonoBinder<TElement>
        where TConverter : IConverter<TValue, TValue>
    {
        [SerializeField] private TValue _defaultValue;
        [SerializeField] private TValue _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _defaultConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _selectedConverter;

        protected sealed override void SetDefaultValue(TElement element)
        {
            var value = _defaultConverter is null
                ? _defaultValue
                : _defaultConverter.Convert(_defaultValue);
            
            SetValue(element, value);
        }

        protected sealed override void SetSelectedValue(TElement element)
        {
            var value = _selectedConverter is null
                ? _selectedValue
                : _selectedConverter.Convert(_selectedValue);
            
            SetValue(element, value);
        }

        protected abstract void SetValue(TElement element, TValue value);
    }
}