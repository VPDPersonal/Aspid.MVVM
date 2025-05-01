using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - MaterialColor EnumGroup")]
    public sealed class RendererMaterialColorEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Header("Parameter")]
        [SerializeField] private Color _defaultValue;
        [SerializeField] private Color _selectedValues;
        [SerializeField] private string _colorPropertyName = "_BaseColor";

        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValuerConverter;
        
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