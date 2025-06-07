using System;

namespace Aspid.MVVM
{
    public sealed class OneWayStructEvent<T> : OneWayStructEvent<T, ValueType>
        where T : struct
    {
        public OneWayStructEvent(T value) 
            : base(value) { }
    }

    public abstract class OneWayStructEvent<T, TBoxed> : IBindableMemberEvent
        where T : struct, TBoxed
        where TBoxed : class
    {
        public event Action<T>? Changed;
        private event Action<TBoxed>? BoxedChanged;

        private T _value;

        protected OneWayStructEvent(T value)
        {
            _value = value;
        }

        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder:
                    specificBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        Changed += specificBinder.SetValue;
                    break;
                
                case IBinder<TBoxed> structBinder:
                    structBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        BoxedChanged += structBinder.SetValue;
                    break;
                
                case IAnyBinder anyBinder:
                    anyBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        Changed += anyBinder.SetValue;
                    break;
                
                default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
            }
            
            return this;
        }

        public void Remove(IBinder binder)
        {
            if (binder.Mode is BindMode.OneTime)
                return;

            switch (binder)
            {
                case IBinder<T> specificBinder: Changed -= specificBinder.SetValue; break;
                case IBinder<TBoxed> structBinder: BoxedChanged -= structBinder.SetValue; break;
                case IAnyBinder anyBinder: Changed -= anyBinder.SetValue; break;
                default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
            }
        }
        
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
            BoxedChanged?.Invoke(value);
        }
    }
}