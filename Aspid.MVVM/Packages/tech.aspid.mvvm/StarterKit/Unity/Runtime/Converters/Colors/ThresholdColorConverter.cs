#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks a colour by which threshold a number has passed.
    /// </summary>
    /// <remarks>
    /// Stepped states — green, amber, red — rather than a continuous ramp. Blending turns the same
    /// authored stops into that ramp; it is off by default because the step is what makes a threshold
    /// readable as a state.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Threshold Color", Tooltip = "Picks a colour by which threshold a number has passed")]
    public sealed class ThresholdColorConverter : IConverter<float, Color>
    {
        [Tooltip("Colours by threshold. The highest threshold at or below the value wins.")]
        [SerializeField] private ColorStop[] _stops = Array.Empty<ColorStop>();

        [Tooltip("Used when the value is below every threshold.")]
        [SerializeField] private Color _fallback = Color.white;

        [Tooltip("Blend towards the next stop up instead of holding this stop's colour until it is reached.")]
        [SerializeField] private bool _interpolate;

        public ThresholdColorConverter() { }

        /// <param name="stops">Colours by threshold.</param>
        /// <param name="fallback">Used when the value is below every threshold.</param>
        /// <param name="interpolate">Whether to blend towards the next stop up.</param>
        public ThresholdColorConverter(ColorStop[]? stops, Color fallback, bool interpolate = false)
        {
            _stops = stops ?? Array.Empty<ColorStop>();
            _fallback = fallback;
            _interpolate = interpolate;
        }

        /// <summary>
        /// Picks the colour for the specified value.
        /// </summary>
        /// <param name="value">The value to place.</param>
        /// <returns>
        /// The colour of the highest qualifying stop, or the fallback. When blending, the colour
        /// between that stop and the next one up, by how far the value has travelled between them.
        /// </returns>
        public Color Convert(float value)
        {
            if (_stops is not { Length: > 0 }) return _fallback;

            // The stops are authored in whatever order suits the Inspector, so both neighbours are
            // found in one pass rather than by sorting — which would allocate on every push.
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
    }
}
