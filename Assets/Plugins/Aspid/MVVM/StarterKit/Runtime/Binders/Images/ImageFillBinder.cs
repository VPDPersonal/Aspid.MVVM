#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class ImageFillBinder : TargetBinder<Image>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public ImageFillBinder(Image target, Func<float, float> converter) 
            : this(target, converter.ToConvert()) { }
        
        public ImageFillBinder(Image target, Converter? converter = null)
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