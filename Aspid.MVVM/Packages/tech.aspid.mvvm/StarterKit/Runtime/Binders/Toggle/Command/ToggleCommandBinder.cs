using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Toggle.onValueChanged"/> with
    /// the new <see cref="Toggle.isOn"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> or <see cref="IRelayCommand{T}"/> with the <see langword="bool"/> state.
    /// </remarks>
    [Serializable]
    public sealed class ToggleCommandBinder : TargetBinder<Toggle>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("How the command's CanExecute is reflected on the toggle.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;

        /// <param name="target">The toggle to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ToggleCommandBinder(Toggle target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the toggle; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            InteractableMode interactableMode,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Toggle.onValueChanged"/> with
    /// the new <see cref="Toggle.isOn"/> and <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    [Serializable]
    public class ToggleCommandBinder<T> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T>>
    {
        [Tooltip("Extra parameter passed after the toggle state.")]
        [SerializeField] private T _param;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param">The extra parameter passed after the toggle state.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T param,
            BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param">The extra parameter passed after the toggle state.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T param,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param = param;

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param">The extra parameter passed after the toggle state.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the toggle; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T param,
            InteractableMode interactableMode,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param = param;

            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<bool, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Toggle.onValueChanged"/> with
    /// the new <see cref="Toggle.isOn"/> and <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    [Serializable]
    public class ToggleCommandBinder<T1, T2> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the toggle state.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the toggle state.")]
        [SerializeField] private T2 _param2;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T1 param1,
            T2 param2,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T1 param1,
            T2 param2,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the toggle; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<bool, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Toggle.onValueChanged"/> with
    /// the new <see cref="Toggle.isOn"/> and <see cref="Param1"/>, <see cref="Param2"/>,
    /// <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    [Serializable]
    public class ToggleCommandBinder<T1, T2, T3> : TargetBinder<Toggle>, IBinder<IRelayCommand<bool, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the toggle state.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the toggle state.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the toggle state.")]
        [SerializeField] private T3 _param3;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="param3">The extra parameter passed after the toggle state.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T1 param1,
            T2 param2,
            T3 param3,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="param3">The extra parameter passed after the toggle state.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ToggleCommandBinder(
            Toggle target,
            T1 param1,
            T2 param2,
            T3 param3,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The toggle to bind.</param>
        /// <param name="param1">The extra parameter passed after the toggle state.</param>
        /// <param name="param2">The extra parameter passed after the toggle state.</param>
        /// <param name="param3">The extra parameter passed after the toggle state.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the toggle; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<bool, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
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

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
