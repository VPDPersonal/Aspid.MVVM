using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="CasterMonoBinder{TFrom, TTo}"/> from <see cref="string"/> to <typeparamref name="TEnum"/>.
    /// Defaults to <see cref="StringToEnumConverter{TEnum}"/>. Close over a concrete enum to make it addable as a component.
    /// </summary>
    /// <typeparam name="TEnum">The enum type the string is converted into.</typeparam>
    public abstract class StringToEnumCasterMonoBinder<TEnum> : CasterMonoBinder<string, TEnum>
        where TEnum : struct, Enum
    {
        /// <inheritdoc/>
        protected override IConverter<string, TEnum> CreateDefaultConverter() =>
            new StringToEnumConverter<TEnum>();
    }
}
