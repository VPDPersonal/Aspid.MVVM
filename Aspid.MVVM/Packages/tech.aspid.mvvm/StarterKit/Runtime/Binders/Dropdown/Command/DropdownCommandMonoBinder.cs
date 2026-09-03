using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="TMP_Dropdown.onValueChanged"/> with the selected index.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T}"/> with an <see langword="int"/> or <see langword="long"/> index.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Command")]
    public sealed partial class DropdownCommandMonoBinder : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int>>,
        IBinder<IRelayCommand<long>>
    {
        [Tooltip("How the command's CanExecute is reflected on the dropdown.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value);
            else _longCommand?.Execute(CachedComponent.value);
        }

        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value));
        }

        private void OnCanExecuteChanged(IRelayCommand<long> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="TMP_Dropdown.onValueChanged"/> with the selected index and <see cref="Param"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2}"/> with an <see langword="int"/> or <see langword="long"/> index.
    /// </remarks>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    public abstract partial class DropdownCommandMonoBinder<T> : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int, T>>,
        IBinder<IRelayCommand<long, T>>
    {
        [Tooltip("Extra parameter passed after the selected index.")]
        [SerializeField] private T _param;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the dropdown.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T> _intCommand;
        private IRelayCommand<long, T> _longCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
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

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param);
            else _longCommand?.Execute(CachedComponent.value, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="TMP_Dropdown.onValueChanged"/> with the selected index and <see cref="Param1"/>,
    /// <see cref="Param2"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3}"/> with an <see langword="int"/> or <see langword="long"/> index.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    public abstract partial class DropdownCommandMonoBinder<T1, T2> : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int, T1, T2>>,
        IBinder<IRelayCommand<long, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the selected index.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the selected index.")]
        [SerializeField] private T2 _param2;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the dropdown.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T1, T2> _intCommand;
        private IRelayCommand<long, T1, T2> _longCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
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

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param1, Param2);
            else _longCommand?.Execute(CachedComponent.value, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that executes a command on
    /// <see cref="TMP_Dropdown.onValueChanged"/> with the selected index and <see cref="Param1"/>,
    /// <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3, T4}"/> with an <see langword="int"/> or <see langword="long"/>
    /// index.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    public abstract partial class DropdownCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int, T1, T2, T3>>,
        IBinder<IRelayCommand<long, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the selected index.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the selected index.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the selected index.")]
        [SerializeField] private T3 _param3;

        [Space]
        [Tooltip("How the command's CanExecute is reflected on the dropdown.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T1, T2, T3> _intCommand;
        private IRelayCommand<long, T1, T2, T3> _longCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the selected index.
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

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else _longCommand?.Execute(CachedComponent.value, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2, Param3));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            CachedComponent.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
