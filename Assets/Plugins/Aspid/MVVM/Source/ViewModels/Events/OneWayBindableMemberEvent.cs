using System;

namespace Aspid.MVVM
{
    public sealed class OneWayBindableMemberEvent<T> : IBindableMemberEvent
    {
        public event Action<T?>? Changed;
        
        private T? _value;

        public OneWayBindableMemberEvent(T? value)
        {
            _value = value;
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWay or BindMode.OneTime)) 
                throw new Exception("Only OneWay and PneTime binding modes are supported in OneWayViewModelEvent.");

            var specificBinder = binder.Cast<T>();
            
            specificBinder.SetValue(_value);
            Changed += specificBinder.SetValue;
            
            return this;
        }

        public void Remove(IBinder binder) =>
            Changed -= binder.Cast<T>().SetValue;
        
        public void Invoke(T value)
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }
}