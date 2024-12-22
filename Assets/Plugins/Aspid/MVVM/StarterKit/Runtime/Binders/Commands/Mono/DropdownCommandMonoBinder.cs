using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Dropdown))]
    [AddComponentMenu("Binders/UI/Commands/Dropdown Command Binder")]
    public sealed class DropdownCommandMonoBinder : MonoCommandBinder<int>
    {
        [Header("Component")]
        [SerializeField] private Dropdown _dropdown;
        
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
        private void Awake()
        {
            if (!_dropdown)
                _dropdown = GetComponent<Dropdown>();
        }

        private void OnEnable() =>
            _dropdown.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() =>
            _dropdown.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_isBindInteractable)
                _dropdown.interactable = command.CanExecute(_dropdown.value);
        }
    }
}