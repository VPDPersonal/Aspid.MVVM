using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ButtonCommandBinder : TargetBinder<Button>, IBinder<IRelayCommand>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;

        private IRelayCommand _command;

        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, BindMode mode)   
            : this(target, true, mode) { }
        
        public ButtonCommandBinder(Button target, bool isBindInteractable = true, BindMode mode = BindMode.OneWay)   
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            Target.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute();

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute();
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T> : TargetBinder<Button>, IBinder<IRelayCommand<T>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T _param;

        private IRelayCommand<T> _command;

        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, T param, BindMode mode)
            : this(target, param, true, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T param,
            bool isBindInteractable = true, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<T> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            Target.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() => 
            _command?.Execute(Param);

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Param);
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;

        private IRelayCommand<T1, T2> _command;

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
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, BindMode mode)
            : this(target, param1, param2, true, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T1 param1,
            T2 param2,
            bool isBindInteractable = true,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<T1, T2> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            Target.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() => 
            _command?.Execute(Param1, Param2);

        protected override void OnUnbound() =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Param1, Param2);
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;

        private IRelayCommand<T1, T2, T3> _command;

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
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, BindMode mode)
            : this(target, param1, param2, param3, true, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T1 param1,
            T2 param2,
            T3 param3, 
            bool isBindInteractable = true,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<T1, T2, T3> command)
        {
            ReleaseCommand();
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            Target.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() =>
            _command?.Execute(Param1, Param2, Param3);

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Param1, Param2, Param3);
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3, T4> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        [SerializeField] private T4 _param4;
        
        private IRelayCommand<T1, T2, T3, T4> _command;

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
        
        public T4 Param4
        {
            get => _param4;
            set => _param4 = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, T4 param4, BindMode mode)
            : this(target, param1, param2, param3, param4, true, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4, 
            bool isBindInteractable = true,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<T1, T2, T3, T4> command)
        {
            ReleaseCommand();            
            _command = command;
            
            Subscribe();
            OnCanExecuteChanged(command);
        }

        private void Subscribe()
        {
            Target.onClick.AddListener(Execute);
            _command.CanExecuteChanged += OnCanExecuteChanged;
        }

        private void Unsubscribe()
        {
            Target.onClick.RemoveListener(Execute);
            _command.CanExecuteChanged -= OnCanExecuteChanged;
        }

        private void Execute() => 
            _command?.Execute(Param1, Param2, Param3, Param4);

        protected override void OnUnbound() =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Param1, Param2, Param3, Param4);
        }
    }
}