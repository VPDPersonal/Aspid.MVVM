using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Line Renderers/Line Renderer Binder - Color Switcher")]
    public sealed class LineRendererColorSwitcherMonoBinder : SwitcherMonoBinder<LineRenderer, Color>
    {
        [SerializeField] private LineRendererColorMode _mode = LineRendererColorMode.StartAndEnd;
        
        protected override void SetValue(Color value) =>
            CachedComponent.SetColor(value, _mode);
    }
}