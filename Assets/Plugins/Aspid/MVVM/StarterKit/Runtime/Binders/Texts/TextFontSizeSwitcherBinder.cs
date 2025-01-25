#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextFontSizeSwitcherBinder : SwitcherBinder<TMP_Text, float>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<float, float>? _converter;

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
            IConverter<float, float>? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif