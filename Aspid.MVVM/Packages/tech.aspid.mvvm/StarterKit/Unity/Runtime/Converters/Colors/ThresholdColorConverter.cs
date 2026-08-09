#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks a colour by which threshold a number has passed.
    /// </summary>
    /// <remarks>Stepped states — green, amber, red — rather than a continuous ramp.</remarks>
    [Serializable]
    public sealed class ThresholdColorConverter : IConverter<float, Color>
    {
        [Tooltip("Colours by threshold. The highest threshold at or below the value wins.")]
        [SerializeField] private ColorStop[] _stops = Array.Empty<ColorStop>();

        [Tooltip("Used when the value is below every threshold.")]
        [SerializeField] private Color _fallback = Color.white;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdColorConverter"/> class with no stops.
        /// </summary>
        public ThresholdColorConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdColorConverter"/> class.
        /// </summary>
        /// <param name="stops">Colours by threshold.</param>
        /// <param name="fallback">Used when the value is below every threshold.</param>
        public ThresholdColorConverter(ColorStop[]? stops, Color fallback)
        {
            _stops = stops ?? Array.Empty<ColorStop>();
            _fallback = fallback;
        }

        /// <summary>
        /// Picks the colour for the specified value.
        /// </summary>
        /// <param name="value">The value to place.</param>
        /// <returns>The colour of the highest qualifying stop, or the fallback.</returns>
        public Color Convert(float value)
        {
            if (_stops is not { Length: > 0 }) return _fallback;

            var color = _fallback;
            var best = float.NegativeInfinity;

            for (var i = 0; i < _stops.Length; i++)
                if (value >= _stops[i].Threshold && _stops[i].Threshold > best)
                {
                    best = _stops[i].Threshold;
                    color = _stops[i].Color;
                }

            return color;
        }
    }
}
