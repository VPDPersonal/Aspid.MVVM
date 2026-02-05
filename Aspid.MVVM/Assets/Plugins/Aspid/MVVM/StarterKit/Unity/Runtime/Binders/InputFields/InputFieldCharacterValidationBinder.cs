#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class InputFieldCharacterValidationBinder : TargetBinder<TMP_InputField>, IBinder<TMP_InputField.CharacterValidation>
    {
        public InputFieldCharacterValidationBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        public void SetValue(TMP_InputField.CharacterValidation value)
        {
            Target.characterValidation = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif