using System;

namespace Aspid.MVVM
{
    internal class BinderInvalidCastException<T> : InvalidCastException
    {
        private BinderInvalidCastException(string message) 
            : base(message) { }

        public static BinderInvalidCastException<T> Class() =>
            throw new InvalidCastException($"Binder must be type {typeof(IBinder<T>)} or {typeof(IAnyBinder)}");
        
        public static BinderInvalidCastException<T> Struct<TBoxed>() =>
            throw new InvalidCastException($"Binder must be type {typeof(IBinder<T>)} or {typeof(IBinder<TBoxed>)} {typeof(IAnyBinder)}");
    }
}