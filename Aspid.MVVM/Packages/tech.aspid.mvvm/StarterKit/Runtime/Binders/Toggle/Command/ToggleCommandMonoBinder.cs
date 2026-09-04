using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="Toggle.onValueChanged"/> with the new <see cref="Toggle.isOn"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> or <see cref="IRelayCommand{T}"/> with the <see langword="bool"/> state.
    /// </remarks>
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – Command")]
    public sealed partial class ToggleCommandMonoBinder : ComponentMonoBinder<Toggle>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        [Tooltip("How the command's CanExecute is reflected on the toggle.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            else if (_isOnCommand is not null) OnCanExecuteChanged(_isOnCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="Toggle.onValueChanged"/> with the new <see cref="Toggle.isOn"/> and <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T> : ComponentMonoBinder<Toggle>,
        IBinder<IRelayCommand<bool, T>>
    {
        [Tooltip("Extra parameter passed after the toggle state.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the toggle.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<bool, T> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="Toggle.onValueChanged"/> with the new <see cref="Toggle.isOn"/> and <see cref="Param1"/>,
    /// <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T1, T2> : ComponentMonoBinder<Toggle>,
        IBinder<IRelayCommand<bool, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the toggle state.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the toggle state.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the toggle.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<bool, T1, T2> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="Toggle.onValueChanged"/> with the new <see cref="Toggle.isOn"/> and <see cref="Param1"/>,
    /// <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    public abstract partial class ToggleCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Toggle>,
        IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the toggle state.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the toggle state.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the toggle state.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the toggle.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<bool, T1, T2, T3> _command;

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the toggle state.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<bool, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
