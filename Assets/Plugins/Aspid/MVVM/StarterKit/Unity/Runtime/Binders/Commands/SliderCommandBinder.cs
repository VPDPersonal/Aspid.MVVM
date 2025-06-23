using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class SliderCommandBinder : TargetBinder<Slider>, IBinder<IRelayCommand<float>>
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameter")]
        [SerializeField] private InteractableMode _interactableMode = InteractableMode.Interactable;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private ICanExecuteView _customInteractable;
        
        private IRelayCommand<int> _intCommand;
        private IRelayCommand<long> _longCommand;
        private IRelayCommand<float> _floatCommand;
        private IRelayCommand<double> _doubleCommand;
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : this(target, InteractableMode.Custom, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public SliderCommandBinder(Slider target, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<int> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<long> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<float> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<double> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int>)null);
            SetValue((IRelayCommand<long>)null);
            SetValue((IRelayCommand<float>)null);
            SetValue((IRelayCommand<double>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(Target.value);
            else if (_intCommand is not null) _intCommand.Execute((int)Target.value);
            else if (_doubleCommand is not null) _doubleCommand.Execute(Target.value);
            else if (_longCommand is not null) _longCommand.Execute((long)Target.value);
        }
        
        private void OnCanExecuteChanged<T>(IRelayCommand<T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = Target.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, T>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }

    [Serializable]
    public class SliderCommandBinder<T> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T>>
    {
        [Header("Parameters")]
        [SerializeField] private T _param;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, T param, BindMode mode = BindMode.OneWay)
            : this(target, param, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, T param, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public SliderCommandBinder(Slider target, T param, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param = param;
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<int, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<long, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<float, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<double, T> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T>)null);
            SetValue((IRelayCommand<long, T>)null);
            SetValue((IRelayCommand<float, T>)null);
            SetValue((IRelayCommand<double, T>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(Target.value, Param);
            else if (_intCommand is not null) _intCommand.Execute((int)Target.value, Param);
            else if (_doubleCommand is not null) _doubleCommand.Execute(Target.value, Param);
            else if (_longCommand is not null) _longCommand.Execute((long)Target.value, Param);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = Target.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class SliderCommandBinder<T1, T2> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T1, T2>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, InteractableMode.Interactable, mode) { }
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;     
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<int, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<long, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<float, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<double, T1, T2> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2>)null);
            SetValue((IRelayCommand<long, T1, T2>)null);
            SetValue((IRelayCommand<float, T1, T2>)null);
            SetValue((IRelayCommand<double, T1, T2>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(Target.value, Param1, Param2);
            else if (_intCommand is not null) _intCommand.Execute((int)Target.value, Param1, Param2);
            else if (_doubleCommand is not null) _doubleCommand.Execute(Target.value, Param1, Param2);
            else if (_longCommand is not null) _longCommand.Execute((long)Target.value, Param1, Param2);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = Target.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
    
    [Serializable]
    public class SliderCommandBinder<T1, T2, T3> : TargetBinder<Slider>, IBinder<IRelayCommand<float, T1, T2, T3>>
    {
        [Header("Parameters")]
        [SerializeField] private T1 _param1;
        [SerializeField] private T2 _param2;
        [SerializeField] private T3 _param3;
        
        // ReSharper disable once MemberInitializerValueIgnored
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
        
        public override bool IsBind => Target is not null;
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, T3 param3, BindMode mode = BindMode.OneWay)
            : this(target, param1, param2, param3, InteractableMode.Interactable, mode) { }

        public SliderCommandBinder(Slider target, T1 param1, T2 param2, T3 param3, ICanExecuteView customInteractable, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;
            
            _interactableMode = InteractableMode.Custom;
            _customInteractable = customInteractable ?? throw new ArgumentNullException(nameof(customInteractable));
        }
        
        public SliderCommandBinder(Slider target, T1 param1, T2 param2, T3 param3, InteractableMode interactableMode, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _param1 = param1;
            _param2 = param2;
            _param3 = param3;         
            
            _interactableMode = interactableMode is not InteractableMode.Custom
                ? interactableMode
                : throw new ArgumentOutOfRangeException(nameof(mode), "InteractableMode can't be Custom. Use constructor by ICanExecuteView");
        }
        
        public void SetValue(IRelayCommand<int, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _intCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<long, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _longCommand, value, OnCanExecuteChanged);
        
        public void SetValue(IRelayCommand<float, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _floatCommand, value, OnCanExecuteChanged);

        public void SetValue(IRelayCommand<double, T1, T2, T3> value) =>
            CommandBinderExtensions.UpdateCommand(ref _doubleCommand, value, OnCanExecuteChanged);

        protected override void OnBound() =>
            Target.onValueChanged.AddListener(OnValueChanged);

        protected override void OnUnbound()
        {
            Target.onValueChanged.RemoveListener(OnValueChanged);
            
            SetValue((IRelayCommand<int, T1, T2, T3>)null);
            SetValue((IRelayCommand<long, T1, T2, T3>)null);
            SetValue((IRelayCommand<float, T1, T2, T3>)null);
            SetValue((IRelayCommand<double, T1, T2, T3>)null);
        }

        private void OnValueChanged(float value)
        {
            if (_floatCommand is not null) _floatCommand.Execute(Target.value, Param1, Param2, Param3);
            else if (_intCommand is not null) _intCommand.Execute((int)Target.value, Param1, Param2, Param3);
            else if (_doubleCommand is not null) _doubleCommand.Execute(Target.value, Param1, Param2, Param3);
            else if (_longCommand is not null) _longCommand.Execute((long)Target.value, Param1, Param2, Param3);
        }
        
        private void OnCanExecuteChanged<TValue>(IRelayCommand<TValue, T1, T2, T3> command)
        {
            if (_interactableMode is InteractableMode.None) return;

            var value = Target.value;
            
            // TODO Check As
            var castedValue = Unsafe.As<float, TValue>(ref value);
            
            SetInteractableMode(command.CanExecute(castedValue, Param1, Param2, Param3));
        }

        private void SetInteractableMode(bool isInteractable)
        {
            switch (_interactableMode)
            {
                case InteractableMode.Interactable: Target.interactable = isInteractable; break;
                case InteractableMode.Visible: Target.gameObject.SetActive(isInteractable); break;
                case InteractableMode.Custom: _customInteractable.SetCanExecute(isInteractable); break;
            }
        }
    }
}