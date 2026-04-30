using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for setting min/max range values on Unity <see cref="Slider"/> components.
    /// </summary>
    public static class SliderSetters
    {
        /// <summary>
        /// Sets the minimum and/or maximum value of a <see cref="Slider"/> according to the specified <see cref="SliderValueMode"/>.
        /// </summary>
        /// <param name="slider">The slider whose range will be modified.</param>
        /// <param name="value">
        /// A <see cref="Vector2"/> where <c>x</c> represents the minimum and <c>y</c> represents the maximum.
        /// Depending on <paramref name="mode"/>, only the relevant component is used.
        /// </param>
        /// <param name="mode">
        /// Determines which endpoint of the slider range is updated:
        /// <see cref="SliderValueMode.Min"/> updates only the minimum,
        /// <see cref="SliderValueMode.Max"/> updates only the maximum,
        /// and <see cref="SliderValueMode.Range"/> updates both endpoints.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMinMax(this Slider slider, Vector2 value, SliderValueMode mode)
        {
            value = mode switch
            {
                SliderValueMode.Min => new Vector2(value.x, slider.maxValue),
                SliderValueMode.Max => new Vector2(slider.minValue, value.y),
                SliderValueMode.Range => new Vector2(value.x, value.y),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            slider.minValue = value.x;
            slider.maxValue = value.y;
        }
    }
}