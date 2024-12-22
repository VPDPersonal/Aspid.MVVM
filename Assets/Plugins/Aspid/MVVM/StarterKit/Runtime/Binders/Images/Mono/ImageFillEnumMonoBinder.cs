using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Image/Image Binder - Fill Enum")]
    public sealed class ImageFillEnumMonoBinder : EnumComponentMonoBinder<Image, float>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterFloat _converter;
#endif

        protected override void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.fillAmount = Mathf.Clamp01(value);
        }
    }
}