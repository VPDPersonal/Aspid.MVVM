#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - CharacterValidation Enum")]
    public sealed class InputFieldCharacterValidationEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            CachedComponent.characterValidation = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif