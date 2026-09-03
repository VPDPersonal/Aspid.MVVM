using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="TMP_Text.maxVisibleCharacters"/>.
    /// </summary>
    /// <remarks>
    /// <c>0</c> hides the text without clearing it.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_maxVisibleCharacters")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – MaxVisibleCharacters")]
    public class TextMaxVisibleCharactersMonoBinder : ComponentIntMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.maxVisibleCharacters;
            set => CachedComponent.maxVisibleCharacters = value;
        }
    }
}
