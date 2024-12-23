#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class SliderMinMaxSwitcherBinder : SwitcherBinder<Slider, Vector2>
    {
        [SerializeField] private SliderValueMode _mode;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<Vector2, Vector2>? _converter;

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
            IConverter<Vector2, Vector2>? converter) 
            : this(target, trueValue, falseValue, SliderValueMode.Range, converter) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SliderValueMode mode = SliderValueMode.Range,
            IConverter<Vector2, Vector2>? converter = null) 
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