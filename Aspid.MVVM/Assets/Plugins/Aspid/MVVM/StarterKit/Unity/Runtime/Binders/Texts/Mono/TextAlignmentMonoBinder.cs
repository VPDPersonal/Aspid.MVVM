#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment")]
    public class TextAlignmentMonoBinder : ComponentMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        protected sealed override TextAlignmentOptions Property
        {
            get => CachedComponent.alignment;
            set => CachedComponent.alignment = value;
        }
    }
}
#endif