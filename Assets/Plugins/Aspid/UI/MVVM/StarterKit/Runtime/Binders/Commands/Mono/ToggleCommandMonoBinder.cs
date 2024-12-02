using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Toggle))]
    [AddComponentMenu("UI/Binders/Commands/Toggle Command Binder")]
    public sealed class ToggleCommandMonoBinder : MonoCommandBinder<bool>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Toggle _toggle;
        
        private void Awake()
        {
            if (!_toggle)
                _toggle = GetComponent<Toggle>();
        }

        private void OnEnable() => _toggle.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => _toggle.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_isBindInteractable)
                _toggle.interactable = command.CanExecute(_toggle.isOn);
        }
    }
}