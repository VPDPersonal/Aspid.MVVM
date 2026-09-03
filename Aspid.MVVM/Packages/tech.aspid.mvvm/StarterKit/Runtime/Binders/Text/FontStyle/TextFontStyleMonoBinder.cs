using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_Text.fontStyle"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontStyle")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontStyle")]
    public class TextFontStyleMonoBinder : ComponentMonoBinder<TMP_Text, FontStyles>
    {
        /// <inheritdoc/>
        protected sealed override FontStyles Property
        {
            get => CachedComponent.fontStyle;
            set => CachedComponent.fontStyle = value;
        }
    }
}
