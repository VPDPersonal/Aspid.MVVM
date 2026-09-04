using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="Slider"/> used by the slider binders.
    /// </summary>
    public static class SliderExtensions
    {
        /// <summary>
        /// Writes <see cref="Slider.minValue"/>, <see cref="Slider.maxValue"/> or both from <paramref name="value"/>.
        /// </summary>
        /// <remarks>
        /// Unity does not keep <c>minValue &lt;= maxValue</c>: an inverted pair is reported and swapped, a non-finite
        /// pair is reported and not applied.
        /// </remarks>
        /// <param name="slider">The slider whose range is set.</param>
        /// <param name="value">
        /// The range; <see cref="Vector2.x"/> is the minimum, <see cref="Vector2.y"/> the maximum.
        /// </param>
        /// <param name="mode">Which endpoints <paramref name="value"/> writes.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMinMax(this Slider slider, Vector2 value, SliderRangeMode mode)
        {
            value = mode switch
            {
                SliderRangeMode.Min => new Vector2(value.x, slider.maxValue),
                SliderRangeMode.Max => new Vector2(slider.minValue, value.y),
                SliderRangeMode.Range => value,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            if (!BinderMath.RequireFinite(typeof(SliderExtensions), value, slider)) return;

            if (value.x > value.y)
            {
                BinderLogger.LogError(
                    typeof(SliderExtensions),
                    problem: $"the range ({value.x}, {value.y}) is inverted",
                    consequence: "The endpoints are swapped.",
                    context: slider);

                value = new Vector2(value.y, value.x);
            }

            slider.minValue = value.x;
            slider.maxValue = value.y;
        }
    }
}
