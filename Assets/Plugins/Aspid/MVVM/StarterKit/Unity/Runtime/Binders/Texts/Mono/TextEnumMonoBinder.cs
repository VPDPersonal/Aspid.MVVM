#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_Text), "m_text")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Text Enum")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - Text Enum")]
    public sealed class TextEnumMonoBinder : EnumComponentMonoBinder<TMP_Text, string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(string value) =>
            CachedComponent.text = _converter?.Convert(value) ?? value;
    }
}
#endif