#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

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
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TextFontBinder(TMP_Text target, IConverter<TMP_FontAsset, TMP_FontAsset>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected sealed override TMP_FontAsset? Property
        {
            get => Target.font;
            set => Target.font = value;
        }
    }
}
#endif