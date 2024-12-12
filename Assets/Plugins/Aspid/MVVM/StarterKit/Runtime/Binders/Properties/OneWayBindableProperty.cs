using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class OneWayBindableProperty<T> : Binder, IBinder<T>, IReadOnlyBindableProperty<T>
    {
        public event Action<T> Changed;
        
        [field: SerializeField]
        public T Value { get; private set; }
        
        public OneWayBindableProperty() { }
        
        public OneWayBindableProperty(T value)
        {
            Value = value;
        }
        
        void IBinder<T>.SetValue(T value) 
        {
            Value = value;
            Changed?.Invoke(value);
        }
        
        public static implicit operator T(OneWayBindableProperty<T> binder) => binder.Value;
    }
}