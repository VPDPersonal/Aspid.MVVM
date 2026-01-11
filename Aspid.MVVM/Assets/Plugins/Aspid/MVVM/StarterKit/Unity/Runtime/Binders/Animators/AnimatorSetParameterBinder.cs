#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>?>? ValueChanged;
        
        private IRelayCommand<T>? _command;
        
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
        
        protected virtual bool CanExecute(T? value) => 
            Target.gameObject.activeInHierarchy;
    }
}