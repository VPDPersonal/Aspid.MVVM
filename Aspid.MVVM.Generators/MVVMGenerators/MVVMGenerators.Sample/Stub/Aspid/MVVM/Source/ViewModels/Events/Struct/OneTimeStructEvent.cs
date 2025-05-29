using System;

namespace Aspid.MVVM
{
    public sealed class OneTimeStructEvent<T> : OneTimeStructEvent<T, ValueType>
        where T : struct
    {
        public OneTimeStructEvent(T value) 
            : base(value) { }
    }

    public abstract class OneTimeStructEvent<T, TBoxed> : IBindableMemberEventAdder
        where T : struct, TBoxed
        where TBoxed : class
    {
        private readonly T _value;

        protected OneTimeStructEvent(T value)
        {
            _value = value;
        }

        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            if (binder.Mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{binder.Mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                case IBinder<TBoxed> structBinder: structBinder.SetValue(_value); break;
                case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
            }
            
            return null;
        }
    }
}