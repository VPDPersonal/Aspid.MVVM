using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillMonoBinder : ComponentMonoBinder<Image>, IBinder<float>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterFloatToFloat Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.fillAmount = Converter?.Convert(value) ?? value;
    }

}