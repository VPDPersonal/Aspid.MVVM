using System;

namespace Aspid.MVVM
{
    public sealed class OneTimeClassEvent<T> : IBindableMemberEventAdder
        where T : class
    {
        private readonly T? _value;

        public OneTimeClassEvent(T? value)
        {
            _value = value;
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            if (binder.Mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{binder.Mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder:
                    specificBinder.SetValue(_value);
                    break;
                
                case IAnyBinder anyBinder:
                    anyBinder.SetValue(_value);
                    break;
                
                default: throw BinderInvalidCastException<T>.Class();
            }
            
            return null;
        }
    }
}