using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Button), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Button Binder - Command")]
    [AddComponentContextMenu(typeof(Button),"Add Button Binder/Button Binder - Command")]
    public sealed partial class ButtonCommandMonoBinder : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _selectableCommand;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            else if (_selectableCommand is not null) OnCanExecuteChanged(_selectableCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _selectableCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<bool>)null);
        }
        
        private void OnCLicked()
        {
            if (_command is not null) _command.Execute();
            else _selectableCommand?.Execute(true);
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute());
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(true));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    public abstract partial class ButtonCommandMonoBinder<T> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T>>
    {
        [SerializeField] private T _param;
        
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
        
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            SetValue(null);
        }
        
        private void OnCLicked() =>
            _command?.Execute(Param);
        
        private void OnCanExecuteChanged(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    public abstract partial class ButtonCommandMonoBinder<T1, T2> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T1, T2>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            SetValue(null);
        }
        
        private void OnCLicked() =>
            _command?.Execute(Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2>  command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T1, T2, T3>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            SetValue(null);
        }
        
        private void OnCLicked() =>
            _command?.Execute(Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3, T4> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        [SerializeField] private T4 _param4;
        
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            SetValue(null);
        }
        
        private void OnCLicked() =>
            _command?.Execute(Param1, Param2, Param3, Param4);
        
        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2, Param3, Param4));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
}