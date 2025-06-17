using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Button), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Button Binder - Command")]
    [AddComponentContextMenu(typeof(Button),"Add Button Binder/Button Binder - Command")]
    public sealed partial class ButtonCommandMonoBinder : ComponentMonoBinder<Button>, IBinder<IRelayCommand>, IBinder<IRelayCommand<bool>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private IRelayCommand _command;
        private IRelayCommand<bool> _selectableCommand;
        
        private void OnEnable() => 
            CachedComponent.onClick.AddListener(Execute);

        private void OnDisable() => 
            CachedComponent.onClick.RemoveListener(Execute);

        [BinderLog]
        public void SetValue(IRelayCommand value) =>
            CommandBinderExtensions.UpdateCommand(ref _command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<bool> value) =>
            CommandBinderExtensions.UpdateCommand(ref _selectableCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        protected override void OnUnbound()
        {
            SetValue((IRelayCommand)null);
            SetValue((IRelayCommand<bool>)null);
        }
        
        private void Execute()
        {
            if (_command is not null) _command.Execute();
            else _selectableCommand?.Execute(true);
        }
        
        private void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute());
        }
        
        private void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(true));
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