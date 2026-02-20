using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    // TODO Aspid.MVVM – Write summary
    public sealed class BinderInvalidCastException : InvalidCastException
    {
        private BinderInvalidCastException(string message) 
            : base(message) { }

        public static BinderInvalidCastException Class<T>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";
            
            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType} or {anyBinderType}.");
            throw new BinderInvalidCastException(message);
        }

        public static BinderInvalidCastException Struct<T, TBoxed>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";
            var specificBinderBoxedType = $"{"IBinder".GetInterfaceMessage()}<{typeof(TBoxed).GetClassMessage()}>";
            
            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType} or {specificBinderBoxedType} or {anyBinderType}.");
            throw new BinderInvalidCastException(message);
        }
    }
}