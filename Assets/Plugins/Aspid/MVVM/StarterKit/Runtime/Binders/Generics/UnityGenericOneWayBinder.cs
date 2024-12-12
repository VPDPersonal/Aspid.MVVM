#nullable enable
using System;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class UnityGenericOneWayBinder<T> : Binder, IBinder<T>
    {
        private readonly UnityAction<T?> _setValue;

        public UnityGenericOneWayBinder(UnityAction<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(value);
    }
    
    public class UnityGenericOneWayBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;

        public UnityGenericOneWayBinder(TTarget target, UnityAction<TTarget, T?> setValue)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);
    }
}