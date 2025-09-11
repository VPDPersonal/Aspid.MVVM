using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class ScrollRectCommandBinder : TargetBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2>>, 
        IBinder<IRelayCommand<Vector3>>
    {
        [Header("Parameter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _interactable;
        
        private IRelayCommand<Vector2> _vector2Command;
        private IRelayCommand<Vector3> _vector3Command;
        
        public override bool IsBind => Target is not null;

        public ScrollRectCommandBinder(ScrollRect target, ICanExecuteView interactable, BindMode mode = BindMode.OneWay)
            : this(target, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }
        
        public ScrollRectCommandBinder(ScrollRect target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }
        
        public void SetValue(IRelayCommand<Vector2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<Vector3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);
        
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2>)null);
            SetValue((IRelayCommand<Vector3>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value);
            else _vector3Command?.Execute(value);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition));
    }

    [Serializable]
    public class ScrollRectCommandBinder<T> :  TargetBinder<ScrollRect>, 
        IBinder<IRelayCommand<Vector2, T>>, 
        IBinder<IRelayCommand<Vector3, T>>
    {
        [Header("Parameter")]
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
        
        public override bool IsBind => Target is not null;
        
        public ScrollRectCommandBinder(
            ScrollRect target,
            T param,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }
        
        public ScrollRectCommandBinder(ScrollRect target, T param, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _param = param;
        }
        
        public void SetValue(IRelayCommand<Vector2, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<Vector3, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);
        
        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T>)null);
            SetValue((IRelayCommand<Vector3, T>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param);
            else _vector3Command?.Execute(value, Param);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param));
    }
    
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2> :  TargetBinder<ScrollRect>, 
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
        
        public override bool IsBind => Target is not null;
        
        public ScrollRectCommandBinder(
            ScrollRect target,
            T1 param1,
            T2 param2,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }
        
        public ScrollRectCommandBinder(ScrollRect target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
        }
        
        public void SetValue(IRelayCommand<Vector2, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<Vector3, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T1, T2>)null);
            SetValue((IRelayCommand<Vector3, T1, T2>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2);
            else _vector3Command?.Execute(value, Param1, Param2);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2));
    }
    
    [Serializable]
    public class ScrollRectCommandBinder<T1, T2, T3> :  TargetBinder<ScrollRect>,
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
        
        public override bool IsBind => Target is not null;
        
        public ScrollRectCommandBinder(
            ScrollRect target,
            T1 param1,
            T2 param2,
            T3 param3,
            ICanExecuteView interactable,
            BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, mode)
        {
            _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));
        }
        
        public ScrollRectCommandBinder(
            ScrollRect target, 
            T1 param1, 
            T2 param2,
            T3 param3, 
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
        }
        
        public void SetValue(IRelayCommand<Vector2, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector2Command, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<Vector3, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _vector3Command, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<Vector2, T1, T2, T3>)null);
            SetValue((IRelayCommand<Vector3, T1, T2, T3>)null);
        }
        
        private void OnValueChanged(Vector2 value)
        {
            if (_vector2Command is not null) _vector2Command.Execute(value, Param1, Param2, Param3);
            else _vector3Command?.Execute(value, Param1, Param2, Param3);
        }
        
        private void OnCanExecuteChanged(IRelayCommand<Vector2, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2, Param3));
        
        private void OnCanExecuteChanged(IRelayCommand<Vector3, T1, T2, T3> command) =>
            _interactable?.SetCanExecute(command.CanExecute(Target.normalizedPosition, Param1, Param2, Param3));
    }
}