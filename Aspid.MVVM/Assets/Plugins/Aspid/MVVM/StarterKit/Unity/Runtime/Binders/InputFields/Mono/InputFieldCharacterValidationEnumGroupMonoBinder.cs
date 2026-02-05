#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - CharacterValidation EnumGroup")]
    public sealed class InputFieldCharacterValidationEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField>
    {
        [SerializeField] private TMP_InputField.CharacterValidation _defaultValue;
        [SerializeField] private TMP_InputField.CharacterValidation _selectedValue;
        
        protected override void SetDefaultValue(TMP_InputField element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(TMP_InputField element) =>
            SetValue(element, _selectedValue);
        
        private static void SetValue(TMP_InputField element, TMP_InputField.CharacterValidation value) 
        {
            element.characterValidation = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif