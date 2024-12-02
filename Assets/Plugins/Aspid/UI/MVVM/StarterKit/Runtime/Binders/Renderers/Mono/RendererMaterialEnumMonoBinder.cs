using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Renderer/Renderer Binder - Material Enum")]
    public sealed class RendererMaterialEnumMonoBinder : EnumMonoBinder<Renderer, Material>
    {
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}