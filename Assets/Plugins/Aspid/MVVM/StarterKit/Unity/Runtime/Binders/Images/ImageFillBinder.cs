#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class ImageFillBinder : TargetBinder<Image>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public ImageFillBinder(Image target, BindMode mode)
            : this(target, null, mode) { }
        
        public ImageFillBinder(Image target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
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