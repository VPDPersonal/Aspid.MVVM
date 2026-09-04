using System;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command when the selected <see cref="EventTrigger"/> event
    /// fires.
    /// </summary>
    /// <remarks>
    /// Accepts a plain command, one that receives the <see cref="BaseEventData"/>, or one that receives the
    /// <see cref="EventTriggerType"/> as its first argument.
    /// </remarks>
    [Serializable]
    public sealed class EventTriggerCommandBinder : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<BaseEventData>>,
        IBinder<IRelayCommand<EventTriggerType>>
    {
        [Tooltip("Event that executes the command.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<BaseEventData> _dataCommand;
        private IRelayCommand<EventTriggerType> _typeCommand;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <param name="target">The event trigger to bind.</param>
        /// <param name="eventType">The event that executes the command.</param>
        /// <param name="customInteractable">
        /// The handler that reflects the command's CanExecute, or <see langword="null"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public EventTriggerCommandBinder(
            EventTrigger target,
            EventTriggerType eventType,
            ICanExecuteHandler customInteractable = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventType;
            _customInteractable = customInteractable;
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<BaseEventData> value) =>
            CommandBinderExtensions.UpdateCommand(ref _dataCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<EventTriggerType> value) =>
            CommandBinderExtensions.UpdateCommand(ref _typeCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry { eventID = _event };
            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;
            _lastEvent = null;

            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<BaseEventData>)null);
            SetValue((IRelayCommand<EventTriggerType>)null);
        }

        private void OnTrigger(BaseEventData data)
        {
            _lastEvent = data;

            if (_command is not null) _command.Execute();
            else if (_dataCommand is not null) _dataCommand.Execute(data);
            else _typeCommand?.Execute(_event);
        }

        private void OnCanExecuteChanged(IRelayCommand command) =>
            _customInteractable?.SetCanExecute(command.CanExecute());

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event));
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command when the selected <see cref="EventTrigger"/> event
    /// fires with <see cref="Param"/>.
    /// </summary>
    /// <remarks>
    /// Accepts a plain command, one that receives the <see cref="BaseEventData"/>, or one that receives the
    /// <see cref="EventTriggerType"/> as its first argument.
    /// </remarks>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    [Serializable]
    public class EventTriggerCommandBinder<T> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T>>,
        IBinder<IRelayCommand<BaseEventData, T>>,
        IBinder<IRelayCommand<EventTriggerType, T>>
    {
        [Tooltip("Event that executes the command.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("Parameter passed to the command.")]
        [SerializeField] private T _param;

        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T> _command;
        private IRelayCommand<BaseEventData, T> _dataCommand;
        private IRelayCommand<EventTriggerType, T> _typeCommand;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <summary>
        /// Gets or sets the parameter passed to the command.
        /// </summary>
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        /// <param name="target">The event trigger to bind.</param>
        /// <param name="eventType">The event that executes the command.</param>
        /// <param name="param">The parameter passed to the command.</param>
        /// <param name="customInteractable">
        /// The handler that reflects the command's CanExecute, or <see langword="null"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public EventTriggerCommandBinder(
            EventTrigger target,
            EventTriggerType eventType,
            T param,
            ICanExecuteHandler customInteractable = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventType;
            _param = param;
            _customInteractable = customInteractable;
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<BaseEventData, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _dataCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<EventTriggerType, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _typeCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry { eventID = _event };
            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;
            _lastEvent = null;

            SetValue((IRelayCommand<T>)null);
            SetValue((IRelayCommand<BaseEventData, T>)null);
            SetValue((IRelayCommand<EventTriggerType, T>)null);
        }

        private void OnTrigger(BaseEventData data)
        {
            _lastEvent = data;

            if (_command is not null) _command.Execute(Param);
            else if (_dataCommand is not null) _dataCommand.Execute(data, Param);
            else _typeCommand?.Execute(_event, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<T> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param));
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command when the selected <see cref="EventTrigger"/> event
    /// fires with <see cref="Param1"/>, <see cref="Param2"/>.
    /// </summary>
    /// <remarks>
    /// Accepts a plain command, one that receives the <see cref="BaseEventData"/>, or one that receives the
    /// <see cref="EventTriggerType"/> as its first argument.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    [Serializable]
    public class EventTriggerCommandBinder<T1, T2> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2>>
    {
        [Tooltip("Event that executes the command.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T1, T2> _command;
        private IRelayCommand<BaseEventData, T1, T2> _dataCommand;
        private IRelayCommand<EventTriggerType, T1, T2> _typeCommand;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

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

        /// <param name="target">The event trigger to bind.</param>
        /// <param name="eventType">The event that executes the command.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="customInteractable">
        /// The handler that reflects the command's CanExecute, or <see langword="null"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public EventTriggerCommandBinder(
            EventTrigger target,
            EventTriggerType eventType,
            T1 param1,
            T2 param2,
            ICanExecuteHandler customInteractable = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventType;
            _param1 = param1;
            _param2 = param2;
            _customInteractable = customInteractable;
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<BaseEventData, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _dataCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _typeCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry { eventID = _event };
            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;
            _lastEvent = null;

            SetValue((IRelayCommand<T1, T2>)null);
            SetValue((IRelayCommand<BaseEventData, T1, T2>)null);
            SetValue((IRelayCommand<EventTriggerType, T1, T2>)null);
        }

        private void OnTrigger(BaseEventData data)
        {
            _lastEvent = data;

            if (_command is not null) _command.Execute(Param1, Param2);
            else if (_dataCommand is not null) _dataCommand.Execute(data, Param1, Param2);
            else _typeCommand?.Execute(_event, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param1, Param2));
    }

    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that executes a command when the selected <see cref="EventTrigger"/> event
    /// fires with <see cref="Param1"/>, <see cref="Param2"/>, <see cref="Param3"/>.
    /// </summary>
    /// <remarks>
    /// Accepts a plain command, one that receives the <see cref="BaseEventData"/>, or one that receives the
    /// <see cref="EventTriggerType"/> as its first argument.
    /// </remarks>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    [Serializable]
    public class EventTriggerCommandBinder<T1, T2, T3> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2, T3>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2, T3>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2, T3>>
    {
        [Tooltip("Event that executes the command.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("First parameter passed to the command.")]
        [SerializeField] private T1 _param1;

        [Tooltip("Second parameter passed to the command.")]
        [SerializeField] private T2 _param2;

        [Tooltip("Third parameter passed to the command.")]
        [SerializeField] private T3 _param3;

        [Tooltip("Optional handler that reflects CanExecute.")]
        [SerializeReference] private ICanExecuteHandler _customInteractable;

        private IRelayCommand<T1, T2, T3> _command;
        private IRelayCommand<BaseEventData, T1, T2, T3> _dataCommand;
        private IRelayCommand<EventTriggerType, T1, T2, T3> _typeCommand;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

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

        /// <param name="target">The event trigger to bind.</param>
        /// <param name="eventType">The event that executes the command.</param>
        /// <param name="param1">The parameter passed to the command.</param>
        /// <param name="param2">The parameter passed to the command.</param>
        /// <param name="param3">The parameter passed to the command.</param>
        /// <param name="customInteractable">
        /// The handler that reflects the command's CanExecute, or <see langword="null"/>.
        /// </param>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public EventTriggerCommandBinder(
            EventTrigger target,
            EventTriggerType eventType,
            T1 param1,
            T2 param2,
            T3 param3,
            ICanExecuteHandler customInteractable = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventType;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _customInteractable = customInteractable;
        }

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<BaseEventData, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _dataCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _typeCommand, value, OnCanExecuteChanged);

        /// <inheritdoc/>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry { eventID = _event };
            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;
            _lastEvent = null;

            SetValue((IRelayCommand<T1, T2, T3>)null);
            SetValue((IRelayCommand<BaseEventData, T1, T2, T3>)null);
            SetValue((IRelayCommand<EventTriggerType, T1, T2, T3>)null);
        }

        private void OnTrigger(BaseEventData data)
        {
            _lastEvent = data;

            if (_command is not null) _command.Execute(Param1, Param2, Param3);
            else if (_dataCommand is not null) _dataCommand.Execute(data, Param1, Param2, Param3);
            else _typeCommand?.Execute(_event, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param1, Param2, Param3));
    }
}
