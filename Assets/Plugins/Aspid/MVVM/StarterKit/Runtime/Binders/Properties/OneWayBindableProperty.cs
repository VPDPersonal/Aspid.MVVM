#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class OneWayBindableProperty<T> : Binder, IBinder<T>, IReadOnlyBindableProperty<T>
    {
        public event Action<T?>? Changed;
        
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
        
        public OneWayBindableProperty() { }

        public OneWayBindableProperty(T? value, Func<T?, T?> converter) 
            : this(value, converter.ToConvert()) { }
        
        public OneWayBindableProperty(T? value, IConverter<T?, T?>? converter = null)
        {
            _value = value;
            _converter = converter;
        }
        
        void IBinder<T>.SetValue(T? value) 
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }
        
        public static implicit operator T?(OneWayBindableProperty<T> binder) => binder.Value;
    }
}