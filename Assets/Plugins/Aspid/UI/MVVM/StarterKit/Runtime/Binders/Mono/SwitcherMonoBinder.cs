using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    public abstract partial class SwitcherMonoBinder<T> : MonoBinder, IBinder<bool> 
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        protected abstract void SetValue(T value);
        
        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }
    
    public abstract partial class SwitcherMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool> 
        where TComponent : Component
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue;
        [SerializeField] private T _falseValue;

        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        protected abstract void SetValue(T value);
        
        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }
}