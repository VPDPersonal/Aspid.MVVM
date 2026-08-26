// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts values back as well, for the trip from the View to the ViewModel.
    /// </summary>
    /// <typeparam name="TFrom">The type held by the ViewModel.</typeparam>
    /// <typeparam name="TTo">The type held by the View.</typeparam>
    /// <remarks>
    /// A binder in <see cref="BindMode.OneWayToSource"/> or <see cref="BindMode.TwoWay"/> sends the
    /// value unchanged when the converter does not offer the reverse conversion.
    /// <para>
    /// <see cref="ConvertBack"/> must satisfy <c>ConvertBack(Convert(x)) == x</c>, otherwise the
    /// value drifts on every round trip.
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
