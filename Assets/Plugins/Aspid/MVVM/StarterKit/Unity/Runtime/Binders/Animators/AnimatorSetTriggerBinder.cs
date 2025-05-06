#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class AnimatorSetTriggerBinder : TargetBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand>? ValueChanged;
        
        [field: Header("Parameters")]
        [field: SerializeField]
        protected string TriggerName { get; private set; }
        
        protected IRelayCommand? Command { get; private set; }
        
        public AnimatorSetTriggerBinder(Animator target, string triggerName)
            : base(target, BindMode.OneWayToSource)
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
        
        protected override void OnBound()
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