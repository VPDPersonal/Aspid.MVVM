using Aspid.FastTools.Types;
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
    /// property for the negation puts View concerns in the ViewModel.
    /// <para>
    /// Thirteen binders carry an <c>_isInvert</c> checkbox for the same reason, and it is not
    /// redundant with this: those binders derive from the two-parameter
    /// <see cref="TargetBinder{TTarget, TProperty}"/>, which has no converter field, so the checkbox
    /// is the only inversion they can offer. Reach for this converter when the binder does have a
    /// converter slot, or when inversion is one step of a longer chain.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Bool", Name = "Bool Invert", Tooltip = "Negates a boolean")]
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
