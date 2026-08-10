#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBoolBinder<TMP_Text>"/> that binds <see cref="TMP_Text.richText"/>.
    /// </summary>
    /// <remarks>
    /// Whether tags in the text are interpreted or shown literally. Worth binding when the string comes from
    /// somewhere the game does not control — a player name, a server message — and its markup should be shown
    /// rather than obeyed.
    /// </remarks>
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
