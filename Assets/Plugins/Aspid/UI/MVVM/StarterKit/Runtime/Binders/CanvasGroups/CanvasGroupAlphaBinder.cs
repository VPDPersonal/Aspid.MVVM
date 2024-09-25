using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.CanvasGroups
{
    public class CanvasGroupAlphaBinder : Binder, IBinder<bool>, IBinder<float>
    {
        protected readonly CanvasGroup CanvasGroup;
        protected readonly IConverter<float, float> Converter;
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, Func<float, float> converter)
            : this(canvasGroup, new GenericFuncConverter<float, float>(converter)) { }
        
        public CanvasGroupAlphaBinder(CanvasGroup canvasGroup, IConverter<float, float> converter = null)
        {
            Converter = converter;
            CanvasGroup = canvasGroup;
        }
        
        public void SetValue(bool value) =>
            SetValue(value ? 1 : 0);

        public void SetValue(float value)
        {
            value = Converter?.Convert(value) ?? value;
            CanvasGroup.alpha = Mathf.Clamp(value, 0, 1);
        }
    }
}