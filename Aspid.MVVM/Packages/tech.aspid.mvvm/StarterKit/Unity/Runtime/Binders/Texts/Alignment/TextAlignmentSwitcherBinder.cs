#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_Text, TextAlignmentOptions}"/> that switches the <see cref="TMP_Text.alignment"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Alignment-1.1.0.xml" path="doc//member[@name='TextAlignmentSwitcherBinder']/*" />
    [Serializable]
    public sealed class TextAlignmentSwitcherBinder : SwitcherBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        public TextAlignmentSwitcherBinder(
            TMP_Text target,
            TextAlignmentOptions trueValue,
            TextAlignmentOptions falseValue,
            IConverter<TextAlignmentOptions, TextAlignmentOptions>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(TextAlignmentOptions value) =>
            Target.alignment = value;
    }
}
#endif