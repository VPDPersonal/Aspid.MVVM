using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class TargetBinder<TTarget, TProperty> : TargetBinder<TTarget>, IBinder<TProperty>, IReverseBinder<TProperty>
    {
        public event Action<TProperty?>? ValueChanged;
        
        protected abstract TProperty? Property { get; set; }
        
        protected TargetBinder(TTarget target, BindMode mode) 
            : base(target, mode) { }

        public void SetValue(TProperty? value) =>
            Property = GetConvertedValue(value);

        protected override void OnBound()
        {
            if (Mode is  BindMode.OneWayToSource)
                ValueChanged?.Invoke(GetConvertedValue(Property));
        }
        
        protected virtual TProperty? GetConvertedValue(TProperty? value) => value;
    }

    public abstract class TargetBinder<TTarget, TProperty, TConverter> : TargetBinder<TTarget, TProperty>
        where TConverter : IConverter<TProperty?, TProperty?>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter? _converter;
        
        protected TargetBinder(TTarget target, TConverter? converter, BindMode mode) 
            : base(target, mode)
        {
            _converter = converter;
        }

        protected override TProperty? GetConvertedValue(TProperty? value) =>
            _converter is not null ? _converter.Convert(value) : value;
    }
}