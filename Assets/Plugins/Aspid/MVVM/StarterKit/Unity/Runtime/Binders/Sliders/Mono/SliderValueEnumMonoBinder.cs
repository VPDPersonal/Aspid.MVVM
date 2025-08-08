using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Slider), "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder - Value Enum")]
    [AddComponentContextMenu(typeof(Slider),"Add Slider Binder/Slider Binder - Value Enum")]
    public sealed class SliderValueEnumMonoBinder : EnumMonoBinder<Slider, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        protected override void SetValue(float value) =>
            CachedComponent.value = _converter?.Convert(value) ?? value;
    }
}