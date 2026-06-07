#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherStringBinder{TMP_Text}"/> that switches the <see cref="TMP_Text.text"/> between two
    /// string values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Text-1.1.0.xml" path="doc//member[@name='TextSwitcherBinder']/*" />
    [Serializable]
    public sealed class TextSwitcherBinder : SwitcherStringBinder<TMP_Text>
    {
        /// <inheritdoc/>
        public TextSwitcherBinder(
            TMP_Text target, 
            string trueValue, 
            string falseValue,
            IConverter<string?, string?>? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.text"/>.
        /// </summary>
        protected override void SetValue(string? value) =>
            Target.text = value;
    }
}
#endif