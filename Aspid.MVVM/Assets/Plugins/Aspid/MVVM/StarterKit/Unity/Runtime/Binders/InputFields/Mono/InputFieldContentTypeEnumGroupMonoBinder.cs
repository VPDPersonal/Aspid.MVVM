#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType EnumGroup")]
    public sealed class InputFieldContentTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField>
    {
        [Header("Values")]
        [SerializeField] private TMP_InputField.ContentType _defaultValue;
        [SerializeField] private TMP_InputField.ContentType _selectedValue;
        
        protected override void SetDefaultValue(TMP_InputField element) =>
            SetValue(element, _defaultValue);

        protected override void SetSelectedValue(TMP_InputField element) =>
            SetValue(element, _selectedValue);
        
        private static void SetValue(TMP_InputField element, TMP_InputField.ContentType value) 
        {
            element.contentType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif