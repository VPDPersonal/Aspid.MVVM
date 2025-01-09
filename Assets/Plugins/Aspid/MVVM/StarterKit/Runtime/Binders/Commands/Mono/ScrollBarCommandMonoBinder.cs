using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Scrollbar))]
    [AddComponentMenu("MVVM/Binders/UI/Commands/Scrolbar Command Binder")]
    public sealed class ScrollBarCommandMonoBinder : MonoCommandBinder<float>
    {
        [Header("Component")]
        [SerializeField] private Scrollbar _scrollBar;
        
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
        private void Awake()
        {
            if (!_scrollBar)
                _scrollBar = GetComponent<Scrollbar>();
        }

        private void OnEnable() => 
            _scrollBar.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => 
            _scrollBar.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_isBindInteractable)
                _scrollBar.interactable = command.CanExecute(_scrollBar.value);
        }
    }
}