using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="InvalidCastException"/> thrown when a binder lacks the interface a bindable member receives values through.
    /// </summary>
    /// <remarks>
    /// The message names the binder type and, for a component, its GameObject path.
    /// </remarks>
    /// <typeparam name="T">The value type of the bindable member.</typeparam>
    public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException
    {
        private ReverseBinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Throws for a binder that implements neither <see cref="IReverseBinder{T}"/> nor <see cref="IAnyReverseBinder"/>.
        /// </summary>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>Never returns; the return type allows a <see langword="throw"/> expression at the call site.</returns>
        /// <exception cref="ReverseBinderInvalidCastException{T}">Always.</exception>
        public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
        {
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";

            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType}.");
            throw new ReverseBinderInvalidCastException<T>(message);
        }

        /// <summary>
        /// Throws for a binder that implements none of <see cref="IReverseBinder{T}"/>, <see cref="IReverseBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </summary>
        /// <typeparam name="TBoxed">The boxed form of <c>T</c>.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>Never returns; the return type allows a <see langword="throw"/> expression at the call site.</returns>
        /// <exception cref="ReverseBinderInvalidCastException{T}">Always.</exception>
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
