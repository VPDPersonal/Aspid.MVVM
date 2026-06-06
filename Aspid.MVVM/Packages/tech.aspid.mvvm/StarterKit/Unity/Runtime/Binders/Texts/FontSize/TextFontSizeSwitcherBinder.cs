#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{TMP_Text}"/> that switches the <see cref="TMP_Text.fontSize"/> between two
    /// <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-FontSize-1.1.0.xml" path="doc//member[@name='TextFontSizeSwitcherBinder']/*" />
    [Serializable]
    public sealed class TextFontSizeSwitcherBinder : SwitcherFloatBinder<TMP_Text>
    {
        /// <inheritdoc/>
        public TextFontSizeSwitcherBinder(
            TMP_Text target, 
            float trueValue, 
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.fontSize"/>.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.fontSize = value;
    }
}
#endif