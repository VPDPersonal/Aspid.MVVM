using System;
using UnityEngine;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public abstract class AnimatorSetParameterBinder<T> : Binder, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>> ValueChanged;
        
        private IRelayCommand<T> _command;

        [field: SerializeField]
        protected Animator Animator { get; private set; }
        
        protected AnimatorSetParameterBinder(Animator animator)
        {
            Animator = animator;
        }

        public void SetValue(T value)
        {
            if (!CanExecute(value)) return;
            
            OnParameterSetting(value);
            SetParameter(value);
            OnParameterSet(value);
        }

        protected abstract void SetParameter(T value);
        
        protected virtual void OnParameterSetting(T value) { }
        
        protected virtual void OnParameterSet(T value) { }
        
        protected virtual bool CanExecute(T value) => Animator.gameObject.activeInHierarchy;
    }
}