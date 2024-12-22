#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherBinder<float>
    {
        [Header("Component")]
        [SerializeField] private CanvasGroup _canvasGroup;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public CanvasGroupAlphaSwitcherBinder(
            float trueValue, 
            float falseValue, 
            CanvasGroup canvasGroup, 
            Func<float, float> converter) 
            : this(trueValue, falseValue, canvasGroup, converter.ToConvert()) { }
        
        public CanvasGroupAlphaSwitcherBinder(
            float trueValue, 
            float falseValue, 
            CanvasGroup canvasGroup, 
            IConverter<float, float>? converter = null) 
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _canvasGroup = canvasGroup ?? throw new ArgumentNullException(nameof(canvasGroup));
        }

        protected override void SetValue(float value)      
        {
            value = _converter?.Convert(value) ?? value;
            _canvasGroup.alpha = Mathf.Clamp01(value);
        }
    }
}