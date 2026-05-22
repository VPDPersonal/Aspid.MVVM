using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="TwoWayStructBindableMember{T,TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>
    /// for two-way enum bindings, supporting both strongly-typed and boxed-enum binders.
    /// </summary>
    /// <typeparam name="T">The enum type of the bound value.</typeparam>
    public sealed class TwoWayEnumBindableMember<T> : TwoWayStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwoWayEnumBindableMember{T}"/> class with the specified value and a setter action.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public TwoWayEnumBindableMember(T value, Action<T> setValue) 
            : base(value, setValue) { }
    }
}
