using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Button}"/> that executes a command each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Button-Command-1.1.0.xml" path="doc//member[@name='ButtonCommandBinder']/*" />
    [Serializable]
    public sealed class ButtonCommandBinder : TargetBinder<Button>, IBinder<IRelayCommand>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Controls how the button's interactable state reflects the bound command's CanExecute result.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [Tooltip("A custom view that reflects the bound command's CanExecute state; used when Interactable Mode is set to Custom.")]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand _command;

        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ButtonCommandBinder(Button target, BindMode mode = BindMode.OneWay)   
            : this(target, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <see cref="IRelayCommand.CanExecute()"/> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ButtonCommandBinder(Button target, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)   
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="interactableMode">Controls how the button's interactable state reflects <see cref="IRelayCommand.CanExecute()"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ButtonCommandBinder(Button target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)   
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        public void SetValue(IRelayCommand command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnClicked()"/>,
        /// which executes the bound command.
        /// </remarks>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from <see cref="Button.onClick"/>
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Passes <see langword="null"/> to <see cref="SetValue(IRelayCommand)"/> to detach the command reference
        /// and unsubscribe from its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
            SetValue(command: null);
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
    
    /// <summary>
    /// <see cref="TargetBinder{Button}"/> that executes a command with one additional parameter
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-Button-Command-1.1.0.xml" path="doc//member[@name='ButtonCommandBinderT']/*" />
    [Serializable]
    public class ButtonCommandBinder<T> : TargetBinder<Button>, IBinder<IRelayCommand<T>>
    {
        [Tooltip("The parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T _param;

        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ButtonCommandBinder(Button target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <see cref="IRelayCommand{T}.CanExecute(T)"/> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded when the command is executed.</param>
        /// <param name="interactableMode">Controls how the button's interactable state reflects <see cref="IRelayCommand{T}.CanExecute(T)"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}"/> and subscribes to its <see cref="IRelayCommand{T}.CanExecuteChanged"/> event.
        /// </summary>
        public void SetValue(IRelayCommand<T> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameter.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnClicked()"/>,
        /// which executes the bound command with <see cref="Param"/>.
        /// </remarks>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

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
            Target.onClick.RemoveListener(OnClicked);
            SetValue(command: null);
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
    
    /// <summary>
    /// <see cref="TargetBinder{Button}"/> that executes a command with two additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-Button-Command-1.1.0.xml" path="doc//member[@name='ButtonCommandBinderT1T2']/*" />
    [Serializable]
    public class ButtonCommandBinder<T1, T2> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;

        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <see cref="IRelayCommand{T1,T2}.CanExecute(T1,T2)"/> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="interactableMode">Controls how the button's interactable state reflects <see cref="IRelayCommand{T1,T2}.CanExecute(T1,T2)"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}"/> and subscribes to its <see cref="IRelayCommand{T1,T2}.CanExecuteChanged"/> event.
        /// </summary>
        public void SetValue(IRelayCommand<T1, T2> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnClicked()"/>,
        /// which executes the bound command with <see cref="Param1"/> and <see cref="Param2"/>.
        /// </remarks>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

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
            Target.onClick.RemoveListener(OnClicked);
            SetValue(command: null);
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
    
    /// <summary>
    /// <see cref="TargetBinder{Button}"/> that executes a command with three additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-Button-Command-1.1.0.xml" path="doc//member[@name='ButtonCommandBinderT1T2T3']/*" />
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T3 _param3;

        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2, T3}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <see cref="IRelayCommand{T1,T2,T3}.CanExecute(T1,T2,T3)"/> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="interactableMode">Controls how the button's interactable state reflects <see cref="IRelayCommand{T1,T2,T3}.CanExecute(T1,T2,T3)"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}"/> and subscribes to its <see cref="IRelayCommand{T1,T2,T3}.CanExecuteChanged"/> event.
        /// </summary>
        public void SetValue(IRelayCommand<T1, T2, T3> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnClicked()"/>,
        /// which executes the bound command with <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </remarks>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

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
            Target.onClick.RemoveListener(OnClicked);
            SetValue(command: null);
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
    
    /// <summary>
    /// <see cref="TargetBinder{Button}"/> that executes a command with four additional parameters
    /// each time <see cref="Button.onClick"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T4">The type of the fourth additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-Button-Command-1.1.0.xml" path="doc//member[@name='ButtonCommandBinderT1T2T3T4']/*" />
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3, T4> : TargetBinder<Button>, IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [Tooltip("The first parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T3 _param3;
        [Tooltip("The fourth parameter forwarded to the command when the button is clicked.")]
        [SerializeField] private T4 _param4;

        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ButtonCommandBinder(Button target, T1 param1, T2 param2, T3 param3, T4 param4, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, param4, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2, T3, T4}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="param4">The fourth additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <see cref="IRelayCommand{T1,T2,T3,T4}.CanExecute(T1,T2,T3,T4)"/> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Initializes a new instance of <see cref="ButtonCommandBinder{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Button"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="param4">The fourth additional parameter forwarded when the command is executed.</param>
        /// <param name="interactableMode">Controls how the button's interactable state reflects <see cref="IRelayCommand{T1,T2,T3,T4}.CanExecute(T1,T2,T3,T4)"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and subscribes to its <see cref="IRelayCommand{T1,T2,T3,T4}.CanExecuteChanged"/> event.
        /// </summary>
        public void SetValue(IRelayCommand<T1, T2, T3, T4> command) =>
            CommandBinderExtensions.UpdateCommand(ref _command, command, OnCanExecuteChanged);
        
        /// <summary>
        /// Called when the binder is bound. Subscribes to <see cref="Button.onClick"/> so that
        /// every click executes the bound command with the configured parameters.
        /// </summary>
        /// <remarks>
        /// The subscription connects the button's click event to <see cref="OnClicked()"/>,
        /// which executes the bound command with <see cref="Param1"/>, <see cref="Param2"/>, <see cref="Param3"/>, and <see cref="Param4"/>.
        /// </remarks>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

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
            Target.onClick.RemoveListener(OnClicked);
            SetValue(command: null);
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