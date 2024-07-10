using TMPro;
using System;
using UnityEngine;
using UltimateUI.MVVM.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Commands
{
    [Serializable]
    public sealed class DropdownCommandProviderInt : Binder, IBinder<IRelayCommand<int>>
    {
        [SerializeField] private bool _isBindInteractable;
        
        [Space]
        [SerializeField] private TMP_Dropdown[] _dropdowns;
        
        private IRelayCommand<int> _command;
        
        public void SetValue(IRelayCommand<int> command)
        {
            ReleaseBinding<IRelayCommand<int>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.AddListener(Execute);
            
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.RemoveListener(Execute);
            
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (!_isBindInteractable) return;

            foreach (var dropdown in _dropdowns)
                dropdown.interactable = command.CanExecute(dropdown.value);
        }
    }
    
    [Serializable]
    public sealed class DropdownCommandProviderInt<T1> : Binder, IBinder<IRelayCommand<int, T1>>
    {
        [SerializeField] private bool _isBindInteractable;
        
        [Space]
        [SerializeField] private T1 _parameter1;
        
        [Space]
        [SerializeField] private TMP_Dropdown[] _dropdowns;
        
        private IRelayCommand<int, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        public void SetValue(IRelayCommand<int, T1> command)
        {
            ReleaseBinding<IRelayCommand<int, T1>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.AddListener(Execute);
            
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.RemoveListener(Execute);
            
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1> command)
        {
            if (!_isBindInteractable) return;

            foreach (var dropdown in _dropdowns)
                dropdown.interactable = command.CanExecute(dropdown.value, Parameter1);
        }
    }
    
    [Serializable]
    public sealed class DropdownCommandProviderInt<T1, T2> : Binder, IBinder<IRelayCommand<int, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable;
        
        [Space]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        
        [Space]
        [SerializeField] private TMP_Dropdown[] _dropdowns;
        
        private IRelayCommand<int, T1, T2> _command;
        
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
        
        public void SetValue(IRelayCommand<int, T1, T2> command)
        {
            ReleaseBinding<IRelayCommand<int, T1, T2>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.AddListener(Execute);
            
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.RemoveListener(Execute);
            
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1, Parameter2);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (!_isBindInteractable) return;

            foreach (var dropdown in _dropdowns)
                dropdown.interactable = command.CanExecute(dropdown.value, Parameter1, Parameter2);
        }
    }
    
    [Serializable]
    public sealed class DropdownCommandProviderInt<T1, T2, T3> : Binder, IBinder<IRelayCommand<int, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable;
        
        [Space]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        
        [Space]
        [SerializeField] private TMP_Dropdown[] _dropdowns;
        
        private IRelayCommand<int, T1, T2, T3> _command;
        
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
        
        public void SetValue(IRelayCommand<int, T1, T2, T3> command)
        {
            ReleaseBinding<IRelayCommand<int, T1, T2, T3>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.AddListener(Execute);
            
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            foreach (var dropdown in _dropdowns)
                dropdown.onValueChanged.RemoveListener(Execute);
            
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1, Parameter2, Parameter3);
        }

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;

            foreach (var dropdown in _dropdowns)
                dropdown.interactable = command.CanExecute(dropdown.value, Parameter1, Parameter2, Parameter3);
        }
    }
}