#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_Text, TMP_FontAsset}"/> that sets the <see cref="TMP_Text.font"/> property.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Font-1.1.0.xml" path="doc//member[@name='TextFontBinder']/*" />
    [Serializable]
    public class TextFontBinder : TargetBinder<TMP_Text, TMP_FontAsset>
    {
        protected sealed override TMP_FontAsset? Property
        {
            get => Target.font;
            set => Target.font = value;
        }

        /// <inheritdoc/>
        public TextFontBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif