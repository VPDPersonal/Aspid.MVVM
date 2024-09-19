using UnityEngine;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha")]
    public partial class CanvasGroupAlphaMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>, IBinder<float>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterFloatToFloat Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? 1 : 0);
        
        [BinderLog]
        public void SetValue(float value)
        {
            value = Converter?.Convert(value) ?? value;
            CachedComponent.alpha = Mathf.Clamp(value, 0, 1);
        }
    }
}