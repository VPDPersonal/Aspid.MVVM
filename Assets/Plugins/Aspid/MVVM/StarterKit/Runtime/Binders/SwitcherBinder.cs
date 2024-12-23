using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue; 
        [SerializeField] private T _falseValue;

        protected SwitcherBinder(T trueValue, T falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        protected abstract void SetValue(T value);
        
        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }
    
    [Serializable]
    public abstract class SwitcherBinder<TTarget, T> : TargetBinder<TTarget>, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private T _trueValue; 
        [SerializeField] private T _falseValue;

        protected SwitcherBinder(TTarget target, T trueValue, T falseValue)
            : base(target)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        protected abstract void SetValue(T value);
        
        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }
}