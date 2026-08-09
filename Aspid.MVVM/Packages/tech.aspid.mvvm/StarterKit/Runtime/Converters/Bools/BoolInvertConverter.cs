using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Negates a boolean.
    /// </summary>
    /// <remarks>
    /// The View often wants the opposite of what the ViewModel exposes — a panel shown while
    /// <c>IsLoading</c> is false, a button enabled while <c>IsBusy</c> is false — and adding a second
    /// property for the negation puts View concerns in the ViewModel. Three binders carry their own
    /// <c>_isInvert</c> flag for the same reason; this is the same thing, available to all of them.
    /// </remarks>
    [Serializable]
    public sealed class BoolInvertConverter : ITwoWayConverter<bool, bool>
    {
        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The negated value.</returns>
        public bool Convert(bool value) => !value;

        /// <summary>
        /// Negates the specified value. Negation is its own inverse.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The negated value.</returns>
        public bool ConvertBack(bool value) => !value;
    }
}
