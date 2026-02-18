#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType")]
    public class InputFieldContentTypeMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        protected sealed override TMP_InputField.ContentType Property
        {
            get => CachedComponent.contentType;
            set
            {
                CachedComponent.contentType = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
#endif