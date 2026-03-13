#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand"/> or
    /// <see cref="IRelayCommand{T}"/> (with string argument) to a <see cref="TMP_InputField"/> event,
    /// and optionally updates the field's interactability based on <c>CanExecute</c>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-Command-1.1.0.xml" path="doc//member[@name='InputFieldCommandBinder']/*" />
    [Serializable]
    public sealed class InputFieldCommandBinder: TargetBinder<TMP_InputField>, 
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, updateEvent, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="customInteractable">The custom view that reflects whether the command can execute.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="interactableMode">Determines how command executability is reflected on the input field. Must not be <see cref="InteractableMode.Custom"/>.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        /// <summary>
        /// Assigns the relay command and subscribes to <c>CanExecute</c> changes.
        /// </summary>
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <c>CanExecute</c> changes.
        /// </summary>
        public void SetValue(IRelayCommand<string> value) =>
            CommandBinderExtensions.UpdateCommand(ref _stringCommand, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to the configured input field event.
        /// </summary>
        /// <remarks>
        /// The specific event subscribed to is determined by the configured <see cref="UpdateInputFieldEvent"/> value.
        /// </remarks>
        protected override void OnBound() =>
            Subscribe();

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from the input field event and clears bound commands.
        /// </summary>
        /// <remarks>
        /// Commands are nullified to release <c>CanExecute</c> subscriptions and prevent stale references.
        /// </remarks>
        protected override void OnUnbound()
        {
            Unsubscribe();
            
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<string>)null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value)
        {
            if (_command is not null) _command.Execute();
            else _stringCommand?.Execute(value);
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute());
        }
        
        private void OnCanExecuteChanged(IRelayCommand<string> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text));
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
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2}"/> (receiving the
    /// input field text and <typeparamref name="T"/> as arguments) to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T">The type of the additional command parameter.</typeparam>
    /// <include file="XmlExampleDoc-InputField-Command-1.1.0.xml" path="doc//member[@name='InputFieldCommandBinderT']/*" />
    [Serializable]
    public class InputFieldCommandBinder<T>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T>>
    {
        [Tooltip("The parameter passed to the command.")]
        [SerializeField] private T _param;
        
        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T> _command;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T param, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, updateEvent, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param">The parameter passed to the command alongside the input field text.</param>
        /// <param name="customInteractable">The custom view that reflects whether the command can execute.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T param,
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T}"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param">The parameter passed to the command alongside the input field text.</param>
        /// <param name="interactableMode">Determines how command executability is reflected on the input field. Must not be <see cref="InteractableMode.Custom"/>.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T param,
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <c>CanExecute</c> changes.
        /// </summary>
        public void SetValue(IRelayCommand<string, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to the configured input field event.
        /// </summary>
        /// <remarks>
        /// The specific event subscribed to is determined by the configured <see cref="UpdateInputFieldEvent"/> value.
        /// </remarks>
        protected override void OnBound() =>
            Subscribe();

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from the input field event and clears the bound command.
        /// </summary>
        /// <remarks>
        /// The command is nullified to release the <c>CanExecute</c> subscription and prevent stale references.
        /// </remarks>
        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param));
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
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2,T3}"/> (receiving the
    /// input field text, <typeparamref name="T1"/>, and <typeparamref name="T2"/> as arguments) to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional command parameter.</typeparam>
    /// <include file="XmlExampleDoc-InputField-Command-1.1.0.xml" path="doc//member[@name='InputFieldCommandBinderT1T2']/*" />
    [Serializable]
    public class InputFieldCommandBinder<T1, T2>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T1, T2>>
    {
        [Tooltip("The first parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("The second parameter passed to the command.")]
        [SerializeField] private T2 _param2;
        
        [Space]
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T1, T2> _command;
        
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

        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, updateEvent, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T1,T2}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param1">The first parameter passed to the command.</param>
        /// <param name="param2">The second parameter passed to the command.</param>
        /// <param name="customInteractable">The custom view that reflects whether the command can execute.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T1,T2}"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param1">The first parameter passed to the command.</param>
        /// <param name="param2">The second parameter passed to the command.</param>
        /// <param name="interactableMode">Determines how command executability is reflected on the input field. Must not be <see cref="InteractableMode.Custom"/>.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <c>CanExecute</c> changes.
        /// </summary>
        public void SetValue(IRelayCommand<string, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to the configured input field event.
        /// </summary>
        /// <remarks>
        /// The specific event subscribed to is determined by the configured <see cref="UpdateInputFieldEvent"/> value.
        /// </remarks>
        protected override void OnBound() =>
            Subscribe();

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from the input field event and clears the bound command.
        /// </summary>
        /// <remarks>
        /// The command is nullified to release the <c>CanExecute</c> subscription and prevent stale references.
        /// </remarks>
        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2));
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
    /// <see cref="TargetBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2,T3,T4}"/> (receiving the
    /// input field text, <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/> as arguments)
    /// to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third additional command parameter.</typeparam>
    /// <include file="XmlExampleDoc-InputField-Command-1.1.0.xml" path="doc//member[@name='InputFieldCommandBinderT1T2T3']/*" />
    [Serializable]
    public class InputFieldCommandBinder<T1, T2, T3>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T1, T2, T3>>
    {
        [Tooltip("The first parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("The second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("The third parameter passed to the command.")]
        [SerializeField] private T3 _param3;
        
        [Space]
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T1, T2, T3> _command;
        
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

        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        /// <inheritdoc/>
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, T3 param3, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, updateEvent, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T1,T2,T3}"/> with a custom interactable view.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param1">The first parameter passed to the command.</param>
        /// <param name="param2">The second parameter passed to the command.</param>
        /// <param name="param3">The third parameter passed to the command.</param>
        /// <param name="customInteractable">The custom view that reflects whether the command can execute.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            T3 param3,
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldCommandBinder{T1,T2,T3}"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="param1">The first parameter passed to the command.</param>
        /// <param name="param2">The second parameter passed to the command.</param>
        /// <param name="param3">The third parameter passed to the command.</param>
        /// <param name="interactableMode">Determines how command executability is reflected on the input field. Must not be <see cref="InteractableMode.Custom"/>.</param>
        /// <param name="updateEvent">The input field event that triggers command execution.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            T3 param3,
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <c>CanExecute</c> changes.
        /// </summary>
        public void SetValue(IRelayCommand<string, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Subscribes to the configured input field event.
        /// </summary>
        /// <remarks>
        /// The specific event subscribed to is determined by the configured <see cref="UpdateInputFieldEvent"/> value.
        /// </remarks>
        protected override void OnBound() =>
            Subscribe();

        /// <summary>
        /// Called when the binder is unbound. Unsubscribes from the input field event and clears the bound command.
        /// </summary>
        /// <remarks>
        /// The command is nullified to release the <c>CanExecute</c> subscription and prevent stale references.
        /// </remarks>
        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2, Param3));
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
#endif