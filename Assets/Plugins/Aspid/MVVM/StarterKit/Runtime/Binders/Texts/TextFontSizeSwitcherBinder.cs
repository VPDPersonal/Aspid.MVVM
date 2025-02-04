#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
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
            BindMode mode = BindMode.OneWay) 
            : this(target, trueValue, falseValue, null, mode) { }
        
        public TextFontSizeSwitcherBinder(
            TMP_Text target, 
            float trueValue, 
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif