using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Button))]
    [AddPropertyContextMenu(typeof(Button), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Selectable Button Command Binder")]
    [AddComponentContextMenu(typeof(Button),"Add Button Binder/Selectable Button Command Binder")]
    public sealed class SelectableButtonCommandMonoBinder : MonoCommandBinder<bool>
    {
        [Header("Component")]
        [SerializeField] private Button _button;
        
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        private void Awake()
        {
            if (!_button)
                _button = GetComponent<Button>();
        }

        private void OnEnable() =>
            _button.onClick.AddListener(InvokeCommand);

        private void OnDisable() =>
            _button.onClick.RemoveListener(InvokeCommand);

        private void InvokeCommand() 
            => InvokeCommand(true);
        
        protected override void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(true);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _button.interactable = interactable; break;
            }
        }
    }
}