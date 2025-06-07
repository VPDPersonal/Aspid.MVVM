using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterVector2;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Slider), "m_MinValue")]
    [AddPropertyContextMenu(typeof(Slider), "m_MaxValue")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder - MinMax EnumGroup")]
    [AddComponentContextMenu(typeof(Slider),"Add Slider Binder/Slider Binder - MinMax EnumGroup")]
    public sealed class SliderMinMaxEnumGroupMonoBinder : EnumGroupMonoBinder<Slider>
    {
        [Header("Parameters")]
        [SerializeField] private Vector2 _defaultValue;
        [SerializeField] private Vector2 _selectedValue;
        [SerializeField] private SliderValueMode _valueMode = SliderValueMode.Range;
        
        [Header("Converters")]
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