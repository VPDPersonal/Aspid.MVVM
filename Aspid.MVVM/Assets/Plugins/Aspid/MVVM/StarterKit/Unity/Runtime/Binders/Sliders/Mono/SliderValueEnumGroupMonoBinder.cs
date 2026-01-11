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
    [AddBinderContextMenu(typeof(Slider), serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – Value EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(Slider element) =>
            element.value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(Slider element) =>
            element.value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}