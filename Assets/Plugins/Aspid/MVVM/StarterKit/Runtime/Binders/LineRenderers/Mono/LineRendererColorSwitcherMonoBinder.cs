using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Line Renderers/LineRenderer Binder - Color Switcher")]
    public sealed class LineRendererColorSwitcherMonoBinder : SwitcherMonoBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetColor(value, _mode);
        }
    }
}