using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Commands;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Binders/UI/Commands/Button Command Binder")]
    public sealed class ButtonCommandMonoBinder : MonoCommandBinder
    {
        [Header("Component")]
        [SerializeField] private Button _button;
        
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;

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
            if (_isBindInteractable)
                _button.interactable = command.CanExecute();
        }
    }
}