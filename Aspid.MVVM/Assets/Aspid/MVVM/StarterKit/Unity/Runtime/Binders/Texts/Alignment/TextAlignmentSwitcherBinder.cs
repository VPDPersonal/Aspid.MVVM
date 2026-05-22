#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

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
        /// <summary>
        /// Initializes a new instance of <see cref="TextAlignmentSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_Text"/> to bind.</param>
        /// <param name="trueValue">The alignment applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The alignment applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode.</param>
        public TextAlignmentSwitcherBinder(
            TMP_Text target,
            TextAlignmentOptions trueValue,
            TextAlignmentOptions falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.alignment"/>.
        /// </summary>
        protected override void SetValue(TextAlignmentOptions value) =>
            Target.alignment = value;
    }
}
#endif