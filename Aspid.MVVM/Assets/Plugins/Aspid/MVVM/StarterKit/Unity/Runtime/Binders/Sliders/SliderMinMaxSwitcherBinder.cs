#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class SliderMinMaxSwitcherBinder : SwitcherBinder<Slider, Vector2, Converter>
    {
        [SerializeField] private SliderValueMode _valueMode;
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, SliderValueMode.Range, converter: null, mode) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SliderValueMode valueMode,
            BindMode mode) 
            : this(target, trueValue, falseValue, valueMode, converter: null, mode) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, SliderValueMode.Range, converter, mode) { }
        
        public SliderMinMaxSwitcherBinder(
            Slider target, 
            Vector2 trueValue, 
            Vector2 falseValue,
            SliderValueMode valueMode = SliderValueMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode)
        {
            _valueMode = valueMode;
        }

        protected override void SetValue(Vector2 value) =>
            Target.SetMinMax(value, _valueMode);
    }
}