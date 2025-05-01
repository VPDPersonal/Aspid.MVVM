using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
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
    
    public abstract partial class SwitcherMonoBinder<TComponent, T> : ComponentMonoBinder<TComponent>, IBinder<bool> 
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