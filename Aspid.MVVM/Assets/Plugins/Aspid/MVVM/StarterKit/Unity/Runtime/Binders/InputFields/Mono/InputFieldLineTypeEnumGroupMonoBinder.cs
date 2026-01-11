#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType EnumGroup")]
    public sealed class InputFieldLineTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField>
    {
        [Header("Values")]
        [SerializeField] private TMP_InputField.LineType _defaultValue;
        [SerializeField] private TMP_InputField.LineType _selectedValue;
        
        protected override void SetDefaultValue(TMP_InputField element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(TMP_InputField element) =>
            SetValue(element, _selectedValue);
        
        private static void SetValue(TMP_InputField element, TMP_InputField.LineType value) 
        {
            element.lineType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif