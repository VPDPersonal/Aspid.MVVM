using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Button Command Binder")]
    public sealed class ButtonCommandMonoBinder : MonoCommandBinder
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

        protected override void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute();
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _button.interactable = interactable; break;
            }
        }
    }
}