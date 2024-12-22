#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Text/Text Binder - Switcher")]
    public sealed class TextSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, string>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<string, string> _converter;
#else
        private IConverterString _converter;
#endif
        
        protected override void SetValue(string value) =>
            CachedComponent.text = _converter?.Convert(value) ?? value;
    }
}
#endif