#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageFillBinder : Binder, INumberBinder
    {
        [Header("Component")]
        [SerializeField] private Image _image;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

        public ImageFillBinder(Image image, Func<float, float> converter) 
            : this(image, new GenericFuncConverter<float, float>(converter)) { }
        
        public ImageFillBinder(Image image, IConverter<float, float>? converter = null)
        {
            _converter = converter;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        public void SetValue(int value) => SetValue((float)value);

        public void SetValue(long value) => SetValue((float)value);
        
        public void SetValue(float value) =>
            _image.fillAmount = _converter?.Convert(value) ?? value;

        public void SetValue(double value) => SetValue((float)value);
    }
}