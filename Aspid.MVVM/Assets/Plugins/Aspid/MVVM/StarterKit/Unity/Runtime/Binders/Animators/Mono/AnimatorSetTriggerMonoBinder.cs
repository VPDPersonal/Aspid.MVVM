using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    [AddBinderContextMenu(typeof(Animator))]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder – Set Trigger")]
    public class AnimatorSetTriggerMonoBinder : ComponentMonoBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand> ValueChanged;
        
        private IRelayCommand _command;
        
        [field: SerializeField] 
        protected string TriggerName { get; private set; }

        protected virtual void OnEnable() => 
            _command?.NotifyCanExecuteChanged();
        
        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        private void SetTrigger()
        {
            if (!CanExecute()) return;
            CachedComponent.SetTrigger(TriggerName);
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
            CachedComponent.gameObject.activeInHierarchy;
    }
}