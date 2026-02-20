#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType")]
    public class InputFieldLineTypeMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        protected sealed override TMP_InputField.LineType Property
        {
            get => CachedComponent.lineType;
            set
            {
                CachedComponent.lineType = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
#endif