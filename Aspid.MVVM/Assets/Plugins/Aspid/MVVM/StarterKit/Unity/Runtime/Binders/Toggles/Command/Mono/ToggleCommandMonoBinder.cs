using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{Toggle}"/> that executes a command each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/> (no value) or <see cref="IRelayCommand{bool}"/> (receiving the isOn state).
    /// </summary>
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/Toggle Binder – Command")]
    public partial class ToggleCommandMonoBinder : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
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

        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{bool}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> so that
        /// every value change executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the toggle's value change event to <c>OnValueChanged</c>, which
        /// dispatches to the first non-null command among all bound command types.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Toggle.onValueChanged"/>
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to each <c>SetValue</c> overload to detach command
        /// references and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Toggle}"/> that executes a command with one additional parameter
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T}"/> (receiving the isOn state and the configured parameter).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the isOn value.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        [SerializeField] private T _param;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<bool, T> _command;
        
        /// <summary>
        /// Gets or sets the additional parameter forwarded alongside the isOn value when the command is executed.
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
        /// Binds an <see cref="IRelayCommand{bool, T}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> so that
        /// every value change executes the bound command with the current isOn value and the configured parameter.
        /// </summary>
        /// <remarks>
        /// The subscription connects the toggle's value change event to <c>OnValueChanged</c>,
        /// which executes the bound command with the isOn state and <see cref="Param"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Toggle.onValueChanged"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <c>SetValue</c> to detach the command reference
        /// and unsubscribe from its <c>CanExecuteChanged</c> event.
        /// </remarks>
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Toggle}"/> that executes a command with two additional parameters
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T1, T2}"/> (receiving the isOn state and the configured parameters).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the isOn value.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T1, T2> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<bool, T1, T2> _command;
        
        /// <summary>
        /// Gets or sets the first additional parameter forwarded alongside the isOn value when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter forwarded alongside the isOn value when the command is executed.
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
        /// Binds an <see cref="IRelayCommand{bool, T1, T2}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> so that
        /// every value change executes the bound command with the current isOn value and the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the toggle's value change event to <c>OnValueChanged</c>,
        /// which executes the bound command with the isOn state, <see cref="Param1"/>, and <see cref="Param2"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Toggle.onValueChanged"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <c>SetValue</c> to detach the command reference
        /// and unsubscribe from its <c>CanExecuteChanged</c> event.
        /// </remarks>
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
    
    /// <summary>
    /// <see cref="ComponentMonoBinder{Toggle}"/> that executes a command with three additional parameters
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T1, T2, T3}"/> (receiving the isOn state and the configured parameters).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded alongside the isOn value.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<bool, T1, T2, T3> _command;
        
        /// <summary>
        /// Gets or sets the first additional parameter forwarded alongside the isOn value when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        /// <summary>
        /// Gets or sets the second additional parameter forwarded alongside the isOn value when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        /// <summary>
        /// Gets or sets the third additional parameter forwarded alongside the isOn value when the command is executed.
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
        /// Binds an <see cref="IRelayCommand{bool, T1, T2, T3}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Toggle.onValueChanged"/> so that
        /// every value change executes the bound command with the current isOn value and the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the toggle's value change event to <c>OnValueChanged</c>,
        /// which executes the bound command with the isOn state, <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </remarks>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Toggle.onValueChanged"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <c>SetValue</c> to detach the command reference
        /// and unsubscribe from its <c>CanExecuteChanged</c> event.
        /// </remarks>
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