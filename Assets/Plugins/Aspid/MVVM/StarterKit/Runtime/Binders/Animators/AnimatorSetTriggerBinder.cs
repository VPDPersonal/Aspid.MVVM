using System;
using UnityEngine;
using Aspid.MVVM.Commands;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetTriggerBinder : Binder, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand> ValueChanged;
        
        private IRelayCommand _command;
        
        [field: SerializeField]
        protected Animator Animator { get; private set; }
        
        [field: SerializeField]
        protected string TriggerName { get; private set; }
        
        public AnimatorSetTriggerBinder(Animator animator, string triggerName)
        {
            Animator = animator;
            TriggerName = triggerName;
        }
        
        private void SetTrigger()
        {
            if (!CanExecute()) return;
            
            OnTriggerSetting();
            Animator.SetTrigger(TriggerName);
            OnTriggerSet();
        }
        
        protected virtual void OnTriggerSetting() { }
        
        protected virtual void OnTriggerSet() { }
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            _command ??= new RelayCommand(SetTrigger, CanExecute);
            ValueChanged?.Invoke(_command);
        }
        
        protected virtual bool CanExecute() => Animator.gameObject.activeInHierarchy;
    }
}