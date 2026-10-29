using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneTimeStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="T">The bound enum type.</typeparam>
    public sealed class OneTimeEnumBindableMember<T> : OneTimeStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <summary>
        /// Creates a member holding <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The enum value to push to the binder.</param>
        public OneTimeEnumBindableMember(T value) : base(value) { }
    }
}
