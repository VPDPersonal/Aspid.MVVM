#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a percentage to a 0..1 fraction.
    /// </summary>
    /// <remarks>
    /// The other direction of <see cref="NormalizedToPercentConverter"/>, whose <c>ConvertBack</c> a
    /// binder only calls in <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Percent To Normalized", Tooltip = "Converts a percentage to a 0..1 fraction")]
    public sealed class PercentToNormalizedConverter : ITwoWayConverter<float, float>
    {
        /// <summary>
        /// Converts the specified percentage to a fraction.
        /// </summary>
        /// <param name="value">The percentage.</param>
        /// <returns>The 0..1 fraction. A percentage outside 0..100 is not clamped.</returns>
        public float Convert(float value) => value / 100f;

        /// <summary>
        /// Converts a fraction back to a percentage.
        /// </summary>
        /// <param name="value">The 0..1 fraction.</param>
        /// <returns>The percentage.</returns>
        public float ConvertBack(float value) => value * 100f;
    }
}
