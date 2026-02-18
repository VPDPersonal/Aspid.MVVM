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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "EnumGroup")]
    public sealed class TextFontSizeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, float, Converter>
    {
        protected override void SetValue(TMP_Text element, float value) =>
            element.fontSize = value;
    }
}
#endif