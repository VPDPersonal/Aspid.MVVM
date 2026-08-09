// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Marks a type as a converter, whatever it converts between.
    /// </summary>
    /// <remarks>
    /// Deliberately empty. Tooling needs to answer "is this a converter?" for a field type or a
    /// picker candidate without walking every interface a type implements, and that is all this
    /// gives. The concrete types stay on <see cref="IConverter{TFrom, TTo}"/>, which is the only
    /// place a conversion is actually declared — a type may implement it many times over.
    /// <para>
    /// It carries no members so that adding it breaks nothing: default interface members are the
    /// only way an interface can gain members without every implementer changing, and Unity does
    /// not support them.
    /// </para>
    /// </remarks>
    public interface IConverter { }

    /// <summary>
    /// Converts a value of type <typeparamref name="TFrom"/> into a value of type <typeparamref name="TTo"/>.
    /// </summary>
    /// <typeparam name="TFrom">The type of the value to convert.</typeparam>
    /// <typeparam name="TTo">The type of the converted value.</typeparam>
    /// <remarks>
    /// Conversion is one-directional: there is no reverse operation, and a binder in
    /// <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/> does not get one for
    /// free. Implementations sit on the hot path of every value push, so
    /// <see cref="Convert"/> should be pure, allocation-free, and cheap enough to run per frame.
    /// A type may implement this interface several times to convert between several type pairs.
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
