using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="InvalidCastException"/> thrown when a binder lacks the interface a bindable member receives values through.
    /// </summary>
    /// <remarks>
    /// The message names the binder type and, for a live component, its GameObject path.
    /// </remarks>
    /// <typeparam name="T">The value type of the bindable member.</typeparam>
    public sealed class ReverseBinderInvalidCastException<T> : InvalidCastException
    {
        private ReverseBinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Creates an exception for a binder that implements neither <see cref="IReverseBinder{T}"/>
        /// nor <see cref="IAnyReverseBinder"/>.
        /// </summary>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>The exception to throw at the call site.</returns>
        public static ReverseBinderInvalidCastException<T> Class(IBinder binder)
        {
            var anyBinderType = "IAnyReverseBinder".GetInterfaceMessage();
            var valueType = typeof(T).GetClassMessage();
            var binderType = binder.GetType().GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{valueType}>";

            var message = binder.AddExceptionMessage(
                $"{binderType} cannot report {valueType} to the ViewModel.\n" +
                $"Required: {specificBinderType} or {anyBinderType}.");
            
            return new ReverseBinderInvalidCastException<T>(message);
        }

        /// <summary>
        /// Creates an exception for a binder that implements none of <see cref="IReverseBinder{T}"/>, <see cref="IReverseBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyReverseBinder"/>.
        /// </summary>
        /// <typeparam name="TBoxed">The boxed form of <c>T</c>.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>The exception to throw at the call site.</returns>
        public static ReverseBinderInvalidCastException<T> Struct<TBoxed>(IBinder binder)
        {
            var anyBinderType = "IAnyReverseBinder".GetInterfaceMessage();
            var valueType = typeof(T).GetClassMessage();
            var binderType = binder.GetType().GetClassMessage();
            var specificBinderType = $"{"IReverseBinder".GetInterfaceMessage()}<{valueType}>";
            var specificBinderBoxedType = $"{"IReverseBinder".GetInterfaceMessage()}<{typeof(TBoxed).GetClassMessage()}>";

            var message = binder.AddExceptionMessage(
                $"{binderType} cannot report {valueType} to the ViewModel.\n" +
                $"Required: {specificBinderType} or {specificBinderBoxedType} or {anyBinderType}.");
            
            return new ReverseBinderInvalidCastException<T>(message);
        }
    }
}
