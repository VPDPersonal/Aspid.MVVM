using System;

namespace Aspid.MVVM
{
    public sealed class TwoWayClassEvent<T> : IBindableMemberEvent
    {
        public event Action<T?>? Changed;

        private T? _value;
        private readonly Action<T?> _setValue;

        public TwoWayClassEvent(T? value, Action<T?> setValue)
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
                        case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                        default: throw BinderInvalidCastException<T>.Class();
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
                        
                    case IAnyBinder anyBinder:
                        anyBinder.SetValue(_value); 
                        Changed += anyBinder.SetValue;
                        break;
                        
                    default: throw BinderInvalidCastException<T>.Class();
                }
            }

            void OneWayToSource()
            {
                if (binder is not IReverseBinder<T> reverseBinder)
                    throw ReverseBinderInvalidCastException<T>.Class();

                reverseBinder.ValueChanged += _setValue;
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

            void OneWay() => Changed -= binder switch
            {
                IBinder<T> specificBinder => specificBinder.SetValue,
                IAnyBinder anyBinder => anyBinder.SetValue,
                _ => throw BinderInvalidCastException<T>.Class()
            };
            
            void OneWayToSource()
            {
                if (binder is not IReverseBinder<T> reverseBinder)
                    throw ReverseBinderInvalidCastException<T>.Class();

                reverseBinder.ValueChanged -= _setValue;
            }
        }

        public void Invoke(T? value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}