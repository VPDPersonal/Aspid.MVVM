using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="InvalidCastException"/> for a binder that cannot receive the bound value type from the ViewModel.
    /// </summary>
    /// <remarks>
    /// The message names the binder type and, for a live component, its GameObject path.
    /// </remarks>
    public sealed class BinderInvalidCastException : InvalidCastException
    {
        private BinderInvalidCastException(string message)
            : base(message) { }

        /// <summary>
        /// Creates an exception for a binder that implements neither <see cref="IBinder{T}"/> nor <see cref="IAnyBinder"/>.
        /// </summary>
        /// <typeparam name="T">The value type of the bindable member.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>The exception to throw at the call site.</returns>
        public static BinderInvalidCastException Class<T>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var valueType = typeof(T).GetClassMessage();
            var binderType = binder.GetType().GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{valueType}>";

            var message = binder.AddExceptionMessage(
                $"{binderType} cannot receive {valueType}.\n" +
                $"Required: {specificBinderType} or {anyBinderType}.");

            return new BinderInvalidCastException(message);
        }

        /// <summary>
        /// Creates an exception describing the typed, boxed and untyped interfaces accepted for this value.
        /// </summary>
        /// <typeparam name="T">The struct type of the bindable member.</typeparam>
        /// <typeparam name="TBoxed">The boxed form of <typeparamref name="T"/>.</typeparam>
        /// <param name="binder">The binder that failed the check.</param>
        /// <returns>The exception to throw at the call site.</returns>
        public static BinderInvalidCastException Struct<T, TBoxed>(IBinder binder)
        {
            var anyBinderType = "IAnyBinder".GetInterfaceMessage();
            var valueType = typeof(T).GetClassMessage();
            var binderType = binder.GetType().GetClassMessage();
            var specificBinderType = $"{"IBinder".GetInterfaceMessage()}<{valueType}>";
            var specificBinderBoxedType = $"{"IBinder".GetInterfaceMessage()}<{typeof(TBoxed).GetClassMessage()}>";

            var message = binder.AddExceptionMessage(
                $"{binderType} cannot receive {valueType}.\n" +
                $"Required: {specificBinderType} or {specificBinderBoxedType} or {anyBinderType}.");

            return new BinderInvalidCastException(message);
        }
    }
}
