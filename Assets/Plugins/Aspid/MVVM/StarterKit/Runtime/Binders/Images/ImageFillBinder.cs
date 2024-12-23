#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ImageFillBinder : TargetBinder<Image>, INumberBinder
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public ImageFillBinder(Image target, Func<float, float> converter) 
            : this(target, converter.ToConvert()) { }
        
        public ImageFillBinder(Image target, IConverter<float, float>? converter = null)
            : base(target)
        {
            _converter = converter;
        }

        public void SetValue(int value) => SetValue((float)value);

        public void SetValue(long value) => SetValue((float)value);
        
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            Target.fillAmount = Mathf.Clamp01(value);
        }

        public void SetValue(double value) => SetValue((float)value);
    }
}