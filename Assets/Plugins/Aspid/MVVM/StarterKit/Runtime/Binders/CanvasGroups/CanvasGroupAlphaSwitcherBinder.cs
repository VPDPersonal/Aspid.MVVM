#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherBinder<CanvasGroup, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target, 
            float trueValue, 
            float falseValue, 
            Func<float, float> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target, 
            float trueValue, 
            float falseValue, 
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(float value)      
        {
            value = _converter?.Convert(value) ?? value;
            Target.alpha = Mathf.Clamp01(value);
        }
    }
}