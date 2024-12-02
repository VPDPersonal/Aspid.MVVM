using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillMonoBinder : ComponentMonoBinder<Image>, IBinder<float>
    {
        [Header("Converter")]
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<float, float> _converter;
#else
        [SerializeReference] private IConverterFloatToFloat _converter;
#endif
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.fillAmount = _converter?.Convert(value) ?? value;
    }

}