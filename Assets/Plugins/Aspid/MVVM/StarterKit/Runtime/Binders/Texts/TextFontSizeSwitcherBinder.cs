#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextFontSizeSwitcherBinder : SwitcherBinder<TMP_Text, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public TextFontSizeSwitcherBinder(
            TMP_Text target, 
            float trueValue, 
            float falseValue,
            Func<float, float> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public TextFontSizeSwitcherBinder(
            TMP_Text target, 
            float trueValue, 
            float falseValue,
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif