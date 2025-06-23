#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.Unity;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_Dropdown), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Dropdown Binder - Command")]
    [AddComponentContextMenu(typeof(TMP_Dropdown), "Add Dropdown Binder/Dropdown Binder - Command")]
    public sealed partial class DropdownCommandMonoBinder : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int>>,
        IBinder<IRelayCommand<long>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
        }

        private void OnValueChanged(int value)
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
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class DropdownCommandMonoBinder<T> : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int, T>>,
        IBinder<IRelayCommand<long, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<int, T> _intCommand;
        private IRelayCommand<long, T> _longCommand;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param);
            else _longCommand?.Execute(CachedComponent.value, Param);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class DropdownCommandMonoBinder<T1, T2> : ComponentMonoBinder<TMP_Dropdown>,
        IBinder<IRelayCommand<int, T1, T2>>,
        IBinder<IRelayCommand<long, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<int, T1, T2> _intCommand;
        private IRelayCommand<long, T1, T2> _longCommand;
        
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param1, Param2);
            else _longCommand?.Execute(CachedComponent.value, Param1, Param2);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }

    public abstract partial class DropdownCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<TMP_Dropdown>, 
        IBinder<IRelayCommand<int, T1, T2, T3>>,
        IBinder<IRelayCommand<long, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;

        private IRelayCommand<int, T1, T2, T3> _intCommand;
        private IRelayCommand<long, T1, T2, T3> _longCommand;
        
        public virtual T1 Param1
        {
            get => _param1;
            set => _param1 = value;
        }
        
        public virtual T2 Param2
        {
            get => _param2;
            set => _param2 = value;
        }
        
        public virtual T3 Param3
        {
            get => _param3;
            set => _param3 = value;
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        
        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
        }

        private void OnValueChanged(int value)
        {
            if (_intCommand is not null) _intCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else _longCommand?.Execute(CachedComponent.value, Param1, Param2, Param3);
        }

        private void OnCanExecuteChanged(IRelayCommand<int, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2, Param3));
        }

        private void OnCanExecuteChanged(IRelayCommand<long, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;
            SetInteractableMode(command.CanExecute(CachedComponent.value, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Visible: gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
}
#endif