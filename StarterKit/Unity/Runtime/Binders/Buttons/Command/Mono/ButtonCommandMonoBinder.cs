using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Button}"/> that executes a command each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/> (no value) or <see cref="IRelayCommand{T}">IRelayCommand&lt;bool&gt;</see>
    /// (receives <see langword="true"/> on click).
    /// </summary>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Button Binder – Command")]
    public sealed partial class ButtonCommandMonoBinder : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<bool> _selectableCommand;

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            else if (_selectableCommand is not null) OnCanExecuteChanged(_selectableCommand);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;bool&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On click, the command receives <see langword="true"/> as its parameter.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _selectableCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnCLicked()"/>,
        /// which dispatches to the first non-null command: the plain <see cref="IRelayCommand"/> is executed
        /// without a parameter; otherwise <see cref="IRelayCommand{T}">IRelayCommand&lt;bool&gt;</see> is executed with <see langword="true"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases both bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to both <see cref="SetValue(IRelayCommand)"/> overloads to detach the command references
        /// and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Button}"/> that executes a command with one additional parameter
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T>>
    {
        [Tooltip("The parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T> _command;
        
        /// <summary>
        /// Gets or sets the additional parameter forwarded when the command is executed.
        /// </summary>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}"/> and subscribes to its <see cref="IRelayCommand{T}.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameter.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnCLicked()"/>,
        /// which executes the bound command with <see cref="Param"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <see cref="SetValue(IRelayCommand{T})"/> to detach the command reference
        /// and unsubscribe from its <see cref="IRelayCommand{T}.CanExecuteChanged"/> event.
        /// </remarks>
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

    /// <summary>
    /// <see cref="ComponentMonoBinder{Button}"/> that executes a command with two additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T1, T2>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2> _command;
        
        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}"/> and subscribes to its <see cref="IRelayCommand{T1,T2}.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnCLicked()"/>,
        /// which executes the bound command with <see cref="Param1"/> and <see cref="Param2"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <see cref="SetValue(IRelayCommand{T1,T2})"/> to detach the command reference
        /// and unsubscribe from its <see cref="IRelayCommand{T1,T2}.CanExecuteChanged"/> event.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnCLicked);
            SetValue(null);
        }

        private void OnCLicked() =>
            _command?.Execute(Param1, Param2);

        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Button}"/> that executes a command with three additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T1, T2, T3>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2, T3> _command;
        
        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        /// <summary>
        /// Gets or sets the third additional parameter forwarded when the command is executed.
        /// </summary>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}"/> and subscribes to its <see cref="IRelayCommand{T1,T2,T3}.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnCLicked()"/>,
        /// which executes the bound command with <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <see cref="SetValue(IRelayCommand{T1,T2,T3})"/> to detach the command reference
        /// and unsubscribe from its <see cref="IRelayCommand{T1,T2,T3}.CanExecuteChanged"/> event.
        /// </remarks>
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Button}"/> that executes a command with four additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T4">The type of the fourth additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3, T4> : ComponentMonoBinder<Button>, 
        IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T3 _param3;
        [Tooltip("The fourth parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T4 _param4;

        [Space]
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2, T3, T4> _command;
        
        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        /// <summary>
        /// Gets or sets the third additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }
        
        /// <summary>
        /// Gets or sets the fourth additional parameter forwarded when the command is executed.
        /// </summary>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and subscribes to its <see cref="IRelayCommand{T1,T2,T3,T4}.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnCLicked()"/>,
        /// which executes the bound command with <see cref="Param1"/>, <see cref="Param2"/>, <see cref="Param3"/>, and <see cref="Param4"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnCLicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <see cref="SetValue(IRelayCommand{T1,T2,T3,T4})"/> to detach the command reference
        /// and unsubscribe from its <see cref="IRelayCommand{T1,T2,T3,T4}.CanExecuteChanged"/> event.
        /// </remarks>
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