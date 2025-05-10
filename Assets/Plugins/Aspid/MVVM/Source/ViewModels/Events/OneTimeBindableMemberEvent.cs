using System;

namespace Aspid.MVVM
{
    public readonly struct OneTimeBindableMemberEvent<T> : IBindableMemberEventAdder
    {
        private readonly T? _value; 
        
        public OneTimeBindableMemberEvent(T? value)
        {
            _value = value;
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            if (binder.Mode is BindMode.OneWayToSource or BindMode.None)
                throw new InvalidOperationException();
            
            binder.Cast<T>().SetValue(_value);
            return null;
        }
    }
}