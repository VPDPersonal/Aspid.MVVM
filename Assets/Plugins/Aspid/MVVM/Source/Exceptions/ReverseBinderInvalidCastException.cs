using System;

namespace Aspid.MVVM
{
    public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException
    {
        private ReverseBinderInvalidCastException(string message) 
            : base(message) { }

        public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
        {
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";
            
            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType}.");
            throw new ReverseBinderInvalidCastException<T>(message);
        }

        public static ReverseBinderInvalidCastException<T> Struct<TBoxed>(IBinder binder)
        {
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";
            var specificBinderBoxedType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(TBoxed).GetClassMessage()}>";
            
            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType} or {specificBinderBoxedType}.");
            throw new ReverseBinderInvalidCastException<T>(message);
        }
    }
}