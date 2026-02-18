#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>,
        IBinder<T>,
        IReverseBinder<Action<T>?>,
        IReverseBinder<IRelayCommand<T>?>
    {
        event Action<Action<T>?>? IReverseBinder<Action<T>?>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }
        
        event Action<IRelayCommand<T>?>? IReverseBinder<IRelayCommand<T>?>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }
        
        private IRelayCommand<T>? _command;
        private Action<Action<T>?>? _reverseAction;
        private Action<IRelayCommand<T>?>? _reverseCommand;
        
        [field: SerializeField]
        protected string ParameterName { get; private set; }
        
        protected AnimatorSetParameterBinder(Animator target, string parameterName, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }
        
        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();

        public void SetValue(T? value)
        {
            if (!CanExecute(value)) return;
            SetParameter(value);
        }
        
        protected abstract void SetParameter(T? value);
        
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
        
        protected virtual bool CanExecute(T? value) => 
            Target.gameObject.activeInHierarchy;
    }
}