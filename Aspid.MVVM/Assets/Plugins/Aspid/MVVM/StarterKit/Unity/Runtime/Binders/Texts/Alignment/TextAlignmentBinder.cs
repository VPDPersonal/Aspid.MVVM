#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_Text, TextAlignmentOptions}"/> that sets the <see cref="TMP_Text.alignment"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-Text-Alignment-1.1.0.xml" path="doc//member[@name='TextAlignmentBinder']/*" />
    [Serializable]
    public class TextAlignmentBinder : TargetBinder<TMP_Text, TextAlignmentOptions>
    {
        protected sealed override TextAlignmentOptions Property
        {
            get => Target.alignment;
            set => Target.alignment = value;
        }
        
        /// <summary>
        /// Initializes a new instance of <see cref="TextAlignmentBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_Text"/> to bind.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public TextAlignmentBinder(TMP_Text target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
#endif