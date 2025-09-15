using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class GenericOneWayBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<T?> _setValue;

        public GenericOneWayBinder(Action<T?> setValue, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(value);
    }
    
    public class GenericOneWayBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, T?> _setValue;

        public GenericOneWayBinder(TTarget target, Action<TTarget, T?> setValue, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);
    }
}