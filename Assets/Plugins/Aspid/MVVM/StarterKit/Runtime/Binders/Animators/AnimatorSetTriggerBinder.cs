#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Commands;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetTriggerBinder : Binder, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand>? ValueChanged;
        
        [field: Header("Component")]
        [field: SerializeField]
        protected Animator Animator { get; private set; }
        
        [field: Header("Parameters")]
        [field: SerializeField]
        public string TriggerName { get; private set; }
        
        protected IRelayCommand? Command { get; private set; }
        
        public AnimatorSetTriggerBinder(Animator animator, string triggerName)
        {
            Animator = animator ?? throw new ArgumentNullException(nameof(animator));
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        public void NotifyCanExecuteChanged() =>
            Command?.NotifyCanExecuteChanged();
        
        private void SetTrigger()
        {
            if (!CanExecute()) return;
            Animator.SetTrigger(TriggerName);
        }
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            Command = new RelayCommand(SetTrigger, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;

        protected virtual bool CanExecute() => 
            Animator.gameObject.activeInHierarchy;
    }
}