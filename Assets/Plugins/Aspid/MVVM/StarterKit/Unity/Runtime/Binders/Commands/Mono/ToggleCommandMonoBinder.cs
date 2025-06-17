using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Toggle))]
    [AddPropertyContextMenu(typeof(Toggle), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Toggle Binder - Command")]
    [AddComponentContextMenu(typeof(Toggle),"Add Toggle Binder/Toggle Binder - Command")]
    public partial class ToggleCommandMonoBinder : ComponentMonoBinder<Toggle>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _isOnCommand;
        
        private void OnEnable() => 
            CachedComponent.onValueChanged.AddListener(Execute);

        private void OnDisable() => 
            CachedComponent.onValueChanged.RemoveListener(Execute);

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _isOnCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        protected override void OnUnbound()
        {
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<bool>)null);
        }
        
        private void Execute(bool isOn)
        {
            if (_command is not null) _command.Execute();
            else _isOnCommand?.Execute(isOn);
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute());
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.isOn));
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