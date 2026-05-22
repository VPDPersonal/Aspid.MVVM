using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayToSourceStructBindableMember{T,TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>
    /// for one-way-to-source enum bindings, forwarding View-side enum changes back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The enum type of the bound value.</typeparam>
    public sealed class OneWayToSourceEnumBindableMember<T> : OneWayToSourceStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OneWayToSourceEnumBindableMember{T}"/> class with the specified value setter action.
        /// </summary>
        /// <param name="setValue">
        /// The action used to set the value when the event is triggered.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public OneWayToSourceEnumBindableMember(Action<T> setValue) 
            : base(setValue) { }
    }
}
