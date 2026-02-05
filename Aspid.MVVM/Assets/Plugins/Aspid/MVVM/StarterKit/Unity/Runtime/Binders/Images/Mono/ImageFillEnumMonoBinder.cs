using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount")]
    public sealed class ImageFillEnumMonoBinder : EnumMonoBinder<Image, float>
    {
        [SerializeReferenceDropdown] 
        [SerializeReference] private Converter _converter;

        protected override void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.fillAmount = Mathf.Clamp01(value);
        }
    }
}