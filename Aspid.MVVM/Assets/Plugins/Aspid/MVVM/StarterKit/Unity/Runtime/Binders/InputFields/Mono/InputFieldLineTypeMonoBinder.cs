#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType")]
    public partial class InputFieldLineTypeMonoBinder : ComponentMonoBinder<TMP_InputField>, IBinder<TMP_InputField.LineType>
    {
        [BinderLog]
        public void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif