using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a one-way bindable member event that supports event notification and handling for values of a specified type.
    /// </summary>
    /// <typeparam name="T">The type of the value being handled in the bindable member event.</typeparam>
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