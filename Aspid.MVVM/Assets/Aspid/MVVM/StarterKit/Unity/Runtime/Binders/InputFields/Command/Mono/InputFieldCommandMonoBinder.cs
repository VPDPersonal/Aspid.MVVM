#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand"/> or
    /// <see cref="IRelayCommand{T}"/> (with string argument) to a <see cref="TMP_InputField"/> event,
    /// and optionally updates the field's interactability based on <see cref="IRelayCommand.CanExecute()"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Command/InputField Binder – Command")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Calls")]
    public sealed partial class InputFieldCommandMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            
            if (_command is not null) OnCanExecuteChanged(_command);
            if (_stringCommand is not null) OnCanExecuteChanged(_stringCommand);
        }

        /// <summary>
        /// Assigns the relay command and subscribes to <see cref="IRelayCommand.CanExecuteChanged"/> notifications.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <see cref="IRelayCommand.CanExecuteChanged"/> notifications.
        /// </summary>
        [BinderLog]
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
        /// Commands are nullified to release <see cref="IRelayCommand.CanExecuteChanged"/> subscriptions and prevent stale references.
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
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
            SetInteractableMode(command.CanExecute(CachedComponent.text));
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
    /// Abstract base <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2}"/> (receiving the
    /// input field text and <typeparamref name="T"/> as arguments) to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T">The type of the additional command parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T>>
    {
        [Tooltip("The parameter passed to the command.")]
        [SerializeField] private T _param;
        
        [Space]
        [Tooltip("The input field event that triggers command execution.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        [Tooltip("Determines how command executability is reflected on the input field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [Tooltip("Custom view for reflecting command executability.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T> _command;

        /// <summary>Gets or sets the parameter passed to the command alongside the input field text.</summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;

            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);

            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <see cref="IRelayCommand.CanExecuteChanged"/> notifications.
        /// </summary>
        [BinderLog]
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
        /// The command is nullified to release the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and prevent stale references.
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param));
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
    /// Abstract base <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2,T3}"/> (receiving the
    /// input field text, <typeparamref name="T1"/>, and <typeparamref name="T2"/> as arguments) to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional command parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T1, T2> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2>>
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

        /// <summary>Gets or sets the first parameter passed to the command alongside the input field text.</summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>Gets or sets the second parameter passed to the command alongside the input field text.</summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;

            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);

            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <see cref="IRelayCommand.CanExecuteChanged"/> notifications.
        /// </summary>
        [BinderLog]
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
        /// The command is nullified to release the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and prevent stale references.
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2));
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
    /// Abstract base <see cref="ComponentMonoBinder{TMP_InputField}"/> that binds an <see cref="IRelayCommand{T1,T2,T3,T4}"/> (receiving the
    /// input field text, <typeparamref name="T1"/>, <typeparamref name="T2"/>, and <typeparamref name="T3"/> as arguments)
    /// to a <see cref="TMP_InputField"/> event.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional command parameter.</typeparam>
    /// <typeparam name="T2">The type of the second additional command parameter.</typeparam>
    /// <typeparam name="T3">The type of the third additional command parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<TMP_InputField>, 
        IBinder<IRelayCommand<string, T1, T2, T3>>
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

        /// <summary>Gets or sets the first parameter passed to the command alongside the input field text.</summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>Gets or sets the second parameter passed to the command alongside the input field text.</summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>Gets or sets the third parameter passed to the command alongside the input field text.</summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        /// <summary>
        /// Assigns the parameterized relay command and subscribes to <see cref="IRelayCommand.CanExecuteChanged"/> notifications.
        /// </summary>
        [BinderLog]
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
        /// The command is nullified to release the <see cref="IRelayCommand.CanExecuteChanged"/> subscription and prevent stale references.
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2, Param3));
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
#endif