#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(TMP_Text), "m_fontSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - FontSize Switcher")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - FontSize Switcher")]
    public sealed class TextFontSizeSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, float>
    {
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif