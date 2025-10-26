#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public class UnityGenericOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;
        
        public UnityGenericOneWayToSourceBinder(
            UnityAction<UnityAction<T>> initialize, 
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            initialize.Invoke(OnValueChanged);
            
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }
        
        public UnityGenericOneWayToSourceBinder(
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }
        
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke());
        }

        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke());
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
    
    public class UnityGenericOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        public UnityGenericOneWayToSourceBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> initialize, 
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
            
            initialize.Invoke(target, OnValueChanged);
        }
        
        public UnityGenericOneWayToSourceBinder(
            TTarget target,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }
        
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke(_target));
        }

        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke(_target));
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
}