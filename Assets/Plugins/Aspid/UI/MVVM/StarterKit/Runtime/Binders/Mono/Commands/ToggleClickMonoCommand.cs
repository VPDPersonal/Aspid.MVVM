using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(Toggle))]
    public sealed class ToggleClickMonoCommand : MonoCommandAdapter
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Toggle _toggle;
        
        private void Awake()
        {
            if (!_toggle)
                _toggle = GetComponent<Toggle>();
        }

        private void OnEnable() => _toggle.onValueChanged.AddListener(OnValueChanged);

        private void OnDisable() => _toggle.onValueChanged.RemoveListener(OnValueChanged);

        private void OnValueChanged(bool _) => InvokeCommand();
        
        protected override void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_isBindInteractable)
                _toggle.interactable = command.CanExecute();
        }
    }
}