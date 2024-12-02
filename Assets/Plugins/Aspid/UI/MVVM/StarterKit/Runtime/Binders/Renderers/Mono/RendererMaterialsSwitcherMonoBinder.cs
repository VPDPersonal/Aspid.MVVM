using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Materials Switcher")]
    public sealed class RendererMaterialsSwitcherMonoBinder : SwitcherMonoBinder<Renderer, Material[]>
    {
        protected override void SetValue(Material[] value) =>
            CachedComponent.materials = value;
    }
}