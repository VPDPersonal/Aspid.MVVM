#if UNITY_2023_1_OR_NEWER || ASPID_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Enum")]
    public sealed class TextEnumMonoBinder : EnumMonoBinder<TMP_Text, string>
    {
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif