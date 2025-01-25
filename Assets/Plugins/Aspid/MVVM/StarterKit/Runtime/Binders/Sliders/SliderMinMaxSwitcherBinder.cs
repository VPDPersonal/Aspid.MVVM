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
    public sealed class SliderMinMaxSwitcherBinder : SwitcherBinder<Slider, Vector2>
    {
        [SerializeField] private SliderValueMode _mode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Func<Vector2, Vector2> converter) 
            : this(target, trueValue, falseValue, SliderValueMode.Range, converter) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SliderValueMode mode,
            Func<Vector2, Vector2> converter) 
            : this(target, trueValue, falseValue, mode, converter.ToConvert()) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter) 
            : this(target, trueValue, falseValue, SliderValueMode.Range, converter) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SliderValueMode mode = SliderValueMode.Range,
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _mode = mode;
            _converter = converter;
        }

        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetMinMax(value, _mode);
        }
    }
}