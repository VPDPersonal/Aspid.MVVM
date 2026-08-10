#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder{TMP_Text}"/> that binds <see cref="TMP_Text.richText"/>.
    /// </summary>
    /// <remarks>
    /// Whether tags in the text are interpreted or shown literally. Worth binding when the string comes from
    /// somewhere the game does not control — a player name, a server message — and its markup should be shown
    /// rather than obeyed.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_isRichText")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – RichText")]
    public class TextRichTextMonoBinder : ComponentBoolMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.richText;
            set => CachedComponent.richText = value;
        }
    }
}
#endif
