using System;

namespace Aspid.MVVM
{
    public sealed class OneWayClassEvent<T> : IBindableMemberEvent
        where T : class
    {
        public event Action<T?>? Changed;

        private T? _value;

        public OneWayClassEvent(T? value)
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
                
                case IAnyBinder anyBinder:
                    anyBinder.SetValue(_value);
                    
                    if (mode is BindMode.OneWay)
                        Changed += anyBinder.SetValue;
                    break;
                
                default: throw BinderInvalidCastException<T>.Class();
            }
            
            return this;
        }

        public void Remove(IBinder binder)
        {
            if (binder.Mode is BindMode.OneTime)
                return;

            Changed -= binder switch
            {
                IBinder<T> specificBinder => specificBinder.SetValue,
                IAnyBinder anyBinder => anyBinder.SetValue,
                _ => throw BinderInvalidCastException<T>.Class()
            };
        }
        
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}