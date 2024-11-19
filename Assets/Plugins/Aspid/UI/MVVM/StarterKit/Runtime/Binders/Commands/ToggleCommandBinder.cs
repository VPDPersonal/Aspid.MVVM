using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed class ToggleCommandBinder : Binder, IBinder<IRelayCommand<bool>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Toggle _toggle;
        
        private IRelayCommand<bool> _command;
        
        public override bool IsBind => _toggle is not null;
        
        private ToggleCommandBinder() { }
        
        public ToggleCommandBinder(Toggle toggle, bool isBindInteractable = true)
        {
            _toggle = toggle;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<bool> command)
        {
            ReleaseCommand();            
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

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T> : Binder, IBinder<IRelayCommand<bool, T>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        private IRelayCommand<bool, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => _toggle is not null;
        
        private ToggleCommandBinder() { }
        
        public ToggleCommandBinder(Toggle toggle, T param, bool isBindInteractable = true)
        {
            _param = param;
            _toggle = toggle;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<bool, T> command)
        {
            ReleaseCommand();            
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
            _command?.Execute(isOn, Param);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Param);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2> : Binder, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;

        private IRelayCommand<bool, T1, T2> _command;
        
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
        
        public override bool IsBind => _toggle is not null;
        
        private ToggleCommandBinder() { }
        
        public ToggleCommandBinder(Toggle toggle, T1 param1, T2 param2, bool isBindInteractable = true)
        {
            _param1 = param1;
            _param2 = param2;
            _toggle = toggle;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2> command)
        {
            ReleaseCommand();            
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
            _command?.Execute(isOn, Param1, Param2);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Param1, Param2);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2, T3> : Binder, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;

        private IRelayCommand<bool, T1, T2, T3> _command;
        
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
        
        public override bool IsBind => _toggle is not null;
        
        private ToggleCommandBinder() { }
        
        public ToggleCommandBinder(Toggle toggle, T1 param1, T2 param2, T3 param3, bool isBindInteractable = true)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _toggle = toggle;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2, T3> command)
        {
            ReleaseCommand();            
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
            _command?.Execute(isOn, Param1, Param2, Param3);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _toggle.interactable = command.CanExecute(_toggle.isOn, Param1, Param2, Param3);
        }
    }
}