#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TMP_InputField, TMP_InputField.LineType}"/> that switches
    /// <see cref="TMP_InputField.lineType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType Switcher")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "Switcher")]
    public sealed class InputFieldLineTypeSwitcherMonoBinder : SwitcherMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.lineType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif