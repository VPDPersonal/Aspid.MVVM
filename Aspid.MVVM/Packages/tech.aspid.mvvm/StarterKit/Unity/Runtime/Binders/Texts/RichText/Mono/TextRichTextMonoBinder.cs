#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="TMP_Text.richText"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_isRichText")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – RichText")]
    public class TextRichTextMonoBinder : ComponentMonoBinder<TMP_Text, bool>
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
