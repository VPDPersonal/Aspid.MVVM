#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class InputFieldCommandBinder: TargetBinder<TMP_InputField>, 
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        [Header("Parameters")]
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        public InputFieldCommandBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        public InputFieldCommandBinder(TMP_InputField target, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, updateEvent, mode) { }
        
        public InputFieldCommandBinder(
            TMP_InputField target, 
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public InputFieldCommandBinder(
            TMP_InputField target, 
            InteractableMode interactableMode,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<string> value) =>
            CommandBinderExtensions.UpdateCommand(ref _stringCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Subscribe();

        protected override void OnUnbound()
        {
            Unsubscribe();
            
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<string>)null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
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
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class InputFieldCommandBinder<T>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T> _command;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        public InputFieldCommandBinder(TMP_InputField target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        public InputFieldCommandBinder(TMP_InputField target, T param, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, updateEvent, mode) { }
        
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T param,
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
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
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        public void SetValue(IRelayCommand<string, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Subscribe();

        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class InputFieldCommandBinder<T1, T2>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T1, T2> _command;
        
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

        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, updateEvent, mode) { }
        
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
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
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        public void SetValue(IRelayCommand<string, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Subscribe();

        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class InputFieldCommandBinder<T1, T2, T3>: TargetBinder<TMP_InputField>, IBinder<IRelayCommand<string, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        [Space]
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T1, T2, T3> _command;
        
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

        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, UpdateInputFieldEvent.OnValueChanged, mode) { }
        
        public InputFieldCommandBinder(TMP_InputField target, T1 param1, T2 param2, T3 param3, UpdateInputFieldEvent updateEvent, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, updateEvent, mode) { }
        
        public InputFieldCommandBinder(
            TMP_InputField target, 
            T1 param1, 
            T2 param2, 
            T3 param3,
            ICanExecuteView customInteractable,
            UpdateInputFieldEvent updateEvent = UpdateInputFieldEvent.OnValueChanged,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _updateEvent =  updateEvent;
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
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
            
            _updateEvent =  updateEvent;
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }

        public void SetValue(IRelayCommand<string, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Subscribe();

        protected override void OnUnbound()
        {
            Unsubscribe();
            SetValue(null);
        }

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: Target.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: Target.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: Target.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: Target.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: Target.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(Target.text, Param1, Param2, Param3));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
}
#endif