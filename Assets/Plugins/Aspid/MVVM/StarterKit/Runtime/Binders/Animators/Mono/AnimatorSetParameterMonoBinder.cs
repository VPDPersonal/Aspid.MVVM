using System;
using UnityEngine;
using Aspid.MVVM.Mono.Generation;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract partial class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>> ValueChanged;
        
        private T _value;
        
        [field: Header("Parameters")]
        [field: SerializeField] 
        protected string ParameterName { get; private set; }
        
        protected IRelayCommand<T> Command { get; private set; }

        protected virtual void OnEnable()
        {
            SetParameter(_value);
            Command?.NotifyCanExecuteChanged();
        }

        protected virtual void OnDisable() =>
            Command?.NotifyCanExecuteChanged();

        [BinderLog]
        public void SetValue(T value)
        {
            _value = value;
            SetParameterInternal(value);
        }

        private void SetParameterInternal(T value)
        {
            _value = value;
            if (!CanExecute(value)) return;
            
            SetParameter(value);
        }
        
        protected abstract void SetParameter(T value);
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            
            Command ??= new RelayCommand<T>(SetParameter, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;
        
        protected virtual bool CanExecute(T value) => 
            CachedComponent.gameObject.activeInHierarchy;
    }
}