using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that sets the <see cref="TMP_Text.alignment"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment")]
    public class TextAlignmentMonoBinder : ComponentMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <inheritdoc/>
        protected sealed override TextAlignmentOptions Property
        {
            get => CachedComponent.alignment;
            set => CachedComponent.alignment = value;
        }
    }
}