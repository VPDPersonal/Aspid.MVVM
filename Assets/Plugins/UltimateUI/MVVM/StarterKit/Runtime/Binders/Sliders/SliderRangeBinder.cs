using System;
using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public class SliderRangeBinder : Binder, IBinder<Vector2>
    {
        protected readonly Slider Slider;
        protected readonly IConverter<Vector2, Vector2> Converter;
        
        public SliderRangeBinder(Slider slider, Func<Vector2, Vector2> converter) 
            : this(slider, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public SliderRangeBinder(Slider slider, IConverter<Vector2, Vector2> converter = null)
        {
            Slider = slider;
            Converter = converter;
        }
        
        public void SetValue(Vector2 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            Slider.minValue = value.x;
            Slider.maxValue = value.y;
        }
    }
}