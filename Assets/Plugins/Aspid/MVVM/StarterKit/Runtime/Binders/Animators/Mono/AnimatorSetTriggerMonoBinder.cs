using System;
using UnityEngine;
using UnityEngine.Events;
using Aspid.MVVM.Commands;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Animator/Animator Binder - Set Trigger")]
    public class AnimatorSetTriggerMonoBinder : ComponentMonoBinder<Animator>, IReverseBinder<IRelayCommand>
    {
        public event Action<IRelayCommand> ValueChanged;

        [Header("Parameters")]
        [SerializeField] private string _triggerName;
        
        [Header("Events")]
        [SerializeField] private UnityEvent _setting;
        [SerializeField] private UnityEvent _set;
        
        private IRelayCommand _command;
        
        protected string TriggerName => _triggerName;

        protected virtual void OnEnable() => _command?.NotifyCanExecuteChanged();
        
        protected virtual void OnDisable() => _command?.NotifyCanExecuteChanged();

        private void SetTrigger()
        {
            if (!CanExecute()) return;
            
            OnTriggerSetting();
            _setting?.Invoke();
            
            CachedComponent.SetTrigger(TriggerName);

            OnTriggerSet();
            _set?.Invoke();
        }
        
        protected virtual void OnTriggerSetting() { }
        
        protected virtual void OnTriggerSet() { }
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            _command ??= new RelayCommand(SetTrigger, CanExecute);
            ValueChanged?.Invoke(_command);
        }
        
        protected virtual bool CanExecute() => CachedComponent.gameObject.activeInHierarchy;
    }
}