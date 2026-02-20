#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset")]
    public class TextFontMonoBinder : ComponentMonoBinder<TMP_Text, TMP_FontAsset>
    {
        protected sealed override TMP_FontAsset Property
        {
            get => CachedComponent.font;
            set => CachedComponent.font = value;
        }
    }
}
#endif