#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public sealed class TwoWayProperty<T> : Binder, IBinder<T>, IReverseBinder<T>, IBindableProperty<T>
    {
        public event Action<T?>? Changed;
        
        event Action<T?>? IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }
        
        [Header("Parameter")]
        [SerializeField] private T? _value;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
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

        public TwoWayProperty(BindMode mode = BindMode.TwoWay)
            : this(default, mode) { }
        
        public TwoWayProperty(T? value, BindMode mode = BindMode.TwoWay)
            : base(mode)
        {
            mode.ThrowExceptionIfNone();
            _value = value;
        }
        
        public TwoWayProperty(T? value, Func<T?, T?> converter, BindMode mode = BindMode.TwoWay) 
            : this(value, converter.ToConvert(), mode) { }
        
        public TwoWayProperty(T? value, IConverter<T?, T?>? converter = null, BindMode mode = BindMode.TwoWay)
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

        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
            if (Mode is not BindMode.OneWayToSource) return;
            
            _valueChanged?.Invoke(Value);
        }

        public static implicit operator T?(TwoWayProperty<T?> binder) => binder.Value;
    }
}