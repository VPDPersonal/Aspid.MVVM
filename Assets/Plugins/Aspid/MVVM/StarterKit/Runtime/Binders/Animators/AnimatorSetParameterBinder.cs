#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Commands;
using Aspid.MVVM.ViewModels;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public abstract class AnimatorSetParameterBinder<T> : Binder, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>>? ValueChanged;
        
        [field: Header("Component")]
        [field: SerializeField]
        protected Animator Animator { get; private set; }
        
        [field: Header("Parameters")]
        [field: SerializeField]
        public string ParameterName { get; private set; }
        
        protected IRelayCommand<T>? Command { get; private set; }
        
        protected AnimatorSetParameterBinder(Animator animator, string parameterName)
        {
            Animator = animator ?? throw new ArgumentNullException(nameof(animator));
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }
        
        public void NotifyCanExecuteChanged() =>
            Command?.NotifyCanExecuteChanged();

        public void SetValue(T? value)
        {
            if (!CanExecute(value)) return;
            SetParameter(value);
        }
        
        protected abstract void SetParameter(T? value);
        
        protected override void OnBound(IViewModel viewModel, string id)
        {
            Command = new RelayCommand<T>(SetParameter, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;
        
        protected virtual bool CanExecute(T? value) => 
            Animator.gameObject.activeInHierarchy;
    }
}