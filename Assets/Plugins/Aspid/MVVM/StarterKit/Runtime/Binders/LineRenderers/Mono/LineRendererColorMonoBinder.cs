using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Line Renderers/Line Renderer Binder - Color")]
    public class LineRendererColorMonoBinder : ComponentMonoBinder<LineRenderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
        
        [Header("Converter")]
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Color, Color> _converter;
#else
        [SerializeReference] private IConverterColorToColor _converter;
#endif
        
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetColor(value, _mode);
        }
    }
}