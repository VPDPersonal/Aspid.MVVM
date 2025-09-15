using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(ScrollRect), "m_Calls")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Commands/ScrollRect Binder - Command")]
    [AddComponentContextMenu(typeof(ScrollRect),"Add ScrollRect Binder/ScrollRect Binder - Command")]
    public sealed partial class ScrollRectCommandMonoBinder : ComponentMonoBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2>>, 
        IBinder<IRelayCommand<Vector3>>
    {
        [Header("Parameter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;
        
        private IRelayCommand<Vector2> _vector2Command;
        private IRelayCommand<Vector3> _vector3Command;

        [BinderLog]
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2>)null);
            SetValue((IRelayCommand<Vector3>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value);
            else _vector3Command?.Execute(value);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition));
    }
    
    public abstract partial class ScrollRectCommandMonoBinder<T> : ComponentMonoBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2, T>>, 
        IBinder<IRelayCommand<Vector3, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        [Space]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;
        
        private IRelayCommand<Vector2, T> _vector2Command;
        private IRelayCommand<Vector3, T> _vector3Command;
        
        public virtual T Param
        {
            get => _param;
            set => _param = value;
        }

        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T>)null);
            SetValue((IRelayCommand<Vector3, T>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param);
            else _vector3Command?.Execute(value, Param);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param));
    }
    
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2> : ComponentMonoBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2, T1, T2>>, 
        IBinder<IRelayCommand<Vector3, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        [Space]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;
        
        private IRelayCommand<Vector2, T1, T2> _vector2Command;
        private IRelayCommand<Vector3, T1, T2> _vector3Command;
        
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

        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T1, T2>)null);
            SetValue((IRelayCommand<Vector3, T1, T2>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2);
            else _vector3Command?.Execute(value, Param1, Param2);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2));
    }
    
    public abstract partial class ScrollRectCommandMonoBinder<T1, T2, T3> : ComponentMonoBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2, T1, T2, T3>>, 
        IBinder<IRelayCommand<Vector3, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        [Space]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;
        
        private IRelayCommand<Vector2, T1, T2, T3> _vector2Command;
        private IRelayCommand<Vector3, T1, T2, T3> _vector3Command;
        
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

        [BinderLog]
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);

        [BinderLog]
        public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            CachedComponent.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T1, T2, T3>)null);
            SetValue((IRelayCommand<Vector3, T1, T2, T3>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2, Param3);
            else _vector3Command?.Execute(value, Param1, Param2, Param3);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(CachedComponent.normalizedPosition, Param1, Param2, Param3));
    }
}