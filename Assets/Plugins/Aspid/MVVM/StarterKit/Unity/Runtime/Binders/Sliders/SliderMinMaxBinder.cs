#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class SliderMinMaxBinder : TargetBinder<Slider>, IBinder<Vector2>
    {
        [Header("Parameter")]
        [SerializeField] private SliderValueMode _valueMode;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public SliderMinMaxBinder(
            Slider target, 
            BindMode mode)
            : this(target, SliderValueMode.Range, null, mode) { }
        
        public SliderMinMaxBinder(
            Slider target, 
            SliderValueMode valueMode, 
            BindMode mode)
            : this(target, valueMode, null, mode) { }
        
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
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
            _valueMode = valueMode;
            _converter = converter; 
        }
        
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.SetMinMax(value, _valueMode);
        }
    }
}