using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class SliderCommandBinder : TargetBinder<Slider>, IBinder<IRelayCommand<float>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private IRelayCommand<float> _command;
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, InteractableMode interactableMode = InteractableMode.Interactable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode;
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

        protected override void OnUnbound() =>
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(Target.value);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: Target.gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: Target.interactable = interactable; break;
            }
        }
    }

    [Serializable]
    public class SliderCommandBinder<T> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        [SerializeField] private T _param;
        
        private IRelayCommand<float, T> _command;
        
        public T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, T param, BindMode mode)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, T param, InteractableMode interactableMode = InteractableMode.Interactable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            _interactableMode = interactableMode;
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

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<float, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(Target.value, Param);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: Target.gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: Target.interactable = interactable; break;
            }
        }
    }
    
    [Serializable]
    public class SliderCommandBinder<T1, T2> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T1, T2>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
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
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, BindMode mode)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, InteractableMode interactableMode = InteractableMode.Interactable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _interactableMode = interactableMode;
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

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(Target.value, Param1, Param2);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: Target.gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: Target.interactable = interactable; break;
            }
        }
    }
    
    [Serializable]
    public class SliderCommandBinder<T1, T2, T3> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T1, T2, T3>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
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
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, T3 param3, BindMode mode)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, T3 param3, InteractableMode interactableMode = InteractableMode.Interactable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _interactableMode = interactableMode;
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

        protected override void OnUnbound() => 
            ReleaseCommand();

        private void ReleaseCommand()
        {
            if (_command is not null) Unsubscribe();
            _command = null;
        }
        
        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(Target.value, Param1, Param2, Param3);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: Target.gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: Target.interactable = interactable; break;
            }
        }
    }
}