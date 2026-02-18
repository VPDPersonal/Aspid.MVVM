using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/LineRenderer/LineRenderer Binder – Color Enum")]
    [AddBinderContextMenu(typeof(LineRenderer), serializePropertyNames: "colorGradient", SubPath = "Enum")]
    public sealed class LineRendererColorEnumMonoBinder : EnumMonoBinder<LineRenderer, Color, Converter>
    {
        [SerializeField] private LineRendererColorMode _colorMode = LineRendererColorMode.StartAndEnd;

        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _colorMode);
    }
}