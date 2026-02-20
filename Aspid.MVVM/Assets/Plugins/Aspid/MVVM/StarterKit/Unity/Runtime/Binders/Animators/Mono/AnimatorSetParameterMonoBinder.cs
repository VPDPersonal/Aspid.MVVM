using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, 
        IBinder<T>,
        IReverseBinder<Action<T>>,
        IReverseBinder<IRelayCommand<T>>
    {
        event Action<Action<T>> IReverseBinder<Action<T>>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }
        
        event Action<IRelayCommand<T>> IReverseBinder<IRelayCommand<T>>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }
        
        [NonSerialized] private T _value;
        
        private IRelayCommand<T> _command;
        private Action<Action<T>> _reverseAction;
        private Action<IRelayCommand<T>> _reverseCommand;
        
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
        
        protected sealed override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand<T>(SetParameter, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetParameter);
            }
        }

        protected sealed override void OnUnbinding()
        {
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }
        
        protected abstract void SetParameter(T value);

        protected virtual bool CanExecute(T value) => 
            CachedComponent.gameObject.activeInHierarchy;
    }
}