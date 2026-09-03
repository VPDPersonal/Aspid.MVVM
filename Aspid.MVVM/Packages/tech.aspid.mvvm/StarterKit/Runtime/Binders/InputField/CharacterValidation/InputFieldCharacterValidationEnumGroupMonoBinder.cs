using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_InputField.characterValidation"/>
    /// on each element.
    /// </summary>
    [AddBinderContextMenu(
        typeof(TMP_InputField),
        serializePropertyNames: "m_CharacterValidation",
        SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterValidation EnumGroup")]
    public sealed class InputFieldCharacterValidationEnumGroupMonoBinder
        : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField element, TMP_InputField.CharacterValidation value)
        {
            element.characterValidation = value;
            element.ForceLabelUpdate();
        }
    }
}
