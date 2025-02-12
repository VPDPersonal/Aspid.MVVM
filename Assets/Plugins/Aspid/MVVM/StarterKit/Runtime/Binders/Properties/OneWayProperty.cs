#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class OneWayProperty<T> : Binder, IBinder<T>, IReadOnlyBindableProperty<T>
    {
        public event Action<T?>? Changed;
        
        [Header("Parameter")]
        [SerializeField] private T? _value;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<T?, T?>? _converter;

        public T? Value
        {
            get => _value;
            private set => _value = value;
        }

        public OneWayProperty(BindMode mode = BindMode.OneWay)
            : this(default, mode) { }
        
        public OneWayProperty(T? value, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _value = value;
        }
        
        public OneWayProperty(T? value, Func<T?, T?> converter, BindMode mode = BindMode.OneWay) 
            : this(value, converter.ToConvert(), mode) { }
        
        public OneWayProperty(T? value, IConverter<T?, T?>? converter = null, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _value = value;
            _converter = converter;
        }

        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }

        public static implicit operator T?(OneWayProperty<T?> binder) => binder.Value;
    }
}