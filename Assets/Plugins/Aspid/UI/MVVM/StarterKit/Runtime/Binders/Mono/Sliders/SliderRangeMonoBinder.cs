using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Range")]
    public partial class SliderRangeMonoBinder : ComponentMonoBinder<UnityEngine.UI.Slider>, IBinder<Vector2>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterVector2ToVector2 Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            CachedComponent.minValue = value.x;
            CachedComponent.maxValue = value.y;
        }
    }
}