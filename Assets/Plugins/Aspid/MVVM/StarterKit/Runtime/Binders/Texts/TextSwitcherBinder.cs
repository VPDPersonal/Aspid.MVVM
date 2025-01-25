#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextSwitcherBinder : SwitcherBinder<TMP_Text, string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

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
            Converter? converter = null) 
            : base(target, trueValue, falseValue)
        {
            _converter = converter;
        }

        protected override void SetValue(string value) =>
            Target.text = _converter?.Convert(value) ?? value;
    }
}
#endif