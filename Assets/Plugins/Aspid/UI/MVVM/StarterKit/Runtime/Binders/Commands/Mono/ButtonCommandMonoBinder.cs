using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("UI/Binders/Commands/Button Command Binder")]
    public sealed class ButtonCommandMonoBinder : MonoCommandBinder
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

        protected override void OnCanExecuteChanged(IRelayCommand command)
        {
            if (_isBindInteractable)
                _button.interactable = command.CanExecute();
        }
    }
}