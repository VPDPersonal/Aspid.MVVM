using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{T1, T2}">EnumGroupMonoBinder&lt;TMP_InputField, TMP_InputField.CharacterValidation&gt;</see> that sets
    /// <see cref="TMP_InputField.characterValidation"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CharacterValidation EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation", SubPath = "EnumGroup")]
    public sealed class InputFieldCharacterValidationEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="TMP_InputField.characterValidation"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField element, TMP_InputField.CharacterValidation value) 
        {
            element.characterValidation = value;
            element.ForceLabelUpdate();
        }
    }
}