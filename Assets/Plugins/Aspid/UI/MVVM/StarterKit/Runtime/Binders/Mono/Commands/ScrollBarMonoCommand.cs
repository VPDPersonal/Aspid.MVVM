using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands
{
    [RequireComponent(typeof(Scrollbar))]
    public sealed class ScrollBarMonoCommand : MonoCommandAdapter<float>
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