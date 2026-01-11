#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<Image, float>
    {
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue, 
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue, 
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
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