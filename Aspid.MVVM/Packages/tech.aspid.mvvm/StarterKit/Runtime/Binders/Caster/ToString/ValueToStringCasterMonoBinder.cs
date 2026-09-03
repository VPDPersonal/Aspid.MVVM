// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="CasterMonoBinder{TFrom, TTo}"/> from <typeparamref name="T"/> to <see cref="string"/>.
    /// Defaults to <see cref="ValueToStringConverter{T}"/>. Close over a concrete type to make it addable as a component.
    /// </summary>
    /// <typeparam name="T">The type of value received from the ViewModel.</typeparam>
    public abstract class ValueToStringCasterMonoBinder<T> : CasterMonoBinder<T, string>
    {
        /// <inheritdoc/>
        protected override IConverter<T, string> CreateDefaultConverter() =>
            new ValueToStringConverter<T>();
    }
}
