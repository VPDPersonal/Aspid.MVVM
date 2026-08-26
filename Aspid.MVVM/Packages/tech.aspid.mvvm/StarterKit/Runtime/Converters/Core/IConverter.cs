// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Marks a type as a converter.
    /// </summary>
    /// <remarks>
    /// Empty by design: a type may implement <see cref="IConverter{TFrom, TTo}"/> for several type pairs,
    /// so no member here could name the converted types.
    /// </remarks>
    public interface IConverter { }

    /// <summary>
    /// Converts a value of type <typeparamref name="TFrom"/> into a value of type <typeparamref name="TTo"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    /// <typeparam name="TTo">The type of the converted value.</typeparam>
    /// <remarks>
    /// One-directional; the reverse trip needs <see cref="ITwoWayConverter{TFrom, TTo}"/>.
    /// <see cref="Convert"/> runs on every value push, so keep it pure and allocation-free.
    /// </remarks>
    public interface IConverter<in TFrom, out TTo> : IConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public TTo Convert(TFrom value);
    }
}
