using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that supports one-way-to-source bindings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be handled in the bindable member event.</typeparam>
    public sealed class OneWayToSourceClassEvent<T> : IBindableMemberEvent
    {
        private readonly Action<T?> _setValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceClassEvent{T}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public OneWayToSourceClassEvent(Action<T?> setValue)
        {
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <inheritdoc />
        /// <summary>
        /// Adds a binder to the event if it supports reverse binding for one-way-to-source or two-way modes.
        /// </summary>
        /// <param name="binder">The binder to bind to the event.</param>
        /// <returns>Returns itself to allow unsubscription later.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the <paramref name="binder"/> does not have a valid binding mode or is not of type <see cref="IReverseBinder{T}"/>.
        /// </exception>
        public IBindableMemberEventRemover Add(IBinder binder)
        {
            var mode = binder.Mode;
            
            if (mode is not (BindMode.OneWayToSource or BindMode.TwoWay))
                throw new InvalidOperationException($"Mode must be OneWayToSource. Mode = {{{mode}}}");

            if (binder is not IReverseBinder<T> reverseBinder) 
                throw ReverseBinderInvalidCastException<T>.Class(binder);
            
            reverseBinder.ValueChanged += _setValue;
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Removes the binder's subscription from the event.
        /// </summary>
        /// <param name="binder">The binder to unsubscribe from the event.</param>
        public void Remove(IBinder binder)
        {
            if (binder is not IReverseBinder<T> reverseBinder) 
                throw ReverseBinderInvalidCastException<T>.Class(binder);
            
            reverseBinder.ValueChanged -= _setValue;
        }
    }
}