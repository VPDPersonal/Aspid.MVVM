using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Scrollbar), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/Scrollbar Binder - Command")]
    [AddComponentContextMenu(typeof(Scrollbar),"Add Scrollbar Binder/Scrollbar Binder - Command")]
    public sealed partial class ScrollBarCommandMonoBinder : ComponentMonoBinder<Scrollbar>, 
        IBinder<IRelayCommand<int>>, 
        IBinder<IRelayCommand<long>>, 
        IBinder<IRelayCommand<float>>, 
        IBinder<IRelayCommand<double>>
    {
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        private IRelayCommand<float> _floatCommand;
        private IRelayCommand<double> _doubleCommand;

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_intCommand is not null) OnCanExecuteChanged(_intCommand);
            else if (_longCommand is not null) OnCanExecuteChanged(_longCommand);
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        [BinderLog]
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<double> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
            SetValue((IRelayCommand<float>)null);
            SetValue((IRelayCommand<double>)null);
        }

        private void OnValueChanged(float value)
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
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
                case InteractableMode.Interactable: CachedComponent.interactable = isInteractable; break;
            }
        }
    }
    
    public abstract partial class ScrollBarCommandMonoBinder<T> : ComponentMonoBinder<Scrollbar>, 
        IBinder<IRelayCommand<int, T>>, 
        IBinder<IRelayCommand<long, T>>, 
        IBinder<IRelayCommand<float, T>>, 
        IBinder<IRelayCommand<double, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        [Space]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;

        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int, T> _intCommand;
        private IRelayCommand<long, T> _longCommand;
        private IRelayCommand<float, T> _floatCommand;
        private IRelayCommand<double, T> _doubleCommand;
        
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
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        [BinderLog]
        public void SetValue(IRelayCommand<float, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<double, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
            SetValue((IRelayCommand<float, T>)null);
            SetValue((IRelayCommand<double, T>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param));
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
    
    public abstract partial class ScrollBarCommandMonoBinder<T1, T2> : ComponentMonoBinder<Scrollbar>, 
        IBinder<IRelayCommand<int, T1, T2>>, 
        IBinder<IRelayCommand<long, T1, T2>>, 
        IBinder<IRelayCommand<float, T1, T2>>, 
        IBinder<IRelayCommand<double, T1, T2>>
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
        private IRelayCommand<float, T1, T2> _floatCommand;
        private IRelayCommand<double, T1, T2> _doubleCommand;
        
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
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<double, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
            SetValue((IRelayCommand<float, T1, T2>)null);
            SetValue((IRelayCommand<double, T1, T2>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param1, Param2);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param1, Param2);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param1, Param2);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param1, Param2);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2));
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
    
    public abstract partial class ScrollBarCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<Scrollbar>, 
        IBinder<IRelayCommand<int, T1, T2, T3>>, 
        IBinder<IRelayCommand<long, T1, T2, T3>>, 
        IBinder<IRelayCommand<float, T1, T2, T3>>, 
        IBinder<IRelayCommand<double, T1, T2, T3>>
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
        private IRelayCommand<float, T1, T2, T3> _floatCommand;
        private IRelayCommand<double, T1, T2, T3> _doubleCommand;
        
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
            else if (_floatCommand is not null) OnCanExecuteChanged(_floatCommand);
            else if (_doubleCommand is not null) OnCanExecuteChanged(_doubleCommand);
        }

        [BinderLog]
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        [BinderLog]
        public void SetValue(IRelayCommand<float, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<double, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
            SetValue((IRelayCommand<float, T1, T2, T3>)null);
            SetValue((IRelayCommand<double, T1, T2, T3>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else if (_intCommand is not null) _intCommand.Execute((int)CachedComponent.value, Param1, Param2, Param3);
            else if (_doubleCommand is not null) _doubleCommand.Execute(CachedComponent.value, Param1, Param2, Param3);
            else if (_longCommand is not null) _longCommand.Execute((long)CachedComponent.value, Param1, Param2, Param3);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = CachedComponent.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2, Param3));
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