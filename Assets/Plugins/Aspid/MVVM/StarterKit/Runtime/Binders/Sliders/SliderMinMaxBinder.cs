#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    public class SliderMinMaxBinder : Binder, IBinder<Vector2>
    {
        private readonly Slider _slider;
        private readonly SliderValueMode _mode;
        private readonly IConverter<Vector2, Vector2>? _converter;
        
        public SliderMinMaxBinder(Slider slider, Func<Vector2, Vector2> converter) 
            : this(slider, SliderValueMode.Range, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public SliderMinMaxBinder(Slider slider, SliderValueMode mode, Func<Vector2, Vector2> converter) 
            : this(slider, mode, new GenericFuncConverter<Vector2, Vector2>(converter)) { }
        
        public SliderMinMaxBinder(Slider slider, SliderValueMode mode = SliderValueMode.Range, IConverter<Vector2, Vector2>? converter = null)
        {
            _converter = converter;
            _slider = slider ?? throw new ArgumentNullException(nameof(slider));
        }
        
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            _slider.SetMinMax(value, _mode);
        }
    }
}