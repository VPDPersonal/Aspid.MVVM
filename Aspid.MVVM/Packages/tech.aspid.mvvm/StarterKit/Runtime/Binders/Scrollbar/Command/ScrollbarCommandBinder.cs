using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on
    /// <see cref="Scrollbar.onValueChanged"/> with the scrollbar value.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T}"/> with an <see langword="int"/>, <see langword="long"/>,
    /// <see langword="float"/> or <see langword="double"/> value; integers are truncated.
    /// </remarks>
    [Serializable]
    public sealed class ScrollbarCommandBinder : TargetBinder<Scrollbar>,
        IBinder<IRelayCommand<int>>,
        IBinder<IRelayCommand<long>>,
        IBinder<IRelayCommand<float>>,
        IBinder<IRelayCommand<double>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("How the command's CanExecute is reflected on the scrollbar.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        private IRelayCommand<float> _floatCommand;
        private IRelayCommand<double> _doubleCommand;

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ScrollbarCommandBinder(Scrollbar target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
            ICanExecuteHandler customInteractable,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the scrollbar; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<double> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
            SetValue((IRelayCommand<float>)null);
            SetValue((IRelayCommand<double>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(value);
            else if (_intCommand is not null) _intCommand.Execute((int)value);
            else if (_doubleCommand is not null) _doubleCommand.Execute(value);
            else if (_longCommand is not null) _longCommand.Execute((long)value);
        }

        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((int)Target.value));
        }

        private void OnCanExecuteChanged(IRelayCommand<long> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((long)Target.value));
        }

        private void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value));
        }

        private void OnCanExecuteChanged(IRelayCommand<double> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on
    /// <see cref="Scrollbar.onValueChanged"/> with the scrollbar value and <see cref="Param"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2}"/> with an <see langword="int"/>, <see langword="long"/>,
    /// <see langword="float"/> or <see langword="double"/> value; integers are truncated.
    /// </remarks>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    [Serializable]
    public class ScrollbarCommandBinder<T> : TargetBinder<Scrollbar>,
        IBinder<IRelayCommand<int, T>>,
        IBinder<IRelayCommand<long, T>>,
        IBinder<IRelayCommand<float, T>>,
        IBinder<IRelayCommand<double, T>>
    {
        [Tooltip("Extra parameter passed after the scrollbar value.")]
        [SerializeField] private T _param;

        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [Tooltip("How the command's CanExecute is reflected on the scrollbar.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T> _intCommand;
        private IRelayCommand<long, T> _longCommand;
        private IRelayCommand<float, T> _floatCommand;
        private IRelayCommand<double, T> _doubleCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param">The extra parameter passed after the scrollbar value.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
            T param,
            BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param">The extra parameter passed after the scrollbar value.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param">The extra parameter passed after the scrollbar value.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the scrollbar; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<float, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<double, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
            SetValue((IRelayCommand<float, T>)null);
            SetValue((IRelayCommand<double, T>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(value, Param);
            else if (_intCommand is not null) _intCommand.Execute((int)value, Param);
            else if (_doubleCommand is not null) _doubleCommand.Execute(value, Param);
            else if (_longCommand is not null) _longCommand.Execute((long)value, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((int)Target.value, Param));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((long)Target.value, Param));
        }

        private void OnCanExecuteChanged(IRelayCommand<float, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param));
        }

        private void OnCanExecuteChanged(IRelayCommand<double, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on
    /// <see cref="Scrollbar.onValueChanged"/> with the scrollbar value and <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3}"/> with an <see langword="int"/>, <see langword="long"/>,
    /// <see langword="float"/> or <see langword="double"/> value; integers are truncated.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    [Serializable]
    public class ScrollbarCommandBinder<T1, T2> : TargetBinder<Scrollbar>,
        IBinder<IRelayCommand<int, T1, T2>>,
        IBinder<IRelayCommand<long, T1, T2>>,
        IBinder<IRelayCommand<float, T1, T2>>,
        IBinder<IRelayCommand<double, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the scrollbar value.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the scrollbar value.")]
        [SerializeField] private T2 _param2;

        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [Tooltip("How the command's CanExecute is reflected on the scrollbar.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T1, T2> _intCommand;
        private IRelayCommand<long, T1, T2> _longCommand;
        private IRelayCommand<float, T1, T2> _floatCommand;
        private IRelayCommand<double, T1, T2> _doubleCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
            T1 param1,
            T2 param2,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the scrollbar; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<float, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<double, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
            SetValue((IRelayCommand<float, T1, T2>)null);
            SetValue((IRelayCommand<double, T1, T2>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(value, Param1, Param2);
            else if (_intCommand is not null) _intCommand.Execute((int)value, Param1, Param2);
            else if (_doubleCommand is not null) _doubleCommand.Execute(value, Param1, Param2);
            else if (_longCommand is not null) _longCommand.Execute((long)value, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((int)Target.value, Param1, Param2));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((long)Target.value, Param1, Param2));
        }

        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param1, Param2));
        }

        private void OnCanExecuteChanged(IRelayCommand<double, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on
    /// <see cref="Scrollbar.onValueChanged"/> with the scrollbar value and <see cref="Param1"/>,
    /// <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand{T, T2, T3, T4}"/> with an <see langword="int"/>, <see langword="long"/>,
    /// <see langword="float"/> or <see langword="double"/> value; integers are truncated.
    /// </remarks>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    [Serializable]
    public class ScrollbarCommandBinder<T1, T2, T3> : TargetBinder<Scrollbar>,
        IBinder<IRelayCommand<int, T1, T2, T3>>,
        IBinder<IRelayCommand<long, T1, T2, T3>>,
        IBinder<IRelayCommand<float, T1, T2, T3>>,
        IBinder<IRelayCommand<double, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the scrollbar value.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the scrollbar value.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the scrollbar value.")]
        [SerializeField] private T3 _param3;

        // ReSharper disable once MemberInitializerValueIgnored
        [Space]
        [Tooltip("How the command's CanExecute is reflected on the scrollbar.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<int, T1, T2, T3> _intCommand;
        private IRelayCommand<long, T1, T2, T3> _longCommand;
        private IRelayCommand<float, T1, T2, T3> _floatCommand;
        private IRelayCommand<double, T1, T2, T3> _doubleCommand;

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the extra parameter passed after the scrollbar value.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param3">The extra parameter passed after the scrollbar value.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
            T1 param1,
            T2 param2,
            T3 param3,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param3">The extra parameter passed after the scrollbar value.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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

        /// <param name="target">The scrollbar to bind.</param>
        /// <param name="param1">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param2">The extra parameter passed after the scrollbar value.</param>
        /// <param name="param3">The extra parameter passed after the scrollbar value.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the scrollbar; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public ScrollbarCommandBinder(
            Scrollbar target,
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
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<float, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<double, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);

            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
            SetValue((IRelayCommand<float, T1, T2, T3>)null);
            SetValue((IRelayCommand<double, T1, T2, T3>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(value, Param1, Param2, Param3);
            else if (_intCommand is not null) _intCommand.Execute((int)value, Param1, Param2, Param3);
            else if (_doubleCommand is not null) _doubleCommand.Execute(value, Param1, Param2, Param3);
            else if (_longCommand is not null) _longCommand.Execute((long)value, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((int)Target.value, Param1, Param2, Param3));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute((long)Target.value, Param1, Param2, Param3));
        }

        private void OnCanExecuteChanged(IRelayCommand<float, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param1, Param2, Param3));
        }

        private void OnCanExecuteChanged(IRelayCommand<double, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.value, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
