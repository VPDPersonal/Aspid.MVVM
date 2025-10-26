using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public static class SliderSetters
    {
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