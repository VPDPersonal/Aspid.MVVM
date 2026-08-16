using Aspid.FastTools.Types;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Negates a boolean.
    /// </summary>
    /// <remarks>
    /// Not redundant with the <c>_isInvert</c> checkbox thirteen binders carry: those derive from the
    /// two-parameter <see cref="TargetBinder{TTarget, TProperty}"/>, which has no converter field, so
    /// the checkbox is the only inversion they can offer. Reach for this converter when the binder does
    /// have a converter slot, or when inversion is one step of a longer chain.
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
