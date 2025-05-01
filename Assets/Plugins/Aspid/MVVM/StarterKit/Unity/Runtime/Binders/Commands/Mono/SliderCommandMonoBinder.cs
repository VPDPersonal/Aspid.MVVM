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