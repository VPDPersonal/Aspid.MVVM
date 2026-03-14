using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{EventTrigger}"/> that executes commands when an <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand"/> (no value),
    /// <see cref="IRelayCommand{T}">IRelayCommand&lt;BaseEventData&gt;</see> (receives the event data),
    /// or <see cref="IRelayCommand{T}">IRelayCommand&lt;EventTriggerType&gt;</see> (receives the event type).
    /// </summary>
    [AddBinderContextMenu(typeof(EventTrigger))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/EventTrigger/EventTrigger Binder – Command")]
    public sealed class EventTriggerCommandMonoBinder : ComponentMonoBinder<EventTrigger>,
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

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            if (_commandWithData is not null) OnCanExecuteChanged(_commandWithData);
            if (_commandWithType is not null) OnCanExecuteChanged(_commandWithType);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;BaseEventData&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T}">IRelayCommand&lt;EventTriggerType&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the component's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <c>OnTrigger</c> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            CachedComponent.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <c>SetValue</c> overloads
        /// to detach command references and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.triggers.Remove(_entry);
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
    /// <see cref="ComponentMonoBinder{EventTrigger}"/> that executes commands with one additional parameter when an <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1}"/>,
    /// <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;BaseEventData, T1&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;EventTriggerType, T1&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    public abstract partial class EventTriggerCommandMonoBinder<T1> : ComponentMonoBinder<EventTrigger>,
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

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            if (_commandWithData is not null) OnCanExecuteChanged(_commandWithData);
            if (_commandWithType is not null) OnCanExecuteChanged(_commandWithType);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;BaseEventData, T1&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}">IRelayCommand&lt;EventTriggerType, T1&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the component's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <c>OnTrigger</c> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            CachedComponent.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <c>SetValue</c> overloads
        /// to detach command references and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.triggers.Remove(_entry);
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
    /// <see cref="ComponentMonoBinder{EventTrigger}"/> that executes commands with two additional parameters when an <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2}"/>,
    /// <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;BaseEventData, T1, T2&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;EventTriggerType, T1, T2&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class EventTriggerCommandMonoBinder<T1, T2> : ComponentMonoBinder<EventTrigger>,
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

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            if (_commandWithData is not null) OnCanExecuteChanged(_commandWithData);
            if (_commandWithType is not null) OnCanExecuteChanged(_commandWithType);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;BaseEventData, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}">IRelayCommand&lt;EventTriggerType, T1, T2&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/> and <see cref="Param2"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the component's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <c>OnTrigger</c> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            CachedComponent.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <c>SetValue</c> overloads
        /// to detach command references and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.triggers.Remove(_entry);
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
    /// <see cref="ComponentMonoBinder{EventTrigger}"/> that executes commands with three additional parameters when an <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3}"/>,
    /// <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;BaseEventData, T1, T2, T3&gt;</see> (receives the event data first),
    /// or <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;EventTriggerType, T1, T2, T3&gt;</see> (receives the event type first).
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class EventTriggerCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<EventTrigger>,
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

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_command is not null) OnCanExecuteChanged(_command);
            if (_commandWithData is not null) OnCanExecuteChanged(_commandWithData);
            if (_commandWithType is not null) OnCanExecuteChanged(_commandWithType);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;BaseEventData, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the <see cref="BaseEventData"/> of the fired event followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}">IRelayCommand&lt;EventTriggerType, T1, T2, T3&gt;</see> and subscribes to its <c>CanExecuteChanged</c> event.
        /// On trigger, the command receives the configured <see cref="EventTriggerType"/> followed by <see cref="Param1"/>, <see cref="Param2"/>, and <see cref="Param3"/>.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the component's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <c>OnTrigger</c> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            CachedComponent.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases all bound command references.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to all <c>SetValue</c> overloads
        /// to detach command references and unsubscribe from their <c>CanExecuteChanged</c> events.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.triggers.Remove(_entry);
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
    /// <see cref="ComponentMonoBinder{EventTrigger}"/> that executes a command with four additional parameters when an <see cref="EventTrigger"/> event fires.
    /// Accepts commands typed as <see cref="IRelayCommand{T1, T2, T3, T4}"/>.
    /// Because this class is abstract, a concrete sealed subclass is required for use.
    /// </summary>
    /// <typeparam name="T1">The type of the first additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T2">The type of the second additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T3">The type of the third additional parameter forwarded when the command is executed.</typeparam>
    /// <typeparam name="T4">The type of the fourth additional parameter forwarded when the command is executed.</typeparam>
    public abstract partial class EventTriggerCommandMonoBinder<T1, T2, T3, T4> : ComponentMonoBinder<EventTrigger>,
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_command is not null) OnCanExecuteChanged(_command);
        }

        /// <summary>
        /// Binds an <see cref="IRelayCommand{T1, T2, T3, T4}"/> and subscribes to its <c>CanExecuteChanged</c> event.
        /// </summary>
        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        /// <summary>
        /// Called when the binder is bound. Creates an <see cref="EventTrigger.Entry"/> for the configured event
        /// and registers it on the component's triggers list.
        /// </summary>
        /// <remarks>
        /// A new <see cref="EventTrigger.Entry"/> is created, <c>OnTrigger</c> is added to its callback,
        /// and the entry is appended to <see cref="EventTrigger.triggers"/>.
        /// </remarks>
        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = _event,
            };

            _entry.callback.AddListener(OnTrigger);
            CachedComponent.triggers.Add(_entry);
        }

        /// <summary>
        /// Called when the binder is unbound. Removes the registered entry from the triggers list
        /// and releases the bound command reference.
        /// </summary>
        /// <remarks>
        /// Removes the <see cref="EventTrigger.Entry"/> from the triggers list, unsubscribes the callback,
        /// clears the entry reference, and passes <see langword="null"/> to <c>SetValue</c>
        /// to detach the command reference and unsubscribe from its <c>CanExecuteChanged</c> event.
        /// </remarks>
        protected override void OnUnbound()
        {
            CachedComponent.triggers.Remove(_entry);
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
