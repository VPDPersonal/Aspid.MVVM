#nullable enable
using System;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class BindableProperty<T> : Binder, IBinder<T>, IReverseBinder<T>, IBindableProperty<T>
    {
        public event Action<T?>? Changed;
        
        event Action<T?>? IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }
        
        [SerializeField] private T? _value;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
#endif
        private IConverter<T?, T?>? _converter;
        
        private Action<T>? _valueChanged;
        
        public T? Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(value);
            }
        }

        public BindableProperty() { }

        public BindableProperty(T value, Func<T?, T?> converter) 
            : this(value, new GenericFuncConverter<T?, T?>(converter)) { }
        
        public BindableProperty(T value, IConverter<T?, T?>? converter = null)
        {
            _value = value;
            _converter = converter;
        }

        void IBinder<T>.SetValue(T? value)
        {
            Value = _converter is not null ? _converter.Convert(value) : value;
            Changed?.Invoke(value);
        }
        
        public static implicit operator T?(BindableProperty<T> binder) => binder.Value;
    }
}