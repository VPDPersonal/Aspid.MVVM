using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Header("Parameters")]
        [UnityEngine.SerializeField] 
#endif
        private T _trueValue;
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private T _falseValue;

        protected SwitcherBinder(T trueValue, T falseValue, BindMode mode)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            
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
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Header("Parameters")]
        [UnityEngine.SerializeField] 
#endif
        private T _trueValue; 
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private T _falseValue;

        protected SwitcherBinder(TTarget target, T trueValue, T falseValue, BindMode mode)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
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