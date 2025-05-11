using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that provides a single value in a one-time binding operation.
    /// </summary>
    /// <typeparam name="T">The type of the value to be bound.</typeparam>
    public class OneTimeBindableMemberEvent<T> : IBindableMemberEventAdder
    {
        private readonly T? _value; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OneTimeBindableMemberEvent{T}"/> class with the specified value.
        /// </summary>
        /// <param name="value">The value to be bound in the event.</param>
        public OneTimeBindableMemberEvent(T? value)
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
        /// Thrown if the binding mode is either <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.None"/>.
        /// </exception>
        public IBindableMemberEventRemover? Add(IBinder binder)
        {
            if (binder.Mode is BindMode.OneWayToSource or BindMode.None)
                throw new InvalidOperationException();
            
            binder.Cast<T>().SetValue(_value);
            return null;
        }
    }
}