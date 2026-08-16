// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Marks a type as a converter, whatever it converts between.
    /// </summary>
    /// <remarks>
    /// Deliberately empty, and stays that way: a type may implement
    /// <see cref="IConverter{TFrom, TTo}"/> many times over, so a member naming the converted types
    /// here would have to answer for all of them at once.
    /// </remarks>
    public interface IConverter { }

    /// <summary>
    /// Converts a value of type <typeparamref name="TFrom"/> into a value of type <typeparamref name="TTo"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    /// <typeparam name="TTo">The type of the converted value.</typeparam>
    /// <remarks>
    /// Conversion is one-directional: a binder in <see cref="BindMode.TwoWay"/> or
    /// <see cref="BindMode.OneWayToSource"/> does not get the reverse for free. Implementations sit on
    /// the hot path of every value push, so <see cref="Convert"/> should be pure, allocation-free and
    /// cheap enough to run per frame. A type may implement this interface for several type pairs.
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
