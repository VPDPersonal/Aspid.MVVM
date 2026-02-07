using System;
using UnityEngine;
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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
}