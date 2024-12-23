using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ToggleCommandBinder : TargetBinder<Toggle>, IBinder<IRelayCommand<bool>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
        private IRelayCommand<bool> _command;
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, bool isBindInteractable = true)
            : base(target)
        {
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
            Target.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onValueChanged.RemoveListener(Execute);
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
            Target.interactable = command.CanExecute(Target.isOn);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T _param;
        
        private IRelayCommand<bool, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, T param, bool isBindInteractable = true)
            : base(target)
        {
            _param = param;
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
            Target.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onValueChanged.RemoveListener(Execute);
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
            Target.interactable = command.CanExecute(Target.isOn, Param);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [Header("Parameters")]
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private bool _isBindInteractable = true;
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
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, bool isBindInteractable = true)
            : base(target)
        {
            _param1 = param1;
            _param2 = param2;
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
            Target.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onValueChanged.RemoveListener(Execute);
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
            Target.interactable = command.CanExecute(Target.isOn, Param1, Param2);
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2, T3> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
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
        
        public override bool IsBind => Target is not null;

        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, T3 param3, bool isBindInteractable = true)
            : base(target)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
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
            Target.onValueChanged.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onValueChanged.RemoveListener(Execute);
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
            Target.interactable = command.CanExecute(Target.isOn, Param1, Param2, Param3);
        }
    }
}