using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class OneWayProperty<T> : Binder, IBinder<T>, IReadOnlyBindableProperty<T>
    {
        public event Action<T?>? Changed;
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Header("Parameter")]
        [UnityEngine.SerializeField] 
#endif
        private T? _value;
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.Header("Converter")]
        [UnityEngine.SerializeReference]
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