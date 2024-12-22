using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class SwitcherMonoBinder<T> : MonoBinder, IBinder<bool> 
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        protected abstract void SetValue(T value);
    }
    
    public abstract class SwitcherMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool> 
        where TComponent : Component
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        protected abstract void SetValue(T value);
    }
}