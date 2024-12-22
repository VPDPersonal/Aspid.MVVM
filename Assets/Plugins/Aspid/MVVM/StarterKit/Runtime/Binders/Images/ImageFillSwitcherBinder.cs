#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<float>
    {
        [Header("Component")]
        [SerializeField] private Image _image;

#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;
        
        public ImageFillSwitcherBinder(float trueValue, float falseValue, Image image, Func<float, float> converter) 
            : this(trueValue, falseValue, image, converter.ToConvert()) { }
        
        public ImageFillSwitcherBinder(
            float trueValue, 
            float falseValue,
            Image image,
            IConverter<float, float>? converter = null)
            : base(trueValue, falseValue)
        {
            _converter = converter;
            _image = image ?? throw new ArgumentNullException(nameof(image));
        }

        protected override void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            _image.fillAmount = Mathf.Clamp01(value);
        }
    }
}