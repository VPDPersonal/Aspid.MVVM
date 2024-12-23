#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupAlphaBinder : TargetBinder<CanvasGroup>, INumberBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public CanvasGroupAlphaBinder(CanvasGroup target, Func<float, float> converter)
            : this(target, converter.ToConvert()) { }
        
        public CanvasGroupAlphaBinder(CanvasGroup target, IConverter<float, float>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(int value) => 
            SetValue((float)value);

        public void SetValue(long value) => 
            SetValue((float)value);
        
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.alpha = Mathf.Clamp01(value);
        }

        public void SetValue(double value) => 
            SetValue((float)value);
    }
}