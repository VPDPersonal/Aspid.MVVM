using TMPro;
using System;
using UnityEngine;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed partial class DropdownCommandProviderInt : Binder, IBinder<IRelayCommand<int>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private IRelayCommand<int> _command;
        
        [BinderLog]
        public void SetValue(IRelayCommand<int> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _dropdown.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _dropdown.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value);
        }
    }
    
    [Serializable]
    public sealed partial class DropdownCommandProviderInt<T1> : Binder, IBinder<IRelayCommand<int, T1>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        
        private IRelayCommand<int, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _dropdown.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _dropdown.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Parameter1);
        }
    }
    
    [Serializable]
    public sealed partial class DropdownCommandProviderInt<T1, T2> : Binder, IBinder<IRelayCommand<int, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;

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
        
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _dropdown.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _dropdown.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1, Parameter2);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Parameter1, Parameter2);
        }
    }
    
    [Serializable]
    public sealed partial class DropdownCommandProviderInt<T1, T2, T3> : Binder, IBinder<IRelayCommand<int, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        
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
        
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _dropdown.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _dropdown.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(int value) 
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Parameter1, Parameter2, Parameter3);
        }

        protected override void OnUnbound(IViewModel viewModel, string id) =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Parameter1, Parameter2, Parameter3);
        }
    }
}