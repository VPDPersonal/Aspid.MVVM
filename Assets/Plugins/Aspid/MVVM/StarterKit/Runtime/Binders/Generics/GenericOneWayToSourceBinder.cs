using System;

namespace Aspid.MVVM.StarterKit
{
    public class GenericOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;
        
        public GenericOneWayToSourceBinder(
            Action<Action<T>> initialize, 
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            initialize.Invoke(OnValueChanged);
            
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }
        
        public GenericOneWayToSourceBinder(
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            
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
    
    public class GenericOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        public GenericOneWayToSourceBinder(
            TTarget target,
            Action<TTarget, Action<T>> initialize, 
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
            
            initialize.Invoke(target, OnValueChanged);
        }
        
        public GenericOneWayToSourceBinder(
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
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            
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