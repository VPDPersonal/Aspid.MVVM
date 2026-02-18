#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class AnimatorSetTriggerBinder : TargetBinder<Animator>,
        IReverseBinder<Action?>,
        IReverseBinder<IRelayCommand?>
    {
        event Action<Action?>? IReverseBinder<Action?>.ValueChanged
        {
            add => _reverseAction += value;
            remove => _reverseAction -= value;
        }
        event Action<IRelayCommand?>? IReverseBinder<IRelayCommand?>.ValueChanged
        {
            add => _reverseCommand += value;
            remove => _reverseCommand -= value;
        }
        
        private IRelayCommand? _command;
        private Action<Action?>? _reverseAction;
        private Action<IRelayCommand?>? _reverseCommand;
        
        [field: SerializeField]
        protected string TriggerName { get; private set; }
        
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target, BindMode.OneWayToSource)
        { 
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        public void NotifyCanExecuteChanged() =>
            _command?.NotifyCanExecuteChanged();
        
        private void SetTrigger()
        {
            if (!CanExecute()) return;
            Target.SetTrigger(TriggerName);
        }
        
        protected sealed override void OnBound()
        {
            if (_reverseCommand is not null)
            {
                _command = new RelayCommand(SetTrigger, CanExecute);
                _reverseCommand.Invoke(_command);
            }
            else
            {
                _reverseAction?.Invoke(SetTrigger);
            }
        }

        protected sealed override void OnUnbinding()
        {
            _command = null;
            _reverseAction?.Invoke(null);
            _reverseCommand?.Invoke(null);
        }

        protected virtual bool CanExecute() => 
            Target.gameObject.activeInHierarchy;
    }
}