using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Scrollbar))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Scrollbar Command Binder")]
    public sealed partial class ScrollBarCommandMonoBinder : MonoCommandBinder<float>, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<double>>
    {
        [Header("Component")]
        [SerializeField] private Scrollbar _scrollBar;
        
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
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
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(_scrollBar.value);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _scrollBar.interactable = interactable; break;
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