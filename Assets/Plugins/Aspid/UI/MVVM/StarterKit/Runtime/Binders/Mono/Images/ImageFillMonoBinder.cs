using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillMonoBinder : ComponentMonoBinder<Image>, IBinder<float>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterFloatToFloat _converter;
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.fillAmount = _converter?.Convert(value) ?? value;
    }

}