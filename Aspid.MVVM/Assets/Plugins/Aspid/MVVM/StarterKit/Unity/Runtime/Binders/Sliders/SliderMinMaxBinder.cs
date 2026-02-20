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
    public class SliderMinMaxBinder : TargetBinder<Slider, Vector2, Converter>, INumberBinder
    {
        [SerializeField] private SliderValueMode _valueMode;
        
        protected sealed override Vector2 Property
        {
            get => new(Target.minValue, Target.maxValue);
            set => Target.SetMinMax(value, _valueMode);
        }
        
        public SliderMinMaxBinder(
            Slider target, 
            BindMode mode)
            : this(target, SliderValueMode.Range, converter: null, mode) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            SliderValueMode valueMode, 
            BindMode mode)
            : this(target, valueMode, converter: null, mode) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, SliderValueMode.Range, converter, mode) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            SliderValueMode valueMode = SliderValueMode.Range, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _valueMode = valueMode;
        }
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}