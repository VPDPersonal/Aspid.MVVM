#nullable enable
using System;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class GenericTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly Action<T?> _setValue;
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;

        public GenericTwoWayBinder(
            Action<Action<T>> initialize, 
            Action<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : this(setValue, onBoundValueChanged, onUnboundValueChanged)
        {
            initialize.Invoke(OnValueChanged);
        }
        
        public GenericTwoWayBinder(
            Action<T?> setValue,
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public void SetValue(T? value) =>
            _setValue.Invoke(value);

        protected override void OnBound(IViewModel viewModel, string id)
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
    
    public class GenericTwoWayBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        public event Action<T?>? ValueChanged;
        
        private readonly TTarget _target;
        private readonly Action<TTarget, T?> _setValue;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        public GenericTwoWayBinder(
            TTarget target,
            Action<TTarget, Action<T>> initialize, 
            Action<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            
            initialize.Invoke(target, OnValueChanged);
        }
        
        public GenericTwoWayBinder(
            TTarget target,
            Action<TTarget, T?> setValue,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
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

        protected override void OnBound(IViewModel viewModel, string id)
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