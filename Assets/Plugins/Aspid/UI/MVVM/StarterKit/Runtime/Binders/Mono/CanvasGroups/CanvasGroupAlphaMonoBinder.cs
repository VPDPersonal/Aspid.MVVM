using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha")]
    public partial class CanvasGroupAlphaMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>, IBinder<float>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterFloatToFloat _converter;
        
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? 1 : 0);
        
        [BinderLog]
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.alpha = Mathf.Clamp(value, 0, 1);
        }
    }
}