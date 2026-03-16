#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TMP_InputField, TMP_InputField.CharacterValidation}"/> that switches
    /// <see cref="TMP_InputField.characterValidation"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - CharacterValidation Switcher")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation", SubPath = "Switcher")]
    public sealed class InputFieldCharacterValidationSwitcherMonoBinder : SwitcherMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.characterValidation"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            CachedComponent.characterValidation = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif