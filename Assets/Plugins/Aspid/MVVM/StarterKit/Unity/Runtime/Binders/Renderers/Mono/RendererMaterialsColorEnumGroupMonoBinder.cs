using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddPropertyContextMenu(typeof(Renderer), "m_Materials")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - MaterialsColor EnumGroup")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - MaterialsColor EnumGroup")]
    public sealed class RendererMaterialsColorEnumGroupMonoBinder : EnumGroupMonoBinder<Renderer>
    {
        [Header("Parameters")]
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
            SetValue(element, value);
        }

        protected override void SetSelectedValue(Renderer element)
        {
            var value = _selectedValuerConverter?.Convert(_selectedValues) ?? _selectedValues;
            SetValue(element, value);
        }

        private void SetValue(Renderer element, Color value)
        {
            foreach (var material in element.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}