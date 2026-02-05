#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/InputField Binder – Command")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_Calls")]
    public sealed partial class InputFieldCommandMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        IBinder<IRelayCommand>,
        IBinder<IRelayCommand<string>>
    {
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand _command;
        private IRelayCommand<string> _stringCommand;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            
            if (_command is not null) OnCanExecuteChanged(_command);
            if (_stringCommand is not null) OnCanExecuteChanged(_stringCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);
        
        [BinderLog]
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
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
            SetInteractableMode(command.CanExecute(CachedComponent.text));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class InputFieldCommandMonoBinder<T> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T>>
    {
        [SerializeField] private T _param;
        
        [Space]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<string, T> _command;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class InputFieldCommandMonoBinder<T1, T2> : ComponentMonoBinder<TMP_InputField>,
        IBinder<IRelayCommand<string, T1, T2>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        [Space]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class InputFieldCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<TMP_InputField>, 
        IBinder<IRelayCommand<string, T1, T2, T3>>
    {
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        [Space]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
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

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            CachedComponent.onEndEdit.RemoveListener(OnValueChanged);
            CachedComponent.onSubmit.RemoveListener(OnValueChanged);
            CachedComponent.onSelect.RemoveListener(OnValueChanged);
            CachedComponent.onDeselect.RemoveListener(OnValueChanged);
            
            Subscribe();
            if (_command is not null) OnCanExecuteChanged(_command);
        }
        
        [BinderLog]
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
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(OnValueChanged); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(OnValueChanged); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnValueChanged(string value) =>
            _command?.Execute(value, Param1, Param2, Param3);
        
        private void OnCanExecuteChanged(IRelayCommand<string, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.text, Param1, Param2, Param3));
        }
        
        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}
#endif