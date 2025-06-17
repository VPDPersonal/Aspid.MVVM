using TMPro;
using System;
using Aspid.MVVM.Unity;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_InputField), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/InputField Binder - Command")]
    [AddComponentContextMenu(typeof(TMP_InputField), "Add InputField Binder/InputField Binder - Command")]
    public sealed partial class InputFieldCommandMonoBinder : ComponentMonoBinder<TMP_InputField>, IBinder<IRelayCommand<string>>
    {
        [Header("Parameters")]
        [SerializeField] private UpdateInputFieldEvent _updateEvent = UpdateInputFieldEvent.OnValueChanged;
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private IRelayCommand<string> _command;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (!Application.isPlaying) return;
            
            CachedComponent.onValueChanged.RemoveListener(Execute);
            CachedComponent.onEndEdit.RemoveListener(Execute);
            CachedComponent.onSubmit.RemoveListener(Execute);
            CachedComponent.onSelect.RemoveListener(Execute);
            CachedComponent.onDeselect.RemoveListener(Execute);
            
            Subscribe();
            
            if (_command is not null)
                OnCanExecuteChanged(_command);
        }

        private void OnEnable() =>
            Subscribe();

        private void OnDisable() =>
            Unsubscribe();
        
        [BinderLog]
        public void SetValue(IRelayCommand<string> value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, onCanExecuteChanged: OnCanExecuteChanged);

        protected override void OnUnbound() =>
            SetValue(null);

        private void Subscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.AddListener(Execute); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.AddListener(Execute); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.AddListener(Execute); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.AddListener(Execute); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.AddListener(Execute); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Unsubscribe()
        {
            switch (_updateEvent)
            {
                case UpdateInputFieldEvent.OnValueChanged: CachedComponent.onValueChanged.RemoveListener(Execute); break;
                case UpdateInputFieldEvent.OnEndEdit: CachedComponent.onEndEdit.RemoveListener(Execute); break;
                case UpdateInputFieldEvent.OnSubmit: CachedComponent.onSubmit.RemoveListener(Execute); break;
                case UpdateInputFieldEvent.OnSelect: CachedComponent.onSelect.RemoveListener(Execute); break;
                case UpdateInputFieldEvent.OnDeselect: CachedComponent.onDeselect.RemoveListener(Execute); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
        
        private void Execute(string value) =>
            _command?.Execute(value);
        
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
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}