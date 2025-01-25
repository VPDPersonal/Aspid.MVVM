#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class SliderMinMaxBinder : TargetBinder<Slider>, IBinder<Vector2>
    {
        [Header("Parameter")]
        [SerializeField] private SliderValueMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public SliderMinMaxBinder(
            Slider target, 
            Func<Vector2, Vector2> converter) 
            : this(target, SliderValueMode.Range, converter) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            SliderValueMode mode, 
            Func<Vector2, Vector2> converter) 
            : this(target, mode, converter.ToConvert()) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            Converter? converter)
            : this(target, SliderValueMode.Range, converter) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            SliderValueMode mode = SliderValueMode.Range, 
            Converter? converter = null)
            : base(target)
        {
            _mode = mode;
            _converter = converter; 
        }
        
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetMinMax(value, _mode);
        }
    }
}