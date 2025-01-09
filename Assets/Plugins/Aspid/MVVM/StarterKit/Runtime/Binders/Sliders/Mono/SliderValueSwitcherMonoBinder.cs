using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Slider/Slider Binder - Value Switcher")]
    public sealed class SliderValueSwitcherMonoBinder : SwitcherMonoBinder<Slider, float>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterFloat _converter;
#endif
        
        protected override void SetValue(float value) =>
            CachedComponent.value = _converter?.Convert(value) ?? value;
    }
}