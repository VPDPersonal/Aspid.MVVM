using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Toggle), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Toggle Binder - Command")]
    [AddComponentContextMenu(typeof(Toggle),"Add Toggle Binder/Toggle Binder - Command")]
    public partial class ToggleCommandMonoBinder : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_command is not null) OnCanExecuteChanged(_command);
            else if (_isOnCommand is not null) OnCanExecuteChanged(_isOnCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
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
            SetInteractableMode(command.CanExecute(CachedComponent.isOn));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class ToggleCommandMonoBinder<T> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.isOn, Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class ToggleCommandMonoBinder<T1, T2> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.isOn, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class ToggleCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            SetValue(null);
        }
        
        private void OnValueChanged(bool isOn) =>
            _command?.Execute(isOn, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<bool, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.isOn, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}