#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_Dropdown), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Dropdown Binder - Command")]
    [AddComponentContextMenu(typeof(TMP_Dropdown), "Add Dropdown Binder/Dropdown Binder - Command")]
    public sealed partial class DropdownCommandMonoBinder : ComponentMonoBinder<TMP_Dropdown>, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;

        private void OnEnable() =>
            CachedComponent.onValueChanged.AddListener(Execute);

        private void OnDisable() =>
            CachedComponent.onValueChanged.RemoveListener(Execute);

        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        protected override void OnUnbound()
        {
            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
        }

        private void Execute(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value);
            else _longCommand?.Execute(CachedComponent.value);
        }

        private void OnCanExecuteChanged(IRelayCommand<int> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value));
        }

        private void OnCanExecuteChanged(IRelayCommand<long> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}
#endif