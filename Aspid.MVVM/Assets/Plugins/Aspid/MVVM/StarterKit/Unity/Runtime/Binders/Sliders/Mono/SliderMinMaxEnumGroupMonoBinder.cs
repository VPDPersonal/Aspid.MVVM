using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder – MinMax EnumGroup")]
    [AddBinderContextMenu(typeof(Slider), "m_MinValue", "m_MaxValue", SubPath = "EnumGroup")]
    public sealed class SliderMinMaxEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [SerializeField] private Vector2 _defaultValue;
        [SerializeField] private Vector2 _selectedValue;
        
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(Slider element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetMinMax(value, _valueMode);
        }

        protected override void SetSelectedValue(Slider element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetMinMax(value, _valueMode);
        }
    }
}