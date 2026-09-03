using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentObjectMonoBinder{TComponent, TObject}"/> that binds <see cref="TMP_Text.font"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font")]
    public class TextFontMonoBinder : ComponentObjectMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <inheritdoc/>
        protected sealed override TMP_FontAsset Property
        {
            get => CachedComponent.font;
            set => CachedComponent.font = value;
        }
    }
}
