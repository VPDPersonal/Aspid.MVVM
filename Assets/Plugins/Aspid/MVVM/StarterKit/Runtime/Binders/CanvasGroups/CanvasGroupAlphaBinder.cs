#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class CanvasGroupAlphaBinder : Binder, INumberBinder
    {
        private CanvasGroup _canvasGroup;
        private IConverter<float, float>? _converter;
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, Func<float, float> converter)
            : this(canvasGroup, new GenericFuncConverter<float, float>(converter)) { }
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, IConverter<float, float>? converter = null)
        {
            _converter = converter;
            _canvasGroup = canvasGroup ?? throw new ArgumentNullException(nameof(canvasGroup));
        }

        public void SetValue(int value) => 
            SetValue((float)value);

        public void SetValue(long value) => 
            SetValue((float)value);

        public void SetValue(double value) => 
            SetValue((float)value);
        
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            _canvasGroup.alpha = Mathf.Clamp(value, 0, 1);
        }
    }
}