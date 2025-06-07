using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterColor;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(LineRenderer), "colorGradient")]
    [AddComponentMenu("Aspid/MVVM/Binders/Line Renderers/LineRenderer Binder - Color")]
    [AddComponentContextMenu(typeof(LineRenderer),"Add LineRenderer Binder/LineRenderer Binder - Color")]
    public partial class LineRendererColorMonoBinder : ComponentMonoBinder<LineRenderer>, IColorBinder
    {
        [Header("Parameter")]
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(Color value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetColor(value, _colorMode);
        }
    }
}