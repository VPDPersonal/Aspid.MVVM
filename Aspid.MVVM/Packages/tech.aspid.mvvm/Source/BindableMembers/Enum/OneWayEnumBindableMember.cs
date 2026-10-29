using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="T">The bound enum type.</typeparam>
    public sealed class OneWayEnumBindableMember<T> : OneWayStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <inheritdoc/>
        public OneWayEnumBindableMember(T value)
            : base(value) { }
    }
}
