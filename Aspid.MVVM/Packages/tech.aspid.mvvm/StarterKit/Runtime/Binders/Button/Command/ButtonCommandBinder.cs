using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Button.onClick"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> or <see cref="IRelayCommand{T}"/> with a <see langword="bool"/>, which
    /// receives <see langword="true"/>.
    /// </remarks>
    [Serializable]
    public sealed class ButtonCommandBinder : TargetBinder<Button>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<bool>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("How the command's CanExecute is reflected on the button.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<bool> _boolCommand;

        /// <param name="target">The button to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ButtonCommandBinder(Button target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }

        /// <param name="target">The button to bind.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The button to bind.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the button; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
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
            CommandBinderExtensions.UpdateCommand(ref _boolCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);

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
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    [Serializable]
    public class ButtonCommandBinder<T> : TargetBinder<Button>,
        IBinder<IRelayCommand<T>>
    {
        [Tooltip("Parameter passed to the command.")]
        [SerializeField] private T _param;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param">The parameter passed to the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T param,
            BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }

        /// <param name="target">The button to bind.</param>
        /// <param name="param">The parameter passed to the command.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param">The parameter passed to the command.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the button; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
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
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    [Serializable]
    public class ButtonCommandBinder<T1, T2> : TargetBinder<Button>,
        IBinder<IRelayCommand<T1, T2>>
    {
        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T1 param1,
            T2 param2,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the button; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
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
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3> : TargetBinder<Button>,
        IBinder<IRelayCommand<T1, T2, T3>>
    {
        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third parameter passed to the command.")]
        [SerializeField] private T3 _param3;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T1 param1,
            T2 param2,
            T3 param3,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the button; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
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
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
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
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on <see cref="Button.onClick"/>
    /// with <see cref="Param1"/>, <see cref="Param2"/>,
    /// <see cref="Param3"/>, <see cref="Param4"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    [Serializable]
    public class ButtonCommandBinder<T1, T2, T3, T4> : TargetBinder<Button>,
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

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="param4">The parameter passed to the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, param4, InteractableMode.Interactable, mode) { }

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="param4">The parameter passed to the command.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
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

        /// <param name="target">The button to bind.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="param4">The parameter passed to the command.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the button; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ButtonCommandBinder(
            Button target,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            InteractableMode interactableMode,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;

            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onClick.AddListener(OnClicked);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onClick.RemoveListener(OnClicked);
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
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
