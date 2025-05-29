using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Toggle Click Command Binder")]
    public sealed class ToggleClickCommandMonoBinder : MonoCommandBinder
    {
        [Header("Component")]
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private void Awake()
        {
            if (!_toggle)
                _toggle = GetComponent<Toggle>();
        }

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(OnValueChanged);

        private void OnDisable() => 
            _toggle.onValueChanged.RemoveListener(OnValueChanged);

        private void OnValueChanged(bool _) 
            => InvokeCommand();
        
        protected override void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute();
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _toggle.interactable = interactable; break;
            }
        }
    }
}