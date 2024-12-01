using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material Color")]
    public partial class RendererMaterialColorMonoBinder : ComponentMonoBinder<Renderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private string _colorPropertyName = "_BaseColor";
        
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
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