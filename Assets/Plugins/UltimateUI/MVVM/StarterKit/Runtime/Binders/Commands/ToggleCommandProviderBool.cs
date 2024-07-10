using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed class ToggleCommandProviderBool : Binder, IBinder<IRelayCommand<bool>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        private IRelayCommand<bool> _command;
        
        public void SetValue(IRelayCommand<bool> command)
        {
            ReleaseBinding<IRelayCommand<bool>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        { 
            _toggle.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _toggle.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute(bool isOn)
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(isOn);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn);
        }
    }
    
    [Serializable]
    public sealed class ToggleCommandProviderBool<T1> : Binder, IBinder<IRelayCommand<bool, T1>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        
        private IRelayCommand<bool, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public void SetValue(IRelayCommand<bool, T1> command)
        {
            ReleaseBinding<IRelayCommand<bool,T1>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        { 
            _toggle.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _toggle.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute(bool isOn)
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(isOn, Parameter1);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Parameter1);
        }
    }
    
    [Serializable]
    public sealed class ToggleCommandProviderBool<T1, T2> : Binder, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;

        private IRelayCommand<bool, T1, T2> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public T2 Parameter2
        {
            get => _parameter2;
            set => _parameter2 = value;
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2> command)
        {
            ReleaseBinding<IRelayCommand<bool, T1, T2>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        { 
            _toggle.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _toggle.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute(bool isOn)
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(isOn, Parameter1, Parameter2);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Parameter1, Parameter2);
        }
    }
    
    [Serializable]
    public sealed class ToggleCommandProviderBool<T1, T2, T3> : Binder, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;

        private IRelayCommand<bool, T1, T2, T3> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public T2 Parameter2
        {
            get => _parameter2;
            set => _parameter2 = value;
        }
        
        public T3 Parameter3
        {
            get => _parameter3;
            set => _parameter3 = value;
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2, T3> command)
        {
            ReleaseBinding<IRelayCommand<bool, T1, T2, T3>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        { 
            _toggle.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _toggle.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute(bool isOn)
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(isOn, Parameter1, Parameter2, Parameter3);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Parameter1, Parameter2, Parameter3);
        }
    }
}