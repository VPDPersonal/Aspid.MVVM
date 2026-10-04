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
        /// <inheritdoc/>
        public OneWayToSourceEnumBindableMember(Action<T> setValue)
            : base(setValue) { }
    }
}
