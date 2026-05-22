using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayStructBindableMember{T,TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>,
    /// allowing enum-typed binders (<see cref="IBinder{Enum}"/>) to receive the boxed enum value alongside
    /// the strongly-typed <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The enum type of the bound value.</typeparam>
    public sealed class OneWayEnumBindableMember<T> : OneWayStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayEnumBindableMember{T}"/> class with the specified initial value.
        /// </summary>
        /// <param name="value">The initial value of the bindable member event.</param>
        public OneWayEnumBindableMember(T value) 
            : base(value) { }
    }
}
