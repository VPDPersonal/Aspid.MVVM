using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ToggleCommandBinder : TargetBinder<Toggle>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }
        
        public ToggleCommandBinder(Toggle target, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Custom, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ToggleCommandBinder(Toggle target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<bool>)null);
        }
        
        private void OnValueChanged(bool isOn)
        {
            if (_command is not null) _command.Execute();
            else _isOnCommand?.Execute(isOn);
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute());
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.isOn));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
                
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<bool, T> _command;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        public ToggleCommandBinder(Toggle target, T param, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param = param;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ToggleCommandBinder(
            Toggle target, T param, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;   
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<bool, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.isOn, Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
                
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<bool, T1, T2> _command;
        
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ToggleCommandBinder(
            Toggle target,
            T1 param1, 
            T2 param2,
            InteractableMode interactableMode,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;       
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.isOn, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ToggleCommandBinder<T1, T2, T3> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
                
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<bool, T1, T2, T3> _command;
        
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }
        
        public override bool IsBind => Target is not null;

        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, T3 param3, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ToggleCommandBinder(
            Toggle target,
            T1 param1,
            T2 param2, 
            T3 param3,
            InteractableMode interactableMode, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;         
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<bool, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.isOn, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
}