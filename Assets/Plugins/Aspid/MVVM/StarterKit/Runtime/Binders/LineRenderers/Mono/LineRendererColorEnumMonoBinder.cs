using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Line Renderers/Line Renderer Binder - Color Enum")]
    public sealed class LineRendererColorEnumMonoBinder : EnumMonoBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
        
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _mode);
    }
}