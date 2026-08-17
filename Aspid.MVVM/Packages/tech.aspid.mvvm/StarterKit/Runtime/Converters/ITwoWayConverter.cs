// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter that can also undo itself, for values travelling back to the ViewModel.
    /// </summary>
    /// <typeparam name="TFrom">The type held by the ViewModel.</typeparam>
    /// <typeparam name="TTo">The type held by the View.</typeparam>
    /// <remarks>
    /// Most conversions cannot be undone — a number rendered as text, a vector reduced to one axis —
    /// so this is opt-in: a binder in <see cref="BindMode.OneWayToSource"/> or
    /// <see cref="BindMode.TwoWay"/> sends the value unchanged when the converter does not offer it.
    /// <para>
    /// <see cref="ConvertBack"/> must satisfy <c>ConvertBack(Convert(x)) == x</c> for every value the
    /// binder can carry; a converter that cannot promise that makes the value drift each round trip.
    /// </para>
    /// </remarks>
    public interface ITwoWayConverter<TFrom, TTo> : IConverter<TFrom, TTo>
    {
        /// <summary>
        /// Converts a value coming back from the View.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>The value as the ViewModel expects it.</returns>
        public TFrom ConvertBack(TTo value);
    }
}
