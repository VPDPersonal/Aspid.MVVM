using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(LineRenderer), "colorGradient")]
    [AddComponentMenu("Aspid/MVVM/Binders/Line Renderers/LineRenderer Binder - Color EnumGroup")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add LineRenderer Binder/LineRenderer Binder - Color EnumGroup")]
    public sealed class LineRendererColorEnumGroupMonoBinder : EnumGroupMonoBinder<LineRenderer>
    {
        [Header("Parameters")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValue;
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;
     
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(LineRenderer element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.SetColor(value, _colorMode);
        }

        protected override void SetSelectedValue(LineRenderer element)
        {
            var value = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
            element.SetColor(value, _colorMode);
        }
    }
}