#nullable enable
using System;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Unity
{
    public class UnityGenericTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly UnityAction<T?> _setValue;
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;

        public UnityGenericTwoWayBinder(
            UnityAction<UnityAction<T>> initialize, 
            UnityAction<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : this(setValue, onBoundValueChanged, onUnboundValueChanged)
        {
            initialize.Invoke(OnValueChanged);
        }
        
        public UnityGenericTwoWayBinder(
            UnityAction<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(value);

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
    
    public class UnityGenericTwoWayBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        public UnityGenericTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> initialize, 
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            
            initialize.Invoke(target, OnValueChanged);
        }
        
        public UnityGenericTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.TwoWay)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");
            
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);

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