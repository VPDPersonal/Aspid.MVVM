using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder - MinMax EnumGroup")]
    public sealed class SliderMinMaxEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [Header("Parameters")]
        [SerializeField] private Vector2 _defaultValue;
        [SerializeField] private Vector2 _selectedValue;
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
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