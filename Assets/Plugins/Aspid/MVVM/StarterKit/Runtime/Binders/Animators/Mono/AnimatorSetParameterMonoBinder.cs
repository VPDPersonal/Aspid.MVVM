using System;
using UnityEngine;
using UnityEngine.Events;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    public abstract class AnimatorSetParameterMonoBinder<T> : ComponentMonoBinder<Animator>, IBinder<T>, IReverseBinder<IRelayCommand<T>>
    {
        public event Action<IRelayCommand<T>> ValueChanged;
        
        public event UnityAction<T> Setting
        {
            add => _setting.AddListener(value);
            remove => _setting.RemoveListener(value);
        }
        
        public event UnityAction<T> Set
        {
            add => _set.AddListener(value);
            remove => _set .RemoveListener(value);
        }
        
        [Header("Events")]
        [SerializeField] private UnityEvent<T> _setting;
        [SerializeField] private UnityEvent<T> _set;
        
        private T _value;
        private IRelayCommand<T> _command;

        protected virtual void OnEnable()
        {
            SetParameter(_value);
            _command?.NotifyCanExecuteChanged();
        }

        protected virtual void OnDisable() =>
            _command?.NotifyCanExecuteChanged();

        public void SetValue(T value)
        {
            _value = value;
            SetParameterInternal(value);
        }

        private void SetParameterInternal(T value)
        {
            if (!CanExecute(value)) return;
            
            OnParameterSetting(value);
            _setting?.Invoke(value);

            SetParameter(value);

            OnParameterSet(value);
            _set?.Invoke(value);
        }

        protected abstract void SetParameter(T value);
        
        protected virtual void OnParameterSetting(T value) { }
        
        protected virtual void OnParameterSet(T value) { }
        
        protected virtual bool CanExecute(T value) => CachedComponent.gameObject.activeInHierarchy;
    }
}