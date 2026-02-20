using System;
using UnityEngine;
using UnityEngine.EventSystems;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class EventTriggerCommandBinder : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<BaseEventData>>,
        IBinder<IRelayCommand<EventTriggerType>>
    {
        [SerializeField] private EventTriggerType _event;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand _command;
        private IRelayCommand<BaseEventData> _commandWithData;
        private IRelayCommand<EventTriggerType> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        public EventTriggerCommandBinder(EventTrigger target, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _customInteractable = customInteractable;
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

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

    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1>>,
        IBinder<IRelayCommand<BaseEventData, T1>>,
        IBinder<IRelayCommand<EventTriggerType, T1>>
    {
        [SerializeField] private EventTriggerType _event;

        [SerializeField] private T1 _param1;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1> _command;
        private IRelayCommand<BaseEventData, T1> _commandWithData;
        private IRelayCommand<EventTriggerType, T1> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        public EventTriggerCommandBinder(EventTrigger target, T1 param1, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _customInteractable = customInteractable;
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

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

    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2>>
    {
        [SerializeField] private EventTriggerType _event;

        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2> _command;
        private IRelayCommand<BaseEventData, T1, T2> _commandWithData;
        private IRelayCommand<EventTriggerType, T1, T2> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        public EventTriggerCommandBinder(EventTrigger target, T1 param1, T2 param2, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;

            _customInteractable = customInteractable;
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

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

    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2, T3> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2, T3>>,
        IBinder<IRelayCommand<BaseEventData, T1, T2, T3>>,
        IBinder<IRelayCommand<EventTriggerType, T1, T2, T3>>
    {
        [SerializeField] private EventTriggerType _event;

        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<T1, T2, T3> _command;
        private IRelayCommand<BaseEventData, T1, T2, T3> _commandWithData;
        private IRelayCommand<EventTriggerType, T1, T2, T3> _commandWithType;

        private BaseEventData _lastEvent;
        private EventTrigger.Entry _entry;

        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        public EventTriggerCommandBinder(EventTrigger target, T1 param1, T2 param2, T3 param3, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;

            _customInteractable = customInteractable;
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<BaseEventData, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithData, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<EventTriggerType, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _commandWithType, value, OnCanExecuteChanged);

        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

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

    [Serializable]
    public abstract partial class EventTriggerCommandBinder<T1, T2, T3, T4> : TargetBinder<EventTrigger>,
        IBinder<IRelayCommand<T1, T2, T3, T4>>
    {
        [SerializeField] private EventTriggerType _event;

        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        [SerializeField] private T4 _param4;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private EventTrigger.Entry _entry;
        private IRelayCommand<T1, T2, T3, T4> _command;

        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }

        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }

        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }

        public virtual T4 Param4
        {
            get => _param4;
            set => _param4 = value;
        }

        public EventTriggerCommandBinder(EventTrigger target, T1 param1, T2 param2, T3 param3, T4 param4, ICanExecuteView customInteractable = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            _param4 = param4;

            _customInteractable = customInteractable;
        }

        [BinderLog]
        public void SetValue(IRelayCommand<T1, T2, T3, T4> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound()
        {
            _entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter,
            };

            _entry.callback.AddListener(OnTrigger);
            Target.triggers.Add(_entry);
        }

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