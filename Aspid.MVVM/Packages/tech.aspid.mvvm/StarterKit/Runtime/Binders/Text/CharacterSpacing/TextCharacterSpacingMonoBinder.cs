using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="TMP_Text.characterSpacing"/>.
    /// </summary>
    /// <remarks>
    /// Only a finite value is applied.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_characterSpacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – CharacterSpacing")]
    public class TextCharacterSpacingMonoBinder : ComponentFloatMonoBinder<TMP_Text>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.characterSpacing;
            set
            {
                if (this.RequireFinite(value))
                    CachedComponent.characterSpacing = value;
            }
        }
    }
}
