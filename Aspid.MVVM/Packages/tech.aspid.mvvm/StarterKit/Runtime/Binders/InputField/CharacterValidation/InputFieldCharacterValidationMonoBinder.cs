using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds
    /// <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterValidation")]
    public class InputFieldCharacterValidationMonoBinder
        : ComponentMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.CharacterValidation Property
        {
            get => CachedComponent.characterValidation;
            set
            {
                CachedComponent.characterValidation = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
