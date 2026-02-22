using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Exception thrown when a binder is not of the expected reverse binder type during a one-way-to-source binding operation.
    /// Provides factory methods for generating descriptive error messages for class and struct reverse binders.
    /// </summary>
    /// <typeparam name="T">The expected reverse-bound value type.</typeparam>
    public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException
    {
        private ReverseBinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Throws a <see cref="ReverseBinderInvalidCastException{T}"/> when a class binder does not implement
        /// <see cref="IReverseBinder{T}"/>.
        /// </summary>
        /// <param name="binder">The binder that failed the type check.</param>
        /// <returns>Never returns; always throws.</returns>
        /// <exception cref="ReverseBinderInvalidCastException{T}">Always thrown.</exception>
        public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
        {
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";

            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType}.");
            throw new ReverseBinderInvalidCastException<T>(message);
        }

        /// <summary>
        /// Throws a <see cref="ReverseBinderInvalidCastException{T}"/> when a struct binder does not implement
        /// <see cref="IReverseBinder{T}"/> or <see cref="IReverseBinder{TBoxed}"/>.
        /// </summary>
        /// <typeparam name="TBoxed">The expected boxed type.</typeparam>
        /// <param name="binder">The binder that failed the type check.</param>
        /// <returns>Never returns; always throws.</returns>
        /// <exception cref="ReverseBinderInvalidCastException{T}">Always thrown.</exception>
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