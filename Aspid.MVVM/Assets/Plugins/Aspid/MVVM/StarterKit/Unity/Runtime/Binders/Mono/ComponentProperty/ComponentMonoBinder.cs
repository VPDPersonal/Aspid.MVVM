using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder that binds a single property on a Unity <see cref="UnityEngine.Component"/>
    /// using get/set accessors. Supports <see cref="BindMode.OneWayToSource"/>: when binding is established
    /// the current property value is sent back to the ViewModel.
    /// </summary>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class ComponentMonoBinder<TComponent, TProperty> : ComponentMonoBinder<TComponent>, IBinder<TProperty>, IReverseBinder<TProperty>
        where TComponent : Component
    {
        public event Action<TProperty> ValueChanged;
        
        protected abstract TProperty Property { get; set; }

        [BinderLog]
        public void SetValue(TProperty value) =>
            Property = GetConvertedValue(value);

        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Property));
        }

        protected virtual TProperty GetConvertedValue(TProperty value) => value;
    }

    /// <summary>
    /// Abstract base MonoBehaviour binder that binds a single property on a Unity <see cref="UnityEngine.Component"/>
    /// using get/set accessors, with value conversion and one-way-to-source support.
    /// </summary>
    public abstract class ComponentMonoBinder<TComponent, TProperty, TConverter> : ComponentMonoBinder<TComponent, TProperty>
        where TComponent : Component
        where TConverter : IConverter<TProperty, TProperty>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _converter;

        protected override TProperty GetConvertedValue(TProperty value) =>
            _converter is not null ? _converter.Convert(value) : value;
    }
}