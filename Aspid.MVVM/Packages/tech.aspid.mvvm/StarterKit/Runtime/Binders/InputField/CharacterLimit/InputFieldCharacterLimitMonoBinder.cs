using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="TMP_InputField.characterLimit"/>.
    /// </summary>
    /// <remarks>
    /// <c>0</c> or a negative value means no limit. Lowering the limit does not shorten existing text.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterLimit")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterLimit")]
    public class InputFieldCharacterLimitMonoBinder : ComponentIntMonoBinder<TMP_InputField>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.characterLimit;
            set => CachedComponent.characterLimit = value;
        }
    }
}
