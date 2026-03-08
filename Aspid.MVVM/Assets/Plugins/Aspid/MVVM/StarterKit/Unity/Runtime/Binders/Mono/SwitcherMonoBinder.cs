using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder that switches a target value between two serialized options
    /// based on a bound boolean ViewModel property.
    /// </summary>
    public abstract partial class SwitcherMonoBinder<T> : MonoBinder, IBinder<bool>
    {
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        protected abstract void SetValue(T value);
    }

    /// <summary>
    /// Abstract base switcher binder that sets a property on a specific Unity <typeparamref name="TComponent"/>,
    /// choosing between the serialized <c>_trueValue</c> and <c>_falseValue</c> based on the bound boolean.
    /// </summary>
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

    /// <summary>
    /// Abstract base switcher binder that sets a property on a specific Unity <typeparamref name="TComponent"/>,
    /// choosing between <c>_trueValue</c> and <c>_falseValue</c> and optionally converting the result
    /// via a serialized <typeparamref name="TConverter"/> before applying it.
    /// </summary>
    public abstract partial class SwitcherMonoBinder<TComponent, T, TConverter> : ComponentMonoBinder<TComponent>, IBinder<bool>
        where TComponent : Component
        where TConverter : IConverter<T, T>
    {
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter _converter;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        protected abstract void SetValue(T value);
        
        protected virtual T GetConvertedValue(T value) =>
            _converter is null ? value : _converter.Convert(value);
    }
}