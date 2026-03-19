using System;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{EventTrigger}"/> that executes commands when a configured <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/> (no value),
    /// <see cref="IRelayCommand{T}">IRelayCommand&lt;BaseEventData&gt;</see> (receives the event data),
    /// or <see cref="IRelayCommand{T}">IRelayCommand&lt;EventTriggerType&gt;</see> (receives the event type).
    /// </summary>
    /// <include file="XmlExampleDoc-EventTrigger-Command-1.1.0.xml" path="doc//member[@name='EventTriggerCommandBinder']/*" />
    [Serializable]
    public sealed class EventTriggerCommandBinder : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<BaseEventData>>,
        IBinder<IRelayCommand<EventTriggerType>>
    {
        [Tooltip("The EventTrigger event type that triggers command execution.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<BaseEventData> _commandWithData;
        private IRelayCommand<EventTriggerType> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <summary>
        /// Initializes a new instance of <see cref="EventTriggerCommandBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="EventTrigger"/> to bind.</param>
        /// <param name="eventTriggerType">The <see cref="EventTriggerType"/> event that triggers command execution.</param>
        /// <param name="customInteractable">An optional custom view that reflects the command's <see cref="IRelayCommand.CanExecute(object)"/> state. Pass <see langword="null"/> to disable interactable feedback.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public EventTriggerCommandBinder(
            EventTrigger target,
            EventTriggerType eventTriggerType,
            ICanExecuteView customInteractable = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _event = eventTriggerType;
            _customInteractable = customInteractable;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;BaseEventData&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;EventTriggerType&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the target's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <see cref="OnTrigger(BaseEventData)"/> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <see cref="SetValue"/> overloads
        /// to detach command references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;

            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<BaseEventData>)null);
            SetValue((IRelayCommand<EventTriggerType>)null);
        }

        private void OnTrigger(BaseEventData e)
        {
            _lastEvent = e;

            _command?.Execute();
            _commandWithData?.Execute(e);
            _commandWithType?.Execute(_event);
        }

        private void OnCanExecuteChanged(IRelayCommand command) =>
            _customInteractable?.SetCanExecute(command.CanExecute());

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(param: _lastEvent));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(param: _event));
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{EventTrigger}"/> that executes commands with one additional parameter when a configured <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1}"/>,
    /// <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;BaseEventData, T1&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;EventTriggerType, T1&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-EventTrigger-Command-1.1.0.xml" path="doc//member[@name='EventTriggerCommandBinder{1}']/*" />
    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1>>,
        IBinder<IRelayCommand<BaseEventData, T1>>,
        IBinder<IRelayCommand<EventTriggerType, T1>>
    {
        [Tooltip("The EventTrigger event type that triggers command execution.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("The additional parameter forwarded when the command is executed.")]
        [SerializeField] private T1 _param1;

        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1> _command;
        private IRelayCommand<BaseEventData, T1> _commandWithData;
        private IRelayCommand<EventTriggerType, T1> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <summary>
        /// Gets or sets the additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventTriggerCommandBinder{T1}"/>.
        /// </summary>
        /// <param name="target">The <see cref="EventTrigger"/> to bind.</param>
        /// <param name="eventTriggerType">The <see cref="EventTriggerType"/> event that triggers command execution.</param>
        /// <param name="param1">The additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">An optional custom view that reflects the command's <see cref="IRelayCommand.CanExecute(object)"/> state. Pass <see langword="null"/> to disable interactable feedback.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public EventTriggerCommandBinder(EventTrigger target, EventTriggerType eventTriggerType, T1 param1, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventTriggerType;
            _param1 = param1;
            _customInteractable = customInteractable;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;BaseEventData, T1&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;EventTriggerType, T1&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the target's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <see cref="OnTrigger(BaseEventData)"/> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <see cref="SetValue"/> overloads
        /// to detach command references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;

            SetValue((IRelayCommand<T1>)null);
            SetValue((IRelayCommand<BaseEventData, T1>)null);
            SetValue((IRelayCommand<EventTriggerType, T1>)null);
        }

        private void OnTrigger(BaseEventData e)
        {
            _lastEvent = e;

            _command?.Execute(Param1);
            _commandWithData?.Execute(e, Param1);
            _commandWithType?.Execute(_event, Param1);
        }

        private void OnCanExecuteChanged(IRelayCommand<T1> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T1> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param1));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T1> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param1));
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{EventTrigger}"/> that executes commands with two additional parameters when a configured <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}"/>,
    /// <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;BaseEventData, T1, T2&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;EventTriggerType, T1, T2&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-EventTrigger-Command-1.1.0.xml" path="doc//member[@name='EventTriggerCommandBinder{2}']/*" />
    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2>>
    {
        [Tooltip("The EventTrigger event type that triggers command execution.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("The first additional parameter forwarded when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded when the command is executed.")]
        [SerializeField] private T2 _param2;

        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2> _command;
        private IRelayCommand<BaseEventData, T1, T2> _commandWithData;
        private IRelayCommand<EventTriggerType, T1, T2> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventTriggerCommandBinder{T1, T2}"/>.
        /// </summary>
        /// <param name="target">The <see cref="EventTrigger"/> to bind.</param>
        /// <param name="eventTriggerType">The <see cref="EventTriggerType"/> event that triggers command execution.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">An optional custom view that reflects the command's <see cref="IRelayCommand.CanExecute(object)"/> state. Pass <see langword="null"/> to disable interactable feedback.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public EventTriggerCommandBinder(EventTrigger target, EventTriggerType eventTriggerType, T1 param1, T2 param2, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventTriggerType;
            _param1 = param1;
            _param2 = param2;

            _customInteractable = customInteractable;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;BaseEventData, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;EventTriggerType, T1, T2&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the target's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <see cref="OnTrigger(BaseEventData)"/> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <see cref="SetValue"/> overloads
        /// to detach command references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;

            SetValue((IRelayCommand<T1, T2>)null);
            SetValue((IRelayCommand<BaseEventData, T1, T2>)null);
            SetValue((IRelayCommand<EventTriggerType, T1, T2>)null);
        }

        private void OnTrigger(BaseEventData e)
        {
            _lastEvent = e;

            _command?.Execute(Param1, Param2);
            _commandWithData?.Execute(e, Param1, Param2);
            _commandWithType?.Execute(_event, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param1, Param2));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T1, T2> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param1, Param2));
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{EventTrigger}"/> that executes commands with three additional parameters when a configured <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}"/>,
    /// <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;BaseEventData, T1, T2, T3&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;EventTriggerType, T1, T2, T3&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-EventTrigger-Command-1.1.0.xml" path="doc//member[@name='EventTriggerCommandBinder{3}']/*" />
    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2, T3> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2, T3>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2, T3>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2, T3>>
    {
        [Tooltip("The EventTrigger event type that triggers command execution.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("The first additional parameter forwarded when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded when the command is executed.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third additional parameter forwarded when the command is executed.")]
        [SerializeField] private T3 _param3;

        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2, T3> _command;
        private IRelayCommand<BaseEventData, T1, T2, T3> _commandWithData;
        private IRelayCommand<EventTriggerType, T1, T2, T3> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the third additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventTriggerCommandBinder{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="target">The <see cref="EventTrigger"/> to bind.</param>
        /// <param name="eventTriggerType">The <see cref="EventTriggerType"/> event that triggers command execution.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">An optional custom view that reflects the command's <see cref="IRelayCommand.CanExecute(object)"/> state. Pass <see langword="null"/> to disable interactable feedback.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public EventTriggerCommandBinder(EventTrigger target, EventTriggerType eventTriggerType, T1 param1, T2 param2, T3 param3, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventTriggerType;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;

            _customInteractable = customInteractable;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;BaseEventData, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;EventTriggerType, T1, T2, T3&gt;</see> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the target's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <see cref="OnTrigger(BaseEventData)"/> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <see cref="SetValue"/> overloads
        /// to detach command references and unsubscribe from their <see cref="IRelayCommand.CanExecuteChanged"/> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;

            SetValue((IRelayCommand<T1, T2, T3>)null);
            SetValue((IRelayCommand<BaseEventData, T1, T2, T3>)null);
            SetValue((IRelayCommand<EventTriggerType, T1, T2, T3>)null);
        }

        private void OnTrigger(BaseEventData e)
        {
            _lastEvent = e;

            _command?.Execute(Param1, Param2, Param3);
            _commandWithData?.Execute(e, Param1, Param2, Param3);
            _commandWithType?.Execute(_event, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<BaseEventData, T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_lastEvent, Param1, Param2, Param3));

        private void OnCanExecuteChanged(IRelayCommand<EventTriggerType, T1, T2, T3> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(_event, Param1, Param2, Param3));
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{EventTrigger}"/> that executes a command with four additional parameters when a configured <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T4">The type of the fourth additional parameter forwarded when the command is executed.</typeparam>
    /// <include file="XmlExampleDoc-EventTrigger-Command-1.1.0.xml" path="doc//member[@name='EventTriggerCommandBinder{4}']/*" />
    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2, T3, T4> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [Tooltip("The EventTrigger event type that triggers command execution.")]
        [SerializeField] private EventTriggerType _event;

        [Tooltip("The first additional parameter forwarded when the command is executed.")]
        [SerializeField] private T1 _param1;
        [Tooltip("The second additional parameter forwarded when the command is executed.")]
        [SerializeField] private T2 _param2;
        [Tooltip("The third additional parameter forwarded when the command is executed.")]
        [SerializeField] private T3 _param3;
        [Tooltip("The fourth additional parameter forwarded when the command is executed.")]
        [SerializeField] private T4 _param4;

        [Tooltip("The view used to reflect the command's CanExecute state.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private EventTrigger.Entry _entry;
        private IRelayCommand<T1, T2, T3, T4> _command;

        /// <summary>
        /// Gets or sets the first additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        /// <summary>
        /// Gets or sets the second additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        /// <summary>
        /// Gets or sets the third additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        /// <summary>
        /// Gets or sets the fourth additional parameter forwarded when the command is executed.
        /// </summary>
        public virtual T4 Param4
        {
            get => _param4;
            set => _param4 = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EventTriggerCommandBinder{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="target">The <see cref="EventTrigger"/> to bind.</param>
        /// <param name="eventTriggerType">The <see cref="EventTriggerType"/> event that triggers command execution.</param>
        /// <param name="param1">The first additional parameter forwarded when the command is executed.</param>
        /// <param name="param2">The second additional parameter forwarded when the command is executed.</param>
        /// <param name="param3">The third additional parameter forwarded when the command is executed.</param>
        /// <param name="param4">The fourth additional parameter forwarded when the command is executed.</param>
        /// <param name="customInteractable">An optional custom view that reflects the command's <see cref="IRelayCommand.CanExecute(object)"/> state. Pass <see langword="null"/> to disable interactable feedback.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public EventTriggerCommandBinder(EventTrigger target, EventTriggerType eventTriggerType, T1 param1, T2 param2, T3 param3, T4 param4, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _event = eventTriggerType;
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;

            _customInteractable = customInteractable;
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and subscribes to its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the target's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <see cref="OnTrigger(BaseEventData)"/> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to <see cref="SetValue"/>
        /// to detach the command reference and unsubscribe from its <see cref="IRelayCommand.CanExecuteChanged"/> event.
        /// </remarks>
        protected override void OnUnbound()
        {
            Target.triggers.Remove(_entry);
            _entry.callback.RemoveListener(OnTrigger);
            _entry = null;

            SetValue(null);
        }

        private void OnTrigger(BaseEventData e) =>
            _command?.Execute(Param1, Param2, Param3, Param4);

        private void OnCanExecuteChanged(IRelayCommand<T1, T2, T3, T4> command) =>
            _customInteractable?.SetCanExecute(command.CanExecute(Param1, Param2, Param3, Param4));
    }
}
