using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Material Switcher")]
    public sealed class RawImageMaterialSwitcherMonoBinder : SwitcherMonoBinder<RawImage, Material>
    {
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}