using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Concrete <see cref="OneWayToSourceStructBindableMember{T, TBoxed}"/> that fixes <c>TBoxed</c> to <see cref="Enum"/>.
    /// </summary>
    /// <typeparam name="T">The bound enum type.</typeparam>
    public sealed class OneWayToSourceEnumBindableMember<T> : OneWayToSourceStructBindableMember<T, Enum>
        where T : struct, Enum
    {
        /// <inheritdoc/>
        public OneWayToSourceEnumBindableMember(Action<T> setValue)
            : base(setValue) { }
    }
}
