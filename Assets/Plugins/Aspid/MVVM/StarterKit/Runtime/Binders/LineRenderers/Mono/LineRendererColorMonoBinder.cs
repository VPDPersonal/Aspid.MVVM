using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Line Renderers/LineRenderer Binder - Color")]
    public class LineRendererColorMonoBinder : ComponentMonoBinder<LineRenderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Color, Color> _converter;
#else
        private IConverterColor _converter;
#endif
        
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetColor(value, _mode);
        }
    }
}