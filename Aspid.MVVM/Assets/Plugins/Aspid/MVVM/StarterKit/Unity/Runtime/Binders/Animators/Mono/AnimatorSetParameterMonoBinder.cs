using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>> ValueChanged;
        
        [NonSerialized]
        private T _value;
        
        private IRelayCommand<T> _command;
        
        [field: SerializeField] 
        protected string ParameterName { get; private set; }

        protected virtual void OnEnable()
        {
            SetParameter(_value);
            _command?.NotifyCanExecuteChanged();
        }

        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        [BinderLog]
        public void SetValue(T value)
        {
            _value = value;
            SetParameterInternal(value);
        }

        private void SetParameterInternal(T value)
        {
            _value = value;
            if (!CanExecute(value)) return;
            
            SetParameter(value);
        }
        
        protected override void OnBound()
        {
            if (ValueChanged is not null)
            {
                _command = new RelayCommand<T>(SetParameter, CanExecute);
                ValueChanged.Invoke(_command);
            }
        }

        protected override void OnUnbinding()
        {
            _command = null;
            ValueChanged?.Invoke(_command);
        }
        
        protected abstract void SetParameter(T value);

        protected virtual bool CanExecute(T value) => 
            CachedComponent.gameObject.activeInHierarchy;
    }
}