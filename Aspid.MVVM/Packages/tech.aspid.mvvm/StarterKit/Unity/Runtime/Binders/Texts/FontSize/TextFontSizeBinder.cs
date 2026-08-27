#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TMP_Text}"/> that sets the <see cref="TMP_Text.fontSize"/> property.
    /// </summary>
    /// <remarks>
    /// Non-finite values are ignored — TMP would otherwise rebuild the mesh from <see cref="float.NaN"/>
    /// and the text disappears entirely.
    /// </remarks>
    /// <include file="XmlExampleDoc-Text-FontSize-1.1.0.xml" path="doc//member[@name='TextFontSizeBinder']/*" />
    [Serializable]
    public class TextFontSizeBinder : TargetFloatBinder<TMP_Text>
    {
        protected sealed override float Property
        {
            get => Target.fontSize;
            set
            {
                if (!BinderMath.IsFinite(value)) return;
                Target.fontSize = value;
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public TextFontSizeBinder(TMP_Text target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay) 
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif