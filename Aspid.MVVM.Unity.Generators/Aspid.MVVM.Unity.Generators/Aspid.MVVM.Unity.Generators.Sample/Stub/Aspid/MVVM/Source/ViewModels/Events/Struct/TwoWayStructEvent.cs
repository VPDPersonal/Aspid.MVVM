using System;

namespace Aspid.MVVM
{
    public sealed class TwoWayStructEvent<T> : TwoWayStructEvent<T, ValueType>
        where T : struct
    {
        public TwoWayStructEvent(T value, Action<T> setValue) 
            : base(value, setValue) { }
    }

    public abstract class TwoWayStructEvent<T, TBoxed> : IBindableMemberEvent
        where T : struct, TBoxed
        where TBoxed : class
    {
        public event Action<T>? Changed;
        private event Action<TBoxed>? BoxedChanged;

        private T _value;
        private readonly Action<T> _setValue;

        protected TwoWayStructEvent(T value, Action<T> setValue)
        {
            _value = value;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            if (mode is BindMode.None)
                throw new InvalidOperationException("Mode can't be None.");

            switch (mode)
            {
                case BindMode.OneWay: OneWay(); break;
                
                case BindMode.TwoWay:
                    OneWay();
                    OneWayToSource();
                    break;
                
                case BindMode.OneTime: switch (binder)
                    {
                        case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                        case IBinder<TBoxed> structBinder: structBinder.SetValue(_value); break;
                        case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                        default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
                    }
                    break;
                
                case BindMode.OneWayToSource: OneWayToSource(); break;
            }

            return this;

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: 
                        specificBinder.SetValue(_value); 
                        Changed += specificBinder.SetValue;
                        break;
                    
                    case IBinder<TBoxed> specificBinder: 
                        specificBinder.SetValue(_value); 
                        BoxedChanged += specificBinder.SetValue;
                        break;
                        
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value); 
                        Changed += anyBinder.SetValue;
                        break;
                        
                    default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
                }
            }

            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged += _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged += SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>();
                }
            }
        }

        public void Remove(IBinder binder)
        {
            switch (binder.Mode)
            {
                case BindMode.OneTime: return;
                case BindMode.OneWay: OneWay(); return;
                
                case BindMode.TwoWay:
                    OneWay();
                    OneWayToSource();
                    return;
                
                case BindMode.OneWayToSource: OneWayToSource(); return;
            }
            
            return;

            void OneWay()
            {
                switch (binder)
                {
                    case IBinder<T> specificBinder: Changed -= specificBinder.SetValue; break;
                    case IBinder<TBoxed> structBinder: BoxedChanged -= structBinder.SetValue; break;
                    case IAnyBinder anyBinder: Changed -= anyBinder.SetValue; break;
                    default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
                }
            }
            
            void OneWayToSource()
            {
                switch (binder)
                {
                    case IReverseBinder<T> reverseBinder: reverseBinder.ValueChanged -= _setValue; break;
                    case IReverseBinder<TBoxed> structReverseBinder: structReverseBinder.ValueChanged -= SetBoxedValue; break;
                    default: throw ReverseBinderInvalidCastException<T>.Struct<TBoxed>();
                }
            }
        }

        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
            BoxedChanged?.Invoke(value);
        }
        
        private void SetBoxedValue(TBoxed? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            
            _setValue.Invoke((T)value);
        }
    }
}