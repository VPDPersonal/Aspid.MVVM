using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Commands;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Commands
{
    [Serializable]
    public sealed class ButtonCommandProvider : Binder, IBinder<IRelayCommand>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;

        private IRelayCommand _command;
        
        public void SetValue(IRelayCommand command)
        {
            ReleaseBinding<IRelayCommand>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute();

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = command.CanExecute();
        }
    }
    
    [Serializable]
    public sealed class ButtonCommandProvider<T1> : Binder, IBinder<IRelayCommand<T1>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter;

        private IRelayCommand<T1> _command;

        public T1 Parameter
        {
            get => _parameter;
            set => _parameter = value;
        }
        
        public void SetValue(IRelayCommand<T1> command)
        {
            ReleaseBinding<IRelayCommand<T1>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute(Parameter);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1> command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = command.CanExecute(Parameter);
        }
    }
    
    [Serializable]
    public sealed class ButtonCommandProvider<T1, T2> : Binder, IBinder<IRelayCommand<T1, T2>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;

        private IRelayCommand<T1, T2> _command;

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
        
        public void SetValue(IRelayCommand<T1, T2> command)
        {
            ReleaseBinding<IRelayCommand<T1, T2>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute(Parameter1, Parameter2);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = command.CanExecute(Parameter1, Parameter2);
        }
    }
    
    [Serializable]
    public sealed class ButtonCommandProvider<T1, T2, T3> : Binder, IBinder<IRelayCommand<T1, T2, T3>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;

        private IRelayCommand<T1, T2, T3> _command;

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
        
        public void SetValue(IRelayCommand<T1, T2, T3> command)
        {
            ReleaseBinding<IRelayCommand<T1, T2, T3>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute(Parameter1, Parameter2, Parameter3);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = command.CanExecute(Parameter1, Parameter2, Parameter3);
        }
    }
    
    [Serializable]
    public sealed class ButtonCommandProvider<T1, T2, T3, T4> : Binder, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;
        
        [Header("Parameters")]
        [SerializeField] private T1 _parameter1;
        [SerializeField] private T2 _parameter2;
        [SerializeField] private T3 _parameter3;
        [SerializeField] private T4 _parameter4;
        
        private IRelayCommand<T1, T2, T3, T4> _command;

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
        
        public T4 Parameter4
        {
            get => _parameter4;
            set => _parameter4 = value;
        }
        
        public void SetValue(IRelayCommand<T1, T2, T3, T4> command)
        {
            ReleaseBinding<IRelayCommand<T1, T2, T3, T4>>();
            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            _button.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            _button.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute(Parameter1, Parameter2, Parameter3, Parameter4);

        protected override void ReleaseBinding<TBinding>()
        {
            if (_command != null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command)
        {
            if (!_isBindInteractable) return;
            _button.interactable = command.CanExecute(Parameter1, Parameter2, Parameter3, Parameter4);
        }
    }
}