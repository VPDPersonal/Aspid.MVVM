#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Dropdown Command Binder")]
    public sealed partial class DropdownCommandMonoBinder : MonoCommandBinder<int>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<float>>, IBinder<IRelayCommand<double>>
    {
        [Header("Component")]
        [SerializeField] private TMP_Dropdown _dropdown;
        
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        private void Awake()
        {
            if (!_dropdown)
                _dropdown = GetComponent<TMP_Dropdown>();
        }

        private void OnEnable() =>
            _dropdown.onValueChanged.AddListener(InvokeCommand);

        private void OnDisable() =>
            _dropdown.onValueChanged.RemoveListener(InvokeCommand);
        
        protected override void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            var interactable = command.CanExecute(_dropdown.value);
            
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(interactable); break;
                case InteractableMode.Interactable: _dropdown.interactable = interactable; break;
            }
        }

        [BinderLog]
        public void SetValue(IRelayCommand<long> command) =>
            SetValue(new RelayCommand<int>(
                execute: value => command.Execute(value), 
                canExecute: value => command.CanExecute(value)));

        [BinderLog]
        public void SetValue(IRelayCommand<float> command) =>
            SetValue(new RelayCommand<int>(
                execute: value => command.Execute(value), 
                canExecute: value => command.CanExecute(value)));
        
        [BinderLog]
        public void SetValue(IRelayCommand<double> command) =>
            SetValue(new RelayCommand<int>(
                execute: value => command.Execute(value), 
                canExecute: value => command.CanExecute(value)));
    }
}
#endif