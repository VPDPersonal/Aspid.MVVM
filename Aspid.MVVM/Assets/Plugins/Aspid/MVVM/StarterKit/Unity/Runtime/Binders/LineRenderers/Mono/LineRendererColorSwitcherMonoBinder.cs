using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Line Renderers/LineRenderer Binder – Color Switcher")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Switcher")]
    public sealed class LineRendererColorSwitcherMonoBinder : SwitcherMonoBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetColor(value, _colorMode);
        }
    }
}