using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TMP_Text}"/> that binds <see cref="TMP_Text.maxVisibleCharacters"/>.
    /// </summary>
    /// <remarks>
    /// The default is large enough to mean "all of them"; <c>0</c> hides the text without clearing it.
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
