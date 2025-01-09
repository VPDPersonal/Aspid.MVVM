using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Slider/Slider Binder - MinMax EnumGroup")]
    public sealed class SliderMinMaxEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [Header("Parameters")]
        [SerializeField] private Vector2 _defaultValue;
        [SerializeField] private Vector2 _selectedValue;
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _defaultValueConverter;
#else
        private IConverterVector2 _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _selectedValueConverter;
#else
        private IConverterVector2 _selectedValueConverter;
#endif
        
        protected override void SetDefaultValue(Slider element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetMinMax(value, _mode);
        }

        protected override void SetSelectedValue(Slider element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetMinMax(value, _mode);
        }
    }
}