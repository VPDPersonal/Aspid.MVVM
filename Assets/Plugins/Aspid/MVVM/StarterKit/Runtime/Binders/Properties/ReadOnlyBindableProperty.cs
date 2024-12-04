using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Properties
{
    [Serializable]
    public sealed class ReadOnlyBindableProperty<T> : Binder, IBinder<T>
    {
        public event Action<T> Changed;
        
        [field: SerializeField]
        public T Value { get; private set; }
        
        public ReadOnlyBindableProperty() { }
        
        public ReadOnlyBindableProperty(T value)
        {
            Value = value;
        }
        
        void IBinder<T>.SetValue(T value) 
        {
            Value = value;
            Changed?.Invoke(value);
        }
        
        public static implicit operator T(ReadOnlyBindableProperty<T> binder) => binder.Value;
    }
}