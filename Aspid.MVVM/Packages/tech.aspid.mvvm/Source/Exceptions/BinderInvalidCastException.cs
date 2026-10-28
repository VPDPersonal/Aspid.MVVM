using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="InvalidCastException"/> thrown when a binder lacks the interface a bindable member pushes values through.
    /// </summary>
    /// <remarks>
    /// The message names the binder type and, for a component, its GameObject path.
    /// </remarks>
    public sealed class BinderInvalidCastException : InvalidCastException
    {
        private BinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Throws for a binder that implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </summary>
        /// <typeparam name="T">The value type of the bindable member.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>Never returns; the return type allows a <see langword="throw"/> expression at the call site.</returns>
        /// <exception cref="BinderInvalidCastException">Always.</exception>
        public static BinderInvalidCastException Class<T>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var binderType = binder.GetType().Name.GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{typeof(T).GetClassMessage()}>";

            var message = binder.AddExceptionMessage($"Binder ({binderType}) must be type {specificBinderType} or {anyBinderType}.");
            throw new BinderInvalidCastException(message);
        }

        /// <summary>
        /// Throws for a binder that implements none of <see cref="IBinder{T}"/>, <see cref="IBinder{T}"/> of
        /// <typeparamref name="TBoxed"/> and <see cref="IAnyBinder"/>.
        /// </summary>
        /// <typeparam name="T">The struct type of the bindable member.</typeparam>
        /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>Never returns; the return type allows a <see langword="throw"/> expression at the call site.</returns>
        /// <exception cref="BinderInvalidCastException">Always.</exception>
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
