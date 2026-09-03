using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterValidation Enum")]
    public sealed class InputFieldCharacterValidationEnumMonoBinder
        : EnumMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            CachedComponent.characterValidation = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
