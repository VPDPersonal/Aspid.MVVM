#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - FontSize Switcher")]
    public sealed class TextFontSizeSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif