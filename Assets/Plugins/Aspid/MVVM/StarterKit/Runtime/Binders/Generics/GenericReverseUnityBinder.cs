#nullable enable
using System;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class GenericReverseUnityBinder<T> : Binder, IBinder<T>, IReverseBinder<T>, IDisposable
    {
        public event Action<T>? ValueChanged;

        private readonly UnityAction<T?>? _setValue;
        private readonly UnityAction<UnityAction<T>> _disposable;

        public GenericReverseUnityBinder(
            UnityAction<UnityAction<T>> initialize,
            UnityAction<UnityAction<T>> disposable,
            UnityAction<T?>? setValue = null)
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
    
    public sealed class GenericReverseUnityBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>, IDisposable
    {
        public event Action<T>? ValueChanged;

        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?>? _setValue;
        private readonly UnityAction<TTarget, UnityAction<T>> _disposable;

        public GenericReverseUnityBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> initialize,
            UnityAction<TTarget, UnityAction<T>> disposable, 
            UnityAction<TTarget, T?>? setValue = null)
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