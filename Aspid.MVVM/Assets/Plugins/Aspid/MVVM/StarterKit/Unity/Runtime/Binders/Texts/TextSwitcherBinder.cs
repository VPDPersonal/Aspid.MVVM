#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class TextSwitcherBinder : SwitcherBinder<TMP_Text, string, Converter>
    {
        public TextSwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            BindMode mode) 
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public TextSwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(string value) =>
            Target.text = value;
    }
}
#endif