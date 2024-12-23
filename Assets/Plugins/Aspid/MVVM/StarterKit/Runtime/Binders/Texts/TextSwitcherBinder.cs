#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextSwitcherBinder : SwitcherBinder<TMP_Text, string>
    {
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public TextSwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            Func<string?, string?> converter) 
            : this(target, trueValue, falseValue, converter.ToConvert()) { }
        
        public TextSwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            IConverter<string?, string?>? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(string value) =>
            Target.text = _converter?.Convert(value) ?? value;
    }
}
#endif