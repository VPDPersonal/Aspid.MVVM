using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Line Renderers/LineRenderer Binder - Color EnumGroup")]
    public sealed class LineRendererColorEnumGroupMonoBinder : EnumGroupMonoBinder<LineRenderer>
    {
        [Header("Parameters")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValue;
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
     
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
        
        protected override void SetDefaultValue(LineRenderer element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetColor(value, _mode);
        }

        protected override void SetSelectedValue(LineRenderer element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetColor(value, _mode);
        }
    }
}