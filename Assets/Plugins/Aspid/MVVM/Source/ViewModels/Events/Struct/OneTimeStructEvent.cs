using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    public sealed class OneTimeStructEvent<T> : OneTimeStructEvent<T, ValueType>
        where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeStructEvent{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The value to be bound in the event.</param>
        public OneTimeStructEvent(T value) 
            : base(value) { }
    }

    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    /// <typeparam name="TBoxed">Boxed type</typeparam>
    public abstract class OneTimeStructEvent<T, TBoxed> : IBindableMemberEventAdder
        where T : struct, TBoxed
        where TBoxed : class
    {
        private readonly T _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeStructEvent{T, TBoxed}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The value to be bound in the event.</param>
        protected OneTimeStructEvent(T value)
        {
            _value = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a one-time binding to the specified binder with the associated value.
        /// </summary>
        /// <param name="binder">The binder to be used for the binding.</param>
        /// <returns>
        /// Always returns <c>null</c> because removal of this binding is not supported.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the binding mode is either <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.OneTime"/> <see cref="BindMode.None"/>.
        /// </exception>
        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            if (binder.Mode is not (BindMode.OneWay or BindMode.OneTime))
                throw new InvalidOperationException($"Mode must be OneWay or OneTime. Mode = {{{binder.Mode}}}");

            switch (binder)
            {
                case IBinder<T> specificBinder: specificBinder.SetValue(_value); break;
                case IBinder<TBoxed> structBinder: structBinder.SetValue(_value); break;
                case IAnyBinder anyBinder: anyBinder.SetValue(_value); break;
                default: throw BinderInvalidCastException<T>.Struct<TBoxed>();
            }
            
            return null;
        }
    }
}