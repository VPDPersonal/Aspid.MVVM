using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Slider))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Slider Command Binder")]
    public sealed partial class SliderCommandMonoBinder : MonoCommandBinder<float>, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<double>>
    {
        [Header("Component")]
        [SerializeField] private Slider _slider;
        
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
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
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(_slider.value);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _slider.interactable = interactable; break;
            }
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int> command) =>
            SetValue(new RelayCommand<float>(
                execute: value => command.Execute((int)value), 
                canExecute: value => command.CanExecute((int)value)));

        [BinderLog]
        public void SetValue(IRelayCommand<long> command) =>
            SetValue(new RelayCommand<float>(
                execute: value => command.Execute((int)value), 
                canExecute: value => command.CanExecute((int)value)));

        [BinderLog]
        public void SetValue(IRelayCommand<double> command) =>
            SetValue(new RelayCommand<float>(
                execute: value => command.Execute((int)value), 
                canExecute: value => command.CanExecute((int)value)));
    }
}