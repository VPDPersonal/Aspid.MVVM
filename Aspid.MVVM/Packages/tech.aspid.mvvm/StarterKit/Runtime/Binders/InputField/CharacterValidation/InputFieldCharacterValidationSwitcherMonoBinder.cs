using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(
        typeof(TMP_InputField),
        serializePropertyNames: "m_CharacterValidation",
        SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterValidation Switcher")]
    public sealed class InputFieldCharacterValidationSwitcherMonoBinder
        : SwitcherMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            CachedComponent.characterValidation = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
