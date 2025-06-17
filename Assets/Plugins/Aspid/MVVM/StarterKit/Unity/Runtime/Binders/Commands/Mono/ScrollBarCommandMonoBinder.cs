using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [RequireComponent(typeof(Scrollbar))]
    [AddPropertyContextMenu(typeof(Scrollbar), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Scrollbar Binder - Command")]
    [AddComponentContextMenu(typeof(Scrollbar),"Add Scrollbar Binder/Scrollbar Binder - Command")]
    public sealed partial class ScrollBarCommandMonoBinder : ComponentMonoBinder<Scrollbar>, IBinder<IRelayCommand<int>>, IBinder<IRelayCommand<long>>, IBinder<IRelayCommand<float>>, IBinder<IRelayCommand<double>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        private IRelayCommand<float> _floatCommand;
        private IRelayCommand<double> _doubleCommand;

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
        
        [BinderLog]
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<double> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, onCanExecuteChanged: OnCanExecuteChanged);

        protected override void OnUnbound()
        {
            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
            SetValue((IRelayCommand<float>)null);
            SetValue((IRelayCommand<double>)null);
        }

        private void Execute(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value);
        }
        
        private void OnCanExecuteChanged<T>(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, T>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue));
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