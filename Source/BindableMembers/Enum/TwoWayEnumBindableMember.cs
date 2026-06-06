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
        /// <inheritdoc/>
        public TwoWayEnumBindableMember(T value, Action<T> setValue)
            : base(value, setValue) { }
    }
}
