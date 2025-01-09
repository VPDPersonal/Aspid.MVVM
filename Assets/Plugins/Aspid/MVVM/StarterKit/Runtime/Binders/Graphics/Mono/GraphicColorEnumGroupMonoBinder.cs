using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Graphic/Graphic Binder - Color EnumGroup")]
    public sealed class GraphicColorEnumGroupMonoBinder : EnumGroupMonoBinder<Graphic>
    {
        [Header("Parameters")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _defaultValueConverter;
#else
        private IConverterColor _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _selectedValueConverter;
#else
        private IConverterColor _selectedValueConverter;
#endif
        protected override void SetDefaultValue(Graphic element) =>
            element.color = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(Graphic element) =>
            element.color = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}