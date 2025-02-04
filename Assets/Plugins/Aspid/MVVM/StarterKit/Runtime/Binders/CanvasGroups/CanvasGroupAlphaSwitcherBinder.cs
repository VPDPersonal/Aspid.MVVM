#nullable enable
using System;
using UnityEngine;
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
            BindMode mode) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target, 
            float trueValue, 
            float falseValue, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
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