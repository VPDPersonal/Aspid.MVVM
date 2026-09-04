using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on <see cref="Button.onClick"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> or <see cref="IRelayCommand{T}"/> with a <see langword="bool"/>, which
    /// receives <see langword="true"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(Button), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Button/Button Binder – Command")]
    public sealed partial class ButtonCommandMonoBinder : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<bool> _boolCommand;

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            else if (_boolCommand is not null) OnCanExecuteChanged(_boolCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _boolCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnClicked);

            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<bool>)null);
        }

        private void OnClicked()
        {
            if (_command is not null) _command.Execute();
            else _boolCommand?.Execute(true);
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

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T>>
    {
        [Tooltip("Parameter passed to the command.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T> _command;

        /// <summary>
        /// Gets or sets the parameter passed to the command.
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
        public void SetValue(IRelayCommand<T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }

        private void OnClicked() =>
            _command?.Execute(Param);

        private void OnCanExecuteChanged(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T1, T2>>
    {
        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T1, T2> _command;

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
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
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }

        private void OnClicked() =>
            _command?.Execute(Param1, Param2);

        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T1, T2, T3>>
    {
        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third parameter passed to the command.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T1, T2, T3> _command;

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
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
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }

        private void OnClicked() =>
            _command?.Execute(Param1, Param2, Param3);

        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>,
    /// <see cref="Param3"/>, <see cref="Param4"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    public abstract partial class ButtonCommandMonoBinder<T1, T2, T3, T4> : ComponentMonoBinder<Button>,
        IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third parameter passed to the command.")]
        [SerializeField] private T3 _param3;

        [Tooltip("Fourth parameter passed to the command.")]
        [SerializeField] private T4 _param4;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T1, T2, T3, T4> _command;

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T4 Param4
        {
            get => _param4;
            set => _param4 = value;
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onClick.RemoveListener(OnClicked);
            SetValue(null);
        }

        private void OnClicked() =>
            _command?.Execute(Param1, Param2, Param3, Param4);

        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Param1, Param2, Param3, Param4));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
