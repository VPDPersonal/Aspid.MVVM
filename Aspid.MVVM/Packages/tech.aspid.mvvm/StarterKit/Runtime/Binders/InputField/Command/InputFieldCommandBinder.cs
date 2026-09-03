using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on the selected field event with the text.
    /// </summary>
    /// <remarks>
    /// Accepts <see cref="IRelayCommand"/> and <see cref="IRelayCommand{T}"/> with a <see langword="string"/>.
    /// </remarks>
    [Serializable]
    public sealed class InputFieldCommandBinder : TargetBinder<TMP_InputField>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("How the command's CanExecute is reflected on the field.")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [Tooltip("Handler that reflects CanExecute in Custom mode.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        /// <param name="target">The field to bind.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            UpdateInputFieldEvent updateEvent,
            BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, updateEvent, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            ICanExecuteHandler customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _updateEvent = updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The field to bind.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the field; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _updateEvent = updateEvent;
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
        public void SetValue(IRelayCommand<string> value) =>
            CommandBinderExtensions.UpdateCommand(ref _stringCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

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
            SetInteractableMode(command.CanExecute(Target.text));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param"/>.
    /// </summary>
    /// <typeparam name="T">The type of the extra parameter.</typeparam>
    [Serializable]
    public class InputFieldCommandBinder<T> : TargetBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T>>
    {
        [Tooltip("Extra parameter passed after the text.")]
        [SerializeField] private T _param;

        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The field to bind.</param>
        /// <param name="param">The extra parameter passed after the text.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T param,
            BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param">The extra parameter passed after the text.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T param,
            UpdateInputFieldEvent updateEvent,
            BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, updateEvent, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param">The extra parameter passed after the text.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T param,
            ICanExecuteHandler customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param = param;

            _updateEvent = updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The field to bind.</param>
        /// <param name="param">The extra parameter passed after the text.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the field; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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

            _updateEvent = updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<string, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);

        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    [Serializable]
    public class InputFieldCommandBinder<T1, T2> : TargetBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2>>
    {
        [Tooltip("First extra parameter passed after the text.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the text.")]
        [SerializeField] private T2 _param2;

        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            UpdateInputFieldEvent updateEvent,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, updateEvent, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            ICanExecuteHandler customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;

            _updateEvent = updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the field; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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

            _updateEvent = updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<string, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);

        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command on the selected field event with the text
    /// and <see cref="Param1"/>, <see cref="Param2"/>,
    /// <see cref="Param3"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first extra parameter.</typeparam>
    /// <typeparam name="T2">The type of the second extra parameter.</typeparam>
    /// <typeparam name="T3">The type of the third extra parameter.</typeparam>
    [Serializable]
    public class InputFieldCommandBinder<T1, T2, T3> : TargetBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2, T3>>
    {
        [Tooltip("First extra parameter passed after the text.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second extra parameter passed after the text.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third extra parameter passed after the text.")]
        [SerializeField] private T3 _param3;

        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Field event that executes the command.")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;

        // ReSharper disable once MemberInitializerValueIgnored
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

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="param3">The extra parameter passed after the text.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            T3 param3,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="param3">The extra parameter passed after the text.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            T3 param3,
            UpdateInputFieldEvent updateEvent,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, updateEvent, mode) { }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="param3">The extra parameter passed after the text.</param>
        /// <param name="customInteractable">The handler that reflects the command's CanExecute.</param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="customInteractable"/> is <see langword="null"/>.
        /// </exception>
        public InputFieldCommandBinder(
            TMP_InputField target,
            T1 param1,
            T2 param2,
            T3 param3,
            ICanExecuteHandler customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;

            _updateEvent = updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }

        /// <param name="target">The field to bind.</param>
        /// <param name="param1">The extra parameter passed after the text.</param>
        /// <param name="param2">The extra parameter passed after the text.</param>
        /// <param name="param3">The extra parameter passed after the text.</param>
        /// <param name="interactableMode">
        /// How the command's CanExecute is reflected on the field; not <see cref="InteractableMode.Custom"/>.
        /// </param>
        /// <param name="updateEvent">The field event that executes the command.</param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="interactableMode"/> is <see cref="InteractableMode.Custom"/>.
        /// </exception>
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

            _updateEvent = updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(
                    nameof(interactableMode),
                    "Use the ICanExecuteHandler constructor for Custom.");
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<string, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound() =>
            Target.GetEvent(_updateEvent).AddListener(OnValueChanged);

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.GetEvent(_updateEvent).RemoveListener(OnValueChanged);

            SetValue(null);
        }

        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);

        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable) =>
            Target.SetInteractable(_interactableMode, isInteractable, _customInteractable, this);
    }
}
