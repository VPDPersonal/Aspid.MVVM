using System;

namespace UltimateUI.MVVM.StarterKit.Binders.Generics
{
    public sealed class GenericBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<T> _setValue;
        
        public GenericBinder(Action<T> setValue)
        {
            _setValue = setValue;
        }

        public void SetValue(T value)
        {
            _setValue?.Invoke(value);
        }
    }
    
    public sealed class GenericBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, T> _setValue;
        
        public GenericBinder(TTarget target, Action<TTarget, T> setValue)
        {
            _target = target;
            _setValue = setValue;
        }

        public void SetValue(T value)
        {
            _setValue?.Invoke(_target, value);
        }
    }
}