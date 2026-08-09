// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A converter that can also undo itself, for values travelling back to the ViewModel.
    /// </summary>
    /// <typeparam name="TFrom">The type held by the ViewModel.</typeparam>
    /// <typeparam name="TTo">The type held by the View.</typeparam>
    /// <remarks>
    /// Most conversions are one-directional and cannot be undone — a number rendered as text, a
    /// vector reduced to one axis — so this is opt-in. A binder in
    /// <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.TwoWay"/> applies
    /// <see cref="ConvertBack"/> when the configured converter offers it, and sends the value
    /// unchanged when it does not.
    /// <para>
    /// <see cref="ConvertBack"/> is expected to satisfy
    /// <c>ConvertBack(Convert(x)) == x</c> for every value the binder can carry. A converter that
    /// cannot promise that should not implement this interface: the alternative is a value that
    /// silently drifts every time it makes the round trip.
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
