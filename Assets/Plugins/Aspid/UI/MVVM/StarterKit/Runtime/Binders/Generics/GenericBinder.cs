#nullable enable
using System;

namespace Aspid.UI.MVVM.StarterKit.Binders.Generics
{
    public sealed class GenericBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<T?> _setValue;
        
        public GenericBinder(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value)
        {
            _setValue(value);
        }
    }
    
    public sealed class GenericBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, T?> _setValue;
        
        public GenericBinder(TTarget target, Action<TTarget, T?> setValue)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value)
        {
            _setValue(_target, value);
        }
    }
}