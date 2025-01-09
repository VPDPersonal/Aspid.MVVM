using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("MVVM/Binders/UI/Commands/Slider Command Binder")]
    public sealed class SliderCommandMonoBinder : MonoCommandBinder<float>
    {
        [Header("Component")]
        [SerializeField] private Slider _slider;
        
        [Header("Parameter")]
        [SerializeField] private bool _isBindInteractable = true;
        
        private void Awake()
        {
            if (!_slider)
                _slider = GetComponent<Slider>();
        }

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() => 
            _slider.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<float> command)
        {
            if (_isBindInteractable)
                _slider.interactable = command.CanExecute(_slider.value);
        }
    }
}