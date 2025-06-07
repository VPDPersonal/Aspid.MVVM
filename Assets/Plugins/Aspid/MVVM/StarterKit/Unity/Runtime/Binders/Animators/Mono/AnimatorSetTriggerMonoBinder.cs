using System;
using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [BindModeOverride(BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/Animator/Animator Binder - Set Trigger")]
    [AddComponentContextMenu(typeof(Animator),"Add Animator Binder/Animator Binder - Set Trigger")]
    public class AnimatorSetTriggerMonoBinder : ComponentMonoBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand> ValueChanged;

        [field: Header("Parameters")]
        [field: SerializeField] 
        protected string TriggerName { get; private set; }
        
        protected IRelayCommand Command { get; private set; }

        protected virtual void OnEnable() => 
            Command?.NotifyCanExecuteChanged();
        
        protected virtual void OnDisable() =>
            Command?.NotifyCanExecuteChanged();

        private void SetTrigger()
        {
            if (!CanExecute()) return;
            CachedComponent.SetTrigger(TriggerName);
        }
        
        protected override void OnBound()
        {
            Command ??= new RelayCommand(SetTrigger, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;
        
        protected virtual bool CanExecute() =>
            CachedComponent.gameObject.activeInHierarchy;
    }
}