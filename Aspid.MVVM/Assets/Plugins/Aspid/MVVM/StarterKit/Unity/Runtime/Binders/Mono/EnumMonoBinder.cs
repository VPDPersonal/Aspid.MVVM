using System;
using UnityEngine;
using Aspid.FastTools;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder that maps a bound enum ViewModel value to a concrete typed value
    /// using a configurable <see cref="Aspid.FastTools.EnumValues{T}"/> lookup table.
    /// </summary>
    public abstract partial class EnumMonoBinder<T> : MonoBinder, IBinder<Enum>
    {
        [SerializeField] private EnumValues<T> _enumValues;

        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));

        protected abstract void SetValue(T value);
    }

    /// <summary>
    /// Abstract base enum binder that resolves the bound <see cref="System.Enum"/> to a <typeparamref name="TValue"/>
    /// via <see cref="Aspid.FastTools.EnumValues{T}"/> and applies it to a specific Unity <typeparamref name="TComponent"/>.
    /// </summary>
    public abstract partial class EnumMonoBinder<TComponent, TValue> : ComponentMonoBinder<TComponent>, IBinder<Enum>
        where TComponent : Component
    {
        [SerializeField] private EnumValues<TValue> _enumValues;

        [BinderLog]
        public void SetValue(Enum value) =>
            SetValue(_enumValues.GetValue(value));

        protected abstract void SetValue(TValue value);
    }

    /// <summary>
    /// Abstract base enum binder that resolves the bound <see cref="System.Enum"/> to a <typeparamref name="TValue"/>
    /// and optionally converts it via a serialized <typeparamref name="TConverter"/> before applying it
    /// to a specific Unity <typeparamref name="TComponent"/>.
    /// </summary>
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