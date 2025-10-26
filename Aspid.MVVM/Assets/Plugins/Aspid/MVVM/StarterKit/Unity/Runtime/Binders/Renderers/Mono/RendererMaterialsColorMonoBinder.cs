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
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder - MaterialsColor")]
    [AddComponentContextMenu(typeof(Renderer),"Add Renderer Binder/Renderer Binder - MaterialsColor")]
    public partial class RendererMaterialsColorMonoBinder : ComponentMonoBinder<Renderer>, IColorBinder
    {
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            
            foreach (var material in CachedComponent.materials)
                material.SetColor(ColorPropertyId, value);
        }
    }
}