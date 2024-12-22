#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class TextSwitcherBinder : SwitcherBinder<string>
    {
        [Header("Component")]
        [SerializeField] private TMP_Text _text;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;

        public TextSwitcherBinder(
            string trueValue, 
            string falseValue,
            TMP_Text text, 
            Func<string?, string?> converter) 
            : this(trueValue, falseValue, text, converter.ToConvert()) { }
        
        public TextSwitcherBinder(
            string trueValue, 
            string falseValue,
            TMP_Text text, 
            IConverter<string?, string?>? converter = null) 
            : base(trueValue, falseValue)
        {
            _text = text ?? throw new ArgumentException(nameof(text));
        }

        protected override void SetValue(string value) =>
            _text.text = value;
    }
}
#endif