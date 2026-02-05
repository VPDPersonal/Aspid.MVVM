#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType Switcher")]
    public sealed class InputFieldLineTypeSwitcherMonoBinder : SwitcherMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        protected override void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif