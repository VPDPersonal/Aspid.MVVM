using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material Color")]
    public partial class RendererMaterialColorMonoBinder : ComponentMonoBinder<Renderer>, IColorBinder
    {
        [Header("Parameters")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Color, Color> _converter;
#else
        [SerializeReference] private IConverterColorToColor _converter;
#endif

        private int? _colorPropertyId;
        
        private int ColorPropertyId => _colorPropertyId ??= Shader.PropertyToID(_colorPropertyName);
        
        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.material.SetColor(ColorPropertyId, value);
        }
    }
}