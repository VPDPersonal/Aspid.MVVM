#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class AnimatorSetTriggerBinder : TargetBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand>? ValueChanged;
        
        [field: Header("Parameters")]
        [field: SerializeField]
        public string TriggerName { get; private set; }
        
        protected IRelayCommand? Command { get; private set; }
        
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target)
        {
            TriggerName = triggerName ?? throw new ArgumentNullException(nameof(triggerName));
        }

        public void NotifyCanExecuteChanged() =>
            Command?.NotifyCanExecuteChanged();
        
        private void SetTrigger()
        {
            if (!CanExecute()) return;
            Target.SetTrigger(TriggerName);
        }
        
        protected override void OnBound(in BindParameters parameters)
        {
            Command = new RelayCommand(SetTrigger, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;

        protected virtual bool CanExecute() => 
            Target.gameObject.activeInHierarchy;
    }
}