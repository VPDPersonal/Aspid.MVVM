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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value Switcher")]
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    public sealed class SliderValueSwitcherMonoBinder : SwitcherMonoBinder<Slider, float>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) =>
            CachedComponent.value = _converter?.Convert(value) ?? value;
    }
}