using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha")]
    public partial class CanvasGroupAlphaMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>, IBinder<float>
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