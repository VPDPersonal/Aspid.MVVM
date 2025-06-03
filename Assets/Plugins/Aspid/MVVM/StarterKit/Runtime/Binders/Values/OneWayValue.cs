using System;

namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class OneWayValue<T> : Binder, IBinder<T>, IReadOnlyBindableValue<T>
    {
        public event Action<T?>? Changed;
        
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private T? _value;
        
#if UNITY_2023_1_OR_NEWER
        [UnityEngine.SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<T?, T?>? _converter;

        public T? Value
        {
            get => _value;
            private set => _value = value;
        }

        public OneWayValue(BindMode mode = BindMode.OneWay)
            : this(default, mode) { }
        
        public OneWayValue(T? value, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _value = value;
        }
        
        public OneWayValue(T? value, IConverter<T?, T?>? converter, BindMode mode = BindMode.OneWay)
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

        public static implicit operator T?(OneWayValue<T?> binder) => binder.Value;
    }
}