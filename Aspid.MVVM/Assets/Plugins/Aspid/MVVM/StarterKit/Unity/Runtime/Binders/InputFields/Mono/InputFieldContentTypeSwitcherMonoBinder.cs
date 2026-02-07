#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType Switcher")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "Switcher")]
    public sealed class InputFieldContentTypeSwitcherMonoBinder : SwitcherMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        protected override void SetValue(TMP_InputField.ContentType value)
        {
            CachedComponent.contentType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif