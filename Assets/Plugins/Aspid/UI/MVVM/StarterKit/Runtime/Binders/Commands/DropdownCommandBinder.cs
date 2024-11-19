using TMPro;
using System;
using UnityEngine;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed class DropdownCommandBinder : Binder, IBinder<IRelayCommand<int>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private IRelayCommand<int> _command;
        
        public override bool IsBind => _dropdown is not null;

        private DropdownCommandBinder() { }
        
        public DropdownCommandBinder(TMP_Dropdown dropdown, bool isBindInteractable = true)
        {
            _dropdown = dropdown;
            _isBindInteractable = isBindInteractable;
        }

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

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value);
        }
    }
    
    [Serializable]
    public class DropdownCommandBinder<T> : Binder, IBinder<IRelayCommand<int, T>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        private IRelayCommand<int, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => _dropdown is not null;
        
        private DropdownCommandBinder() { }
        
        public DropdownCommandBinder(TMP_Dropdown dropdown, T param, bool isBindInteractable = true)
        {
            _param = param;
            _dropdown = dropdown;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<int, T> command)
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
            _command?.Execute(value, Param);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Param);
        }
    }
    
    [Serializable]
    public class DropdownCommandBinder<T1, T2> : Binder, IBinder<IRelayCommand<int, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;

        private IRelayCommand<int, T1, T2> _command;
        
        public T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public override bool IsBind => _dropdown is not null;
        
        private DropdownCommandBinder() { }
        
        public DropdownCommandBinder(TMP_Dropdown dropdown, T1 param1, T2 param2, bool isBindInteractable = true)
        {
            _param1 = param1;
            _param2 = param2;
            _dropdown = dropdown;
            _isBindInteractable = isBindInteractable;
        }
        
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
            _command?.Execute(value, Param1, Param2);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Param1, Param2);
        }
    }
    
    [Serializable]
    public class DropdownCommandBinder<T1, T2, T3> : Binder, IBinder<IRelayCommand<int, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        private IRelayCommand<int, T1, T2, T3> _command;
        
        public T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }
        
        public override bool IsBind => _dropdown is not null;
        
        private DropdownCommandBinder() { }
        
        public DropdownCommandBinder(TMP_Dropdown dropdown, T1 param1, T2 param2, T3 param3, bool isBindInteractable = true)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _dropdown = dropdown;
            _isBindInteractable = isBindInteractable;
        }
        
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
            _command?.Execute(value, Param1, Param2, Param3);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _dropdown.interactable = command.CanExecute(_dropdown.value, Param1, Param2, Param3);
        }
    }
}