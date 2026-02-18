using System;
using UnityEngine;
using Aspid.UnityFastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private EnumValues<T> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(T value);
    }
    
    public abstract partial class EnumMonoBinder<TComponent, TValue> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [SerializeField] private EnumValues<TValue> _enumValues;
        
        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));
        
        protected abstract void SetValue(TValue value);
    }
    
    public abstract partial class EnumMonoBinder<TComponent, TValue, TConverter> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
        where TConverter : IConverter<TValue, TValue>
    {
        [SerializeField] private EnumValues<TValue> _enumValues;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _converter;
        
        [BinderLog]
        public void SetValue(Enum value)
        {
            var enumValue = _converter is null
                ? _enumValues.GetValue(value)
                : _converter.Convert(_enumValues.GetValue(value));
            
            SetValue(enumValue);
        }

        protected abstract void SetValue(TValue value);
    }
}