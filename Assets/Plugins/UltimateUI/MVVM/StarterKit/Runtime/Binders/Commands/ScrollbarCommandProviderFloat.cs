using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Commands
{
    [Serializable]
    public sealed partial class ScrollbarCommandProviderFloat : Binder, IBinder<IRelayCommand<float>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Scrollbar _scrollbar;
        
        private IRelayCommand<float> _command;
        
        [BinderLog]
        public void SetValue(IRelayCommand<float> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _scrollbar.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _scrollbar.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (!_isBindInteractable) return;
            _scrollbar.interactable = command.CanExecute(_scrollbar.value);
        }
    }
    
    [Serializable]
    public sealed partial class ScrollbarCommandProviderFloat<T1> : Binder, IBinder<IRelayCommand<float, T1>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Scrollbar _scrollbar;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        
        private IRelayCommand<float, T1> _command;
        
        public T1 Parameter1
        {
            get => _parameter1;
            set => _parameter1 = value;
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _scrollbar.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _scrollbar.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1> command)
        {
            if (!_isBindInteractable) return;
            _scrollbar.interactable = command.CanExecute(_scrollbar.value, Parameter1);
        }
    }
    
    [Serializable]
    public sealed partial class ScrollbarCommandProviderFloat<T1, T2> : Binder, IBinder<IRelayCommand<float, T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Scrollbar _scrollbar;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        
        private IRelayCommand<float, T1, T2> _command;
        
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
        public void SetValue(IRelayCommand<float, T1, T2> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _scrollbar.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _scrollbar.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _scrollbar.interactable = command.CanExecute(_scrollbar.value, Parameter1, Parameter2);
        }
    }
    
    [Serializable]
    public sealed partial class ScrollbarCommandProviderFloat<T1, T2, T3> : Binder, IBinder<IRelayCommand<float, T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Scrollbar _scrollbar;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        
        private IRelayCommand<float, T1, T2, T3> _command;
        
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
        public void SetValue(IRelayCommand<float, T1, T2, T3> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }
        
        private void Subscribe()
        {
            _scrollbar.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _scrollbar.onValueChanged.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _scrollbar.interactable = command.CanExecute(_scrollbar.value, Parameter1, Parameter2, Parameter3);
        }
    }
}