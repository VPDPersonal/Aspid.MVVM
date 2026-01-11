#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class AnimatorSetTriggerBinder : TargetBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand?>? ValueChanged;
        
        private IRelayCommand? _command;
        
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
        
        protected override void OnBound()
        {
            if (ValueChanged is not null)
            {
                _command = new RelayCommand(SetTrigger, CanExecute);
                ValueChanged.Invoke(_command);
            }
        }

        protected override void OnUnbinding()
        {
            _command = null;
            ValueChanged?.Invoke(_command);
        }

        protected virtual bool CanExecute() => 
            Target.gameObject.activeInHierarchy;
    }
}