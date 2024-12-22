#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupAlphaBinder : Binder, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private CanvasGroup _canvasGroup;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, Func<float, float> converter)
            : this(canvasGroup, converter.ToConvert()) { }
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, IConverter<float, float>? converter = null)
        {
            _converter = converter;
            _canvasGroup = canvasGroup ?? throw new ArgumentNullException(nameof(canvasGroup));
        }

        public void SetValue(int value) => 
            SetValue((float)value);

        public void SetValue(long value) => 
            SetValue((float)value);
        
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            _canvasGroup.alpha = Mathf.Clamp01(value);
        }

        public void SetValue(double value) => 
            SetValue((float)value);
    }
}