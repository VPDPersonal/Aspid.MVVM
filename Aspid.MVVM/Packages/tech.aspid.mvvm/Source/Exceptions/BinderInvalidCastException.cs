using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a binder is not of the expected type during a binding operation.
    /// Provides factory methods for generating descriptive error messages for class and struct binders.
    /// </summary>
    public sealed class BinderInvalidCastException : InvalidCastException
    {
        private BinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Throws a <see cref="BinderInvalidCastException"/> when a class binder does not implement
        /// <see cref="IBinder{T}"/> or <see cref="IAnyBinder"/>.
        /// </summary>
        /// <typeparam name="T">The expected bound value type.</typeparam>
        /// <param name="binder">The binder that failed the type check.</param>
        /// <returns>This method never returns; the return type exists to support <see langword="throw"/> expressions.</returns>
        /// <exception cref="BinderInvalidCastException">Always thrown.</exception>
        public static BinderInvalidCastException Class<T>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";

            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType} or {anyBinderType}.");
            throw new BinderInvalidCastException(message);
        }

        /// <summary>
        /// Throws a <see cref="BinderInvalidCastException"/> when a struct binder does not implement
        /// <see cref="IBinder{T}"/>, <see cref="IBinder{TBoxed}"/>, or <see cref="IAnyBinder"/>.
        /// </summary>
        /// <typeparam name="T">The expected unboxed struct type.</typeparam>
        /// <typeparam name="TBoxed">The expected boxed type.</typeparam>
        /// <param name="binder">The binder that failed the type check.</param>
        /// <returns>This method never returns; the return type exists to support <see langword="throw"/> expressions.</returns>
        /// <exception cref="BinderInvalidCastException">Always thrown.</exception>
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
