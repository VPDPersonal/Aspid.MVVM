using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Button))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Selectable Button Command Binder")]
    public sealed class SelectableButtonCommandMonoBinder : MonoCommandBinder<bool>
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

        private void InvokeCommand() 
            => InvokeCommand(true);
        
        protected override void OnCanExecuteChanged(IRelayCommand<bool> command)
        {
            if (_isBindInteractable)
                _button.interactable = command.CanExecute(true);
        }
    }
}