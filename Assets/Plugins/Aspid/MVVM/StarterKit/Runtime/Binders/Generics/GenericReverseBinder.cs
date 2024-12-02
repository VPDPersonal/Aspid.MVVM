#nullable enable
using System;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class GenericReverseBinder<T> : Binder, IBinder<T>, IReverseBinder<T>, IDisposable
    {
        public event Action<T>? ValueChanged;

        private readonly Action<T?>? _setValue;
        private readonly Action<Action<T>> _disposable;

        public GenericReverseBinder(
            Action<Action<T>> initialize,
            Action<Action<T>> disposable,
            Action<T?>? setValue = null)
        {
            if (initialize is null) throw new ArgumentNullException(nameof(initialize));
            
            _setValue = setValue;
            _disposable = disposable ?? throw new ArgumentNullException(nameof(disposable));
            
            initialize.Invoke(OnValueChanged);
        }

        public void SetValue(T? value) =>
            _setValue?.Invoke(value);

        private void OnValueChanged(T value) =>
            ValueChanged?.Invoke(value);

        public void Dispose() => _disposable?.Invoke(OnValueChanged);
    }
    
    public sealed class GenericReverseBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>, IDisposable
    {
        public event Action<T>? ValueChanged;

        private readonly TTarget _target;
        private readonly Action<TTarget, T?>? _setValue;
        private readonly Action<TTarget, Action<T>> _disposable;

        public GenericReverseBinder(
            TTarget target,
            Action<TTarget, Action<T>> initialize,
            Action<TTarget, Action<T>> disposable, 
            Action<TTarget, T?>? setValue = null)
        {
            if (initialize is null) throw new ArgumentNullException(nameof(initialize));
            
            _setValue = setValue;
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _disposable = disposable ?? throw new ArgumentNullException(nameof(disposable));
            
            initialize.Invoke(_target, OnValueChanged);
        }

        public void SetValue(T? value) =>
            _setValue?.Invoke(_target, value);

        private void OnValueChanged(T value) =>
            ValueChanged?.Invoke(value);

        public void Dispose() => _disposable?.Invoke(_target, OnValueChanged);
    }
}