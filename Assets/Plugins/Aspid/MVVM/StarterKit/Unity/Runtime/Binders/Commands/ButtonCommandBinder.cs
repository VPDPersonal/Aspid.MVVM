using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ButtonCommandBinder : TargetBinder<Button>, IBinder<IRelayCommand>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand _command;

        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, BindMode mode = BindMode.OneWay)   
            : this(target, InteractableMode.Interactable, mode) { }
        
        public ButtonCommandBinder(Button target, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)   
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ButtonCommandBinder(Button target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)   
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }
        
        private void OnClicked() =>
            _command?.Execute();

        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var isInteractable = command.CanExecute();
            
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T> : TargetBinder<Button>, IBinder<IRelayCommand<T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T> _command;

        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T param,
            ICanExecuteView customInteractable, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ButtonCommandBinder(
            Button target, 
            T param,
            InteractableMode interactableMode, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<T> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }
        
        private void OnClicked() => 
            _command?.Execute(Param);

        private void OnCanExecuteChanged(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var isInteractable = command.CanExecute(Param);
            
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2> _command;

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
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        public ButtonCommandBinder(
            Button target, 
            T1 param1,
            T2 param2,
            ICanExecuteView customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ButtonCommandBinder(
            Button target, 
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
        
        public void SetValue(IRelayCommand<T1, T2> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }

        private void OnClicked() => 
            _command?.Execute(Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var isInteractable = command.CanExecute(Param1, Param2);
            
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3>>
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

        private IRelayCommand<T1, T2, T3> _command;

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
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
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
        
        public void SetValue(IRelayCommand<T1, T2, T3> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }
        
        private void OnClicked() =>
            _command?.Execute(Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var isInteractable = command.CanExecute(Param1, Param2, Param3);
            
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3, T4> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        [SerializeField] private T4 _param4;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<T1, T2, T3, T4> _command;

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
        
        public virtual T4 Param4
        {
            get => _param4;
            set => _param4 = value;
        }
        
        public override bool IsBind => Target is not null;
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, T4 param4, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, param4, InteractableMode.Interactable, mode) { }
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, T4 param4, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, T4 param4, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<T1, T2, T3, T4> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }
        
        private void OnClicked() => 
            _command?.Execute(Param1, Param2, Param3, Param4);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var isInteractable = command.CanExecute(Param1, Param2, Param3, Param4);
            
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
}