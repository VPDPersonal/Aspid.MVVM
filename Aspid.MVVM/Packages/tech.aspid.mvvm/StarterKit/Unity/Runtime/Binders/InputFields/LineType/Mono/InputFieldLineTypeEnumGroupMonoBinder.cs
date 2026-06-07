#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TMP_InputField, TMP_InputField.LineType}"/> that sets
    /// <see cref="TMP_InputField.lineType"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "EnumGroup")]
    public sealed class InputFieldLineTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="TMP_InputField.lineType"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField element, TMP_InputField.LineType value) 
        {
            element.lineType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif