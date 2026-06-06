#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TMP_InputField, TMP_InputField.InputType}"/> that switches
    /// <see cref="TMP_InputField.inputType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - InputType Switcher")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "Switcher")]
    public sealed class InputFieldInputTypeSwitcherMonoBinder : SwitcherMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.inputType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.InputType value)
        {
            CachedComponent.inputType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif