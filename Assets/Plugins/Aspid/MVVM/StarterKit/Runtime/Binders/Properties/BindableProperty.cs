using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Properties
{
    [Serializable]
    public sealed class BindableProperty<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        public event Action<T> Changed;
        
        event Action<T> IReverseBinder<T>.ValueChanged
        {
            add => _valueChanged += value;
            remove => _valueChanged -= value;
        }
        
        [SerializeField] private T _value;
        
        private Action<T> _valueChanged;
        
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _valueChanged?.Invoke(value);
            }
        }

        public BindableProperty() { }
        
        public BindableProperty(T value)
        {
            _value = value;
        }

        void IBinder<T>.SetValue(T value) 
        {
            Value = value;
            Changed?.Invoke(value);
        }
        
        public static implicit operator T(BindableProperty<T> binder) => binder.Value;
    }
}