using System;

namespace Aspid.MVVM
{
    internal class ReverseBinderInvalidCastException<T> : InvalidCastException
    {
        private ReverseBinderInvalidCastException(string message) 
            : base(message) { }

        public static BinderInvalidCastException<T> Class() =>
            throw new InvalidCastException($"Binder must be type {typeof(IReverseBinder<T>)}");
        
        public static BinderInvalidCastException<T> Struct<TBoxed>() =>
            throw new InvalidCastException($"Binder must be type {typeof(IReverseBinder<T>)} or {typeof(IReverseBinder<TBoxed>)}");
    }
}