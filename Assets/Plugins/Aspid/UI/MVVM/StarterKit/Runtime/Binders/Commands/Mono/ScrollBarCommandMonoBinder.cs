using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Scrollbar))]
    [AddComponentMenu("UI/Binders/Commands/Scrolbar Command Binder")]
    public sealed class ScrollBarCommandMonoBinder : MonoCommandBinder<float>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Scrollbar _scrollBar;
        
        private void Awake()
        {
            if (!_scrollBar)
                _scrollBar = GetComponent<Scrollbar>();
        }

        private void OnEnable() => _scrollBar.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => _scrollBar.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_isBindInteractable)
                _scrollBar.interactable = command.CanExecute(_scrollBar.value);
        }
    }
}