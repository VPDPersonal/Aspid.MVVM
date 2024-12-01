using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material Switcher")]
    public sealed class RendererMaterialSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material>
    {
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}