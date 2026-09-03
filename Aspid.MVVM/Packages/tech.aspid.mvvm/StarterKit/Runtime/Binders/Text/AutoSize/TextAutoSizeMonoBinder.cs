using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_Text.enableAutoSizing"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_enableAutoSizing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – AutoSize")]
    public class TextAutoSizeMonoBinder : ComponentMonoBinder<TMP_Text, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.enableAutoSizing;
            set => CachedComponent.enableAutoSizing = value;
        }
    }
}
