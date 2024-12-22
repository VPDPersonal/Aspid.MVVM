using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Renderer/Renderer Binder - MaterialColor EnumGroup")]
    public sealed class RendererMaterialColorEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Header("Parameter")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValues;
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _defaultValueConverter;
#else
        sprivate IConverterColor _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _selectedValuerConverter;
#else
        sprivate IConverterColor _selectedValuerConverter;
#endif
        
        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        protected override void SetDefaultValue(Renderer element)
        {
            var value = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;
            element.material.SetColor(ColorPropertyId, value);
        }

        protected override void SetSelectedValue(Renderer element)
        {
            var value = _selectedValuerConverter?.Convert(_selectedValues) ?? _selectedValues;
            element.material.SetColor(ColorPropertyId, value);
        }
    }
}