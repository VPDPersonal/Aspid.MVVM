using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Slider/Slider Binder - Value EnumGroup")]
    public sealed class SliderValueEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [Header("Parameters")]
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _defaultValueConverter;
#else
        private IConverterFloat _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _selectedValueConverter;
#else
        private IConverterFloat _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(Slider element) =>
            element.value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(Slider element) =>
            element.value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}