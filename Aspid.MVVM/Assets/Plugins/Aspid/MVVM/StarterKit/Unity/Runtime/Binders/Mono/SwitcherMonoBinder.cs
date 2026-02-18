using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract partial class SwitcherMonoBinder<T> : MonoBinder, IBinder<bool> 
    {
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        protected abstract void SetValue(T value);
    }
    
    public abstract partial class SwitcherMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool> 
        where TComponent : Component
    {
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        protected abstract void SetValue(T value);
    }
    
    public abstract partial class SwitcherMonoBinder<TComponent, T, TConverter> : ComponentMonoBinder<TComponent>, IBinder<bool> 
        where TComponent : Component
        where TConverter : IConverter<T, T>
    {
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _converter;

        [BinderLog]
        public void SetValue(bool value)
        {
            var chooseValue = value ? _trueValue : _falseValue;
            chooseValue = _converter is null ? chooseValue : _converter.Convert(chooseValue);
            
            SetValue(chooseValue);
        }

        protected abstract void SetValue(T value);
    }
}