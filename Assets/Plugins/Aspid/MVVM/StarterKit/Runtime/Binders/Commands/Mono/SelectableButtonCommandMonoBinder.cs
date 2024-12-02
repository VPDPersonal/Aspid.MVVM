using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("UI/Binders/Commands/Selectable Button Command Binder")]
    public sealed class SelectableButtonCommandMonoBinder : MonoCommandBinder<bool>
    {
        [SerializeField] private bool _isBindInteractable = true;
        [SerializeField] private Button _button;

        private void Awake()
        {
            if (!_button)
                _button = GetComponent<Button>();
        }

        private void OnEnable() => _button.onClick.AddListener(InvokeCommand);

        private void OnDisable() => _button.onClick.RemoveListener(InvokeCommand);

        private void InvokeCommand() => InvokeCommand(true);
        
        protected override void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_isBindInteractable)
                _button.interactable = command.CanExecute(true);
        }
    }
}