using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("MVVM/Binders/UI/Commands/Toggle Click Command Binder")]
    public sealed class ToggleClickCommandMonoBinder : MonoCommandBinder
    {
        [Header("Component")]
        [SerializeField] private Toggle _toggle;
        
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
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
            if (_isBindInteractable)
                _toggle.interactable = command.CanExecute();
        }
    }
}