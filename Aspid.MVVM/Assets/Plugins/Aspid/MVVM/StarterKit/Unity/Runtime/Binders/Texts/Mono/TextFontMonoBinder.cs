#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Font")]
    [AddPropertyContextMenu(typeof(TMP_Text), "m_text")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - Font")]
    public partial class TextFontMonoBinder : ComponentMonoBinder<TMP_Text>, IBinder<TMP_FontAsset>
    {
        [BinderLog]
        public void SetValue(TMP_FontAsset value) =>
            CachedComponent.font = value;
    }
}
#endif