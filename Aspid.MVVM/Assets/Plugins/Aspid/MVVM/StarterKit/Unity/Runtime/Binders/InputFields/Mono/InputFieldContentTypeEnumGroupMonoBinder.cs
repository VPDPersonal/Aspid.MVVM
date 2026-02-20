#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "EnumGroup")]
    public sealed class InputFieldContentTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        protected override void SetValue(TMP_InputField element, TMP_InputField.ContentType value) 
        {
            element.contentType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif