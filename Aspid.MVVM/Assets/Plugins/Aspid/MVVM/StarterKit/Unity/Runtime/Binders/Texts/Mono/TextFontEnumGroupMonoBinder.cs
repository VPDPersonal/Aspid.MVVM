#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font Enum Group")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset")]
    public sealed class TextFontEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text>
    {
        [Header("Values")]
        [SerializeField] private TMP_FontAsset _defaultValue;
        [SerializeField] private TMP_FontAsset _selectedValue;
            
        protected override void SetDefaultValue(TMP_Text element) =>
            element.font = _defaultValue;

        protected override void SetSelectedValue(TMP_Text element) =>
            element.font = _selectedValue;
    }
}
#endif