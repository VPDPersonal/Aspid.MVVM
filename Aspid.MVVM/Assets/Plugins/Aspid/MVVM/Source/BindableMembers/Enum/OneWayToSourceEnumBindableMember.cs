using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents a bindable member event that supports one-way-to-source bindings.
    /// </summary>
    /// <typeparam name="T">The type of the value to be handled in the bindable member event.</typeparam>
    public sealed class OneWayToSourceEnumBindableMember<T> : OneWayToSourceStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceEnumBindableMember{T}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <c>null</c>.</exception>
        public OneWayToSourceEnumBindableMember(Action<T> setValue) 
            : base(setValue) { }
    }
}