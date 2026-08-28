#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_Text, TMP_FontAsset}"/> that switches the <see cref="TMP_Text.font"/>
    /// property between two <see cref="TMP_FontAsset"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Font-1.1.0.xml" path="doc//member[@name='TextFontSwitcherBinder']/*" />
    [Serializable]
    public sealed class TextFontSwitcherBinder : SwitcherBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        public TextFontSwitcherBinder(
            TMP_Text target, 
            TMP_FontAsset trueValue, 
            TMP_FontAsset falseValue, 
            IConverter<TMP_FontAsset?, TMP_FontAsset?>? converter = null,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(TMP_FontAsset value) =>
            Target.font = value;
    }
}
#endif