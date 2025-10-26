using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class TwoWayValue<T> : Binder, IBinder<T>, IReverseBinder<T>, IBindableValue<T>
    {
        public event Action<T?>? Changed;
        
        event Action<T?>? IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private T? _value;
        
#if UNITY_2023_1_OR_NEWER
        [UnityEngine.SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<T?, T?>? _converter;
        
        private Action<T?>? _valueChanged;

        public T? Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(value);
            }
        }

        public TwoWayValue(BindMode mode = BindMode.TwoWay)
            : this(default, mode) { }
        
        public TwoWayValue(T? value, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            _value = value;
        }
        
        public TwoWayValue(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            
            _value = value;
            _converter = converter;
        }

        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        protected override void OnBound()
        {
            if (Mode is not BindMode.OneWayToSource) return;
            _valueChanged?.Invoke(Value);
        }

        public static implicit operator T?(TwoWayValue<T?> binder) => binder.Value;
    }
}