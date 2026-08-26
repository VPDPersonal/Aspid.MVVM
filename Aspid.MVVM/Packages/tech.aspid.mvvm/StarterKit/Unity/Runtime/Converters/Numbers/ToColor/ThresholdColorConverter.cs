#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks a color by which threshold a number has passed.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Color",
        Name = "Threshold",
        Tooltip = "Picks a color by which threshold a number has passed")]
    public sealed class ThresholdColorConverter : IConverter<float, Color>, IConverter<double, Color>
    {
        [Tooltip("Colors by threshold. The highest threshold at or below the value wins.")]
        [SerializeField] private ColorStop[] _stops = Array.Empty<ColorStop>();

        [Tooltip("Blend toward the next stop up instead of holding this stop's color until it is reached.")]
        [SerializeField] private bool _interpolate;

        [Tooltip("Used when the value is below every threshold.")]
        [SerializeField] private Color _fallback = Color.white;

        private ThresholdColorConverter() { }

        /// <param name="stops">
        /// Colors by threshold. With none the converter has nothing to pick from, which is reported
        /// as an error.
        /// </param>
        /// <param name="fallback">Used when the value is below every threshold.</param>
        /// <param name="interpolate">Whether to blend toward the next stop up.</param>
        public ThresholdColorConverter(ColorStop[]? stops, Color fallback, bool interpolate = false)
        {
            _stops = stops ?? Array.Empty<ColorStop>();
            _fallback = fallback;
            _interpolate = interpolate;
        }

        /// <summary>
        /// Picks the color for the specified value.
        /// </summary>
        /// <param name="value">The value to place.</param>
        /// <returns>
        /// The color of the highest qualifying stop, or the fallback. When blending, the color
        /// between that stop and the next one up, by how far the value has traveled between them.
        /// With no stops authored the fallback is returned and the failure is reported as an error.
        /// </returns>
        public Color Convert(float value)
        {
            if (_stops is not { Length: > 0 })
            {
                this.LogError("no stops are authored, so no threshold can ever be passed",
                    "Returning the fallback color.");

                return _fallback;
            }

            // The stops are authored in any order, so both neighbors are found in one pass, not sorted.
            var color = _fallback;
            var lower = 0f;
            var hasLower = false;

            var next = default(Color);
            var upper = 0f;
            var hasUpper = false;

            for (var i = 0; i < _stops.Length; i++)
            {
                var threshold = _stops[i].Threshold;

                if (threshold <= value)
                {
                    if (hasLower && threshold <= lower) continue;

                    lower = threshold;
                    color = _stops[i].Color;
                    hasLower = true;
                }
                else
                {
                    if (hasUpper && threshold >= upper) continue;

                    upper = threshold;
                    next = _stops[i].Color;
                    hasUpper = true;
                }
            }

            if (!_interpolate || !hasLower || !hasUpper) return color;

            var span = upper - lower;
            return span <= 0f ? color : Color.Lerp(color, next, (value - lower) / span);
        }

        // The stops are authored as floats, so the double width is compared against them as one.
        Color IConverter<double, Color>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
