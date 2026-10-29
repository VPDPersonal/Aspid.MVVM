using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="TwoWayStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="T">The bound enum type.</typeparam>
    public sealed class TwoWayEnumBindableMember<T> : TwoWayStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <inheritdoc/>
        public TwoWayEnumBindableMember(T value, Action<T> setValue)
            : base(value, setValue) { }
    }
}
