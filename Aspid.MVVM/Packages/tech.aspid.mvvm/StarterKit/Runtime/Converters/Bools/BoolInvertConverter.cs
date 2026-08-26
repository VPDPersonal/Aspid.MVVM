using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Negates a boolean.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Bool",
        Name = "Invert",
        Tooltip = "Negates a boolean")]
    public sealed class BoolInvertConverter : ITwoWayConverter<bool, bool>
    {
        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The negated value.</returns>
        public bool Convert(bool value) => !value;

        /// <summary>
        /// Negates the specified value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>The negated value.</returns>
        public bool ConvertBack(bool value) => !value;
    }
}
