#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_Text, TMP_FontAsset}"/> that sets the <see cref="TMP_Text.font"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current font
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset")]
    public class TextFontMonoBinder : ComponentMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        protected sealed override TMP_FontAsset Property
        {
            get => CachedComponent.font;
            set => CachedComponent.font = value;
        }
    }
}
#endif