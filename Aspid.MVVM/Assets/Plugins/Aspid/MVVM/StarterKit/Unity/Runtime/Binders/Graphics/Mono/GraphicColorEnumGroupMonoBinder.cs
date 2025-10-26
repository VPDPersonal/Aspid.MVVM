using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Graphic), "m_Color")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Graphic/Graphic Binder - Color EnumGroup")]
    [AddComponentContextMenu(typeof(Graphic),"Add Graphic Binder/Graphic Binder - Color EnumGroup")]
    public sealed class GraphicColorEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic>
    {
        [Header("Values")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(Graphic element) =>
            element.color = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(Graphic element) =>
            element.color = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}