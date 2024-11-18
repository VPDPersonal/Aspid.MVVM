using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Commands.Adapters
{
    [RequireComponent(typeof(Slider))]
    public sealed class SliderMonoCommand : MonoCommandAdapter<float>
    {
        [SerializeField] private bool _isBindInteractable;
        [SerializeField] private Slider _slider;
        
        private void Awake()
        {
            if (!_slider)
                _slider = GetComponent<Slider>();
        }

        private void OnEnable() => _slider.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => _slider.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_isBindInteractable)
                _slider.interactable = command.CanExecute(_slider.value);
        }
    }
}