#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_Text), "m_fontSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - FontSize Enum")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - FontSize Enum")]
    public sealed class TextFontSizeEnumMonoBinder : EnumMonoBinder<TMP_Text, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = _converter?.Convert(value) ?? value;
    }
}
#endif