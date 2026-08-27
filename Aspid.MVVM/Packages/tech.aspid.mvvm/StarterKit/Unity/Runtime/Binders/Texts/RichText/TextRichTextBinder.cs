#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder{TMP_Text}"/> that binds <see cref="TMP_Text.richText"/>.
    /// </summary>
    [Serializable]
    public class TextRichTextBinder : TargetBoolBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.richText;
            set => Target.richText = value;
        }

        /// <inheritdoc/>
        public TextRichTextBinder(
            TMP_Text target,
            bool isInvert = false,
            BindMode mode = BindMode.OneWay)
            : base(target, isInvert, mode) { }
    }
}
#endif
