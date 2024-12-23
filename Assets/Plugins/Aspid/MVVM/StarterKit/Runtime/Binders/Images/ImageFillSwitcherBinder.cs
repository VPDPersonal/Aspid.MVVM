#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<Image, float>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue, 
            float falseValue, 
            Func<float, float> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue, 
            float falseValue,
            IConverter<float, float>? converter = null)
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.fillAmount = Mathf.Clamp01(value);
        }
    }
}