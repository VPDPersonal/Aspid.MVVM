using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed partial class ToggleCommandProviderBool : Binder, IBinder<IRelayCommand<bool>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        private IRelayCommand<bool> _command;
        
        public override bool IsBind => _toggle != null;
        
        [BinderLog]
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
    public sealed partial class ToggleCommandProviderBool<T1> : Binder, IBinder<IRelayCommand<bool, T1>>
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
        
        public override bool IsBind => _toggle != null;
        
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1> command)
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
            _command?.Execute(isOn, Parameter1);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
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
    public sealed partial class ToggleCommandProviderBool<T1, T2> : Binder, IBinder<IRelayCommand<bool, T1, T2>>
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
        
        public override bool IsBind => _toggle != null;
        
        [BinderLog]
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
            _command?.Execute(isOn, Parameter1, Parameter2);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
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
    public sealed partial class ToggleCommandProviderBool<T1, T2, T3> : Binder, IBinder<IRelayCommand<bool, T1, T2, T3>>
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
        
        public override bool IsBind => _toggle != null;
        
        [BinderLog]
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
            _command?.Execute(isOn, Parameter1, Parameter2, Parameter3);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
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