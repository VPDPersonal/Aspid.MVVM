using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{Toggle}"/> that executes a command each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/> (no value) or <see cref="IRelayCommand{bool}"/> (receiving the isOn state).
    /// </summary>
    /// <include file="XmlExampleDoc-Toggle-Command-1.1.0.xml" path="doc//member[@name='ToggleCommandBinder']/*" />
    [Serializable]
    public sealed class ToggleCommandBinder : TargetBinder<Toggle>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ToggleCommandBinder(Toggle target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ToggleCommandBinder(Toggle target, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Custom, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="interactableMode">Controls how the toggle's interactable state reflects <c>CanExecute</c>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ToggleCommandBinder(Toggle target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{bool}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
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
            Target.onValueChanged.AddListener(OnValueChanged);
        
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
    
    /// <summary>
    /// <see cref="TargetBinder{Toggle}"/> that executes a command with one additional parameter
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T}"/> (receiving the isOn state and the configured parameter).
    /// </summary>
    /// <typeparam name="T">The type of the additional parameter forwarded alongside the isOn value.</typeparam>
    /// <include file="XmlExampleDoc-Toggle-Command-1.1.0.xml" path="doc//member[@name='ToggleCommandBinderT']/*" />
    [Serializable]
    public class ToggleCommandBinder<T> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        [SerializeField] private T _param;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ToggleCommandBinder(Toggle target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded alongside the isOn value.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ToggleCommandBinder(Toggle target, T param, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param = param;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param">The additional parameter forwarded alongside the isOn value.</param>
        /// <param name="interactableMode">Controls how the toggle's interactable state reflects <c>CanExecute</c>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{bool, T}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
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
            Target.onValueChanged.AddListener(OnValueChanged);
        
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
    
    /// <summary>
    /// <see cref="TargetBinder{Toggle}"/> that executes a command with two additional parameters
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T1, T2}"/> (receiving the isOn state and the configured parameters).
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the isOn value.</typeparam>
    /// <include file="XmlExampleDoc-Toggle-Command-1.1.0.xml" path="doc//member[@name='ToggleCommandBinderT1T2']/*" />
    [Serializable]
    public class ToggleCommandBinder<T1, T2> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;
        
        /// <inheritdoc/>
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T1, T2}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the isOn value.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T1, T2}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the isOn value.</param>
        /// <param name="interactableMode">Controls how the toggle's interactable state reflects <c>CanExecute</c>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{bool, T1, T2}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
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
            Target.onValueChanged.AddListener(OnValueChanged);
        
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
    
    /// <summary>
    /// <see cref="TargetBinder{Toggle}"/> that executes a command with three additional parameters
    /// each time <see cref="Toggle.onValueChanged"/> fires.
    /// Accepts commands typed as <see cref="IRelayCommand{bool, T1, T2, T3}"/> (receiving the isOn state and the configured parameters).
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded alongside the isOn value.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded alongside the isOn value.</typeparam>
    /// <include file="XmlExampleDoc-Toggle-Command-1.1.0.xml" path="doc//member[@name='ToggleCommandBinderT1T2T3']/*" />
    [Serializable]
    public class ToggleCommandBinder<T1, T2, T3> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        /// <inheritdoc cref="TargetBinder{TTarget}.IsBind"/>
        public override bool IsBind => Target is not null;

        /// <inheritdoc/>
        public ToggleCommandBinder(Toggle target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T1, T2, T3}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param3">The third additional parameter forwarded alongside the isOn value.</param>
        /// <param name="customInteractable">A custom view that reflects the command's <c>CanExecute</c> state.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Initializes a new instance of <see cref="ToggleCommandBinder{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="target">The <see cref="Toggle"/> to bind.</param>
        /// <param name="param1">The first additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param2">The second additional parameter forwarded alongside the isOn value.</param>
        /// <param name="param3">The third additional parameter forwarded alongside the isOn value.</param>
        /// <param name="interactableMode">Controls how the toggle's interactable state reflects <c>CanExecute</c>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
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
        
        /// <summary>
        /// Binds an <see cref="IRelayCommand{bool, T1, T2, T3}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
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
            Target.onValueChanged.AddListener(OnValueChanged);
        
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