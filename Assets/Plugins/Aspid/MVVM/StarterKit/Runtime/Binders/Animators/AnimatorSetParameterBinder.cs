#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public abstract class AnimatorSetParameterBinder<T> : TargetBinder<Animator>, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>>? ValueChanged;
        
        [field: Header("Parameters")]
        [field: SerializeField]
        protected string ParameterName { get; private set; }
        
        protected IRelayCommand<T>? Command { get; private set; }
        
        protected AnimatorSetParameterBinder(Animator target, string parameterName, BindMode mode)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
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
        
        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            
            Command = new RelayCommand<T>(SetParameter, CanExecute);
            ValueChanged?.Invoke(Command);
        }
        
        protected override void OnUnbound() => 
            Command = null;
        
        protected virtual bool CanExecute(T? value) => 
            Target.gameObject.activeInHierarchy;
    }
}