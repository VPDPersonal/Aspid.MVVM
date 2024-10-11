#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Sliders
{
    public class SliderRangeBinder : Binder, IBinder<Vector2>
    {
        private readonly Slider _slider;
        private readonly IConverter<Vector2, Vector2>? _converter;
        
        public SliderRangeBinder(Slider slider, Func<Vector2, Vector2> converter) 
            : this(slider, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public SliderRangeBinder(Slider slider, IConverter<Vector2, Vector2>? converter = null)
        {
            _converter = converter;
            _slider = slider ?? throw new ArgumentNullException(nameof(slider));
        }
        
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            
            _slider.minValue = value.x;
            _slider.maxValue = value.y;
        }
    }
}