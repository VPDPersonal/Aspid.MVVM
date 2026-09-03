using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on the selected field event with the text.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> and <see cref="IRelayCommand{T}"/> with a <see langword="string"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – Command")]
    public sealed partial class InputFieldCommandMonoBinder : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("How the command's CanExecute is reflected on the field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        /// <summary>
        /// Re-subscribes to the selected event after an Inspector change in Play mode.
        /// </summary>
        /// <remarks>
        /// Runs only while bound, so the listener is never stacked or left behind.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            CachedComponent.RemoveListenerFromAll(OnValueChanged);
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

            if (_command is not null) OnCanExecuteChanged(_command);
            if (_stringCommand is not null) OnCanExecuteChanged(_stringCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<string> value) =>
            CommandBinderExtensions.UpdateCommand(ref _stringCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<string>)null);
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T>>
    {
        [Tooltip("Extra parameter passed after the text.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("How the command's CanExecute is reflected on the field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<string, T> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <summary>
        /// Re-subscribes to the selected event after an Inspector change in Play mode.
        /// </summary>
        /// <remarks>
        /// Runs only while bound, so the listener is never stacked or left behind.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            CachedComponent.RemoveListenerFromAll(OnValueChanged);
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<string, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);

        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T1, T2> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the text.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the text.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("How the command's CanExecute is reflected on the field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<string, T1, T2> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Re-subscribes to the selected event after an Inspector change in Play mode.
        /// </summary>
        /// <remarks>
        /// Runs only while bound, so the listener is never stacked or left behind.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            CachedComponent.RemoveListenerFromAll(OnValueChanged);
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<string, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);

        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param1"/>, <see cref="Param2"/>,
    /// <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    public abstract partial class InputFieldCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the text.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the text.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the text.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        [Tooltip("How the command's CanExecute is reflected on the field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<string, T1, T2, T3> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the text.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <summary>
        /// Re-subscribes to the selected event after an Inspector change in Play mode.
        /// </summary>
        /// <remarks>
        /// Runs only while bound, so the listener is never stacked or left behind.
        /// </remarks>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying || !IsBound) return;

            CachedComponent.RemoveListenerFromAll(OnValueChanged);
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<string, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);

        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
