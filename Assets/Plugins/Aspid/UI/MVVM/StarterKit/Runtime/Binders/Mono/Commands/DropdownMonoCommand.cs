using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(Dropdown))]
    public sealed class DropdownMonoCommand : MonoCommandAdapter<int>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Dropdown _dropdown;
        
        private void Awake()
        {
            if (!_dropdown)
                _dropdown = GetComponent<Dropdown>();
        }

        private void OnEnable() => _dropdown.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => _dropdown.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_isBindInteractable)
                _dropdown.interactable = command.CanExecute(_dropdown.value);
        }
    }
}