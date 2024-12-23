using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ScrollbarCommandBinder : TargetBinder<Scrollbar>, IBinder<IRelayCommand<float>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
        private IRelayCommand<float> _command;
        
        public override bool IsBind => Target is not null;
        
        public ScrollbarCommandBinder(Scrollbar target, bool isBindInteractable = true)
            : base(target)
        {
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<float> command)
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
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Target.value);
        }
    }
    
    [Serializable]
    public class ScrollbarCommandBinder<T> : TargetBinder<Scrollbar>, IBinder<IRelayCommand<float, T>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T _param;
        
        private IRelayCommand<float, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ScrollbarCommandBinder(Scrollbar target, T param, bool isBindInteractable = true)
            : base(target)
        {
            _param = param;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<float, T> command)
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
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float, T> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Target.value, Param);
        }
    }
    
    [Serializable]
    public class ScrollbarCommandBinder<T1, T2> : TargetBinder<Scrollbar>, IBinder<IRelayCommand<float, T1, T2>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        private IRelayCommand<float, T1, T2> _command;
        
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
        
        public ScrollbarCommandBinder(Scrollbar target, T1 param1, T2 param2, bool isBindInteractable = true)
            : base(target)
        {
            _param1 = param1;
            _param2 = param2;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<float, T1, T2> command)
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
        
        private void Execute(float value)
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
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Target.value, Param1, Param2);
        }
    }
    
    [Serializable]
    public class ScrollbarCommandBinder<T1, T2, T3> : TargetBinder<Scrollbar>, IBinder<IRelayCommand<float, T1, T2, T3>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        private IRelayCommand<float, T1, T2, T3> _command;
        
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
        
        public ScrollbarCommandBinder(Scrollbar target, T1 param1, T2 param2, T3 param3, bool isBindInteractable = true)
            : base(target)
        {
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _isBindInteractable = isBindInteractable;
        }
        
        public void SetValue(IRelayCommand<float, T1, T2, T3> command)
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
        
        private void Execute(float value)
        {
            OnCanExecuteChanged(_command);
            _command?.Execute(value, Param1, Param2, Param3);
        }

        protected override void OnUnbound() => ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2, T3> command)
        {
            if (!_isBindInteractable) return;
            Target.interactable = command.CanExecute(Target.value, Param1, Param2, Param3);
        }
    }
}